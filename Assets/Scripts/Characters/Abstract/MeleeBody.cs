using System;
using System.Collections;
using UnityEngine;

namespace Characters.Abstract
{
	[Serializable]
	public class MeleeAttack
	{
		/// <summary>
		/// How much hp do we take with one swing
		/// </summary>
		public float Strength;

		/// <summary>
		/// Weapon's area of effect |<--   -->|
		/// </summary>
		public float Width;
		/// <summary>
		/// Weapon's lower bound of hitbox<br />
		///      |     <br />
		///      ↓     <br />
		/// ___________
		/// </summary>
		public float MinRange;
		/// <summary>
		/// Weapon's upper bound of hitbox<br />
		/// ___________<br />
		///      ↑     <br />
		///      |
		/// </summary>
		public float MaxRange;

		/// <summary>
		/// Delay between attack initiation and actual swing.
		/// </summary>
		public float Delay;
		/// <summary>
		/// Cooldown between attacks.
		/// </summary>
		public float Cooldown;
		/// <summary>
		/// Time during which trigger should be detecting collision
		/// with hit by weapon enemies (moment of actual swing).
		/// </summary>
		public float SwingTime;

		public Collider Collider;
	}

	[Serializable]
	public abstract class MeleeBody : AbstractBody
	{
		public MeleeAttack Attack;

		public override void Init(BodyComponent parent)
		{
			base.Init(parent);
			float range = Attack.MaxRange - Attack.MinRange;
			BoxCollider boxCollider = GameObject.AddComponent<BoxCollider>();
			boxCollider.center = new Vector3(0, range + 0.5f);
			boxCollider.size = new Vector3(Attack.Width, range);
			boxCollider.enabled = false;
			Attack.Collider = boxCollider;
		}

		public override void Fire()
		{
			if(AttackCooldown > Attack.Cooldown)
			{
				Game.StartAsync(AttackAfterDelay());
				AttackCooldown = 0.0f;
			}
		}

		private IEnumerator AttackAfterDelay()
		{
			yield return new WaitForSeconds(Attack.Delay);
			Attack.Collider.enabled = true;
			yield return new WaitForSeconds(Attack.SwingTime);
			Attack.Collider.enabled = false;
		}

		protected void Update()
		{
			AttackCooldown += Time.deltaTime;
		}

		public override string ToString()
		{
			return JsonUtility.ToJson(this, true);
		}
	}
}