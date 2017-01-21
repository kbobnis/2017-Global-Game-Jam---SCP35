using System;
using UnityEngine;

namespace Characters
{
	[Serializable]
	public class MeleeAttack
	{
		public float Width;
		public float MinRange;
		public float MaxRange;
		public int Strength;
		public float Delay;
		public float Cooldown;

		public Collider Collider;
	}

	public class MeleeBody : AbstractBody
	{
		public MeleeAttack Attack;

		public override void Init()
		{
			float range = Attack.MaxRange - Attack.MinRange;
			BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
			boxCollider.center = new Vector3(0, range + 0.5f);
			boxCollider.size = new Vector3(Attack.Width, range);
			Attack.Collider = boxCollider;
		}

		public override void Fire()
		{

		}

		public override void Die()
		{

		}

		public override void Deathrattle()
		{

		}
	}
}