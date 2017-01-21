using System;
using UnityEngine;

namespace Characters
{
	[Serializable]
	public class RangedAttack
	{
		/// <summary>
		/// How hard do bullet hit.
		/// </summary>
		public float Strength;
		/// <summary>
		/// How fast do bullet travel.
		/// </summary>
		public float Speed;
		/// <summary>
		/// How often can we fire.
		/// </summary>
		public float Cooldown;

		/// <summary>
		/// Bullet prefab index
		/// </summary>
		public int Bullet;
	}

	public abstract class GunnerBody : AbstractBody
	{
		public const string BulletHoleGOName = "BulletHole";

		public RangedAttack Attack;

		protected Transform Transfrom;
		protected Transform BulletHole;

		public override void Init()
		{
			Transfrom = transform;
			BulletHole = Transfrom.FindChild(BulletHoleGOName);
			if(BulletHole == null)
			{
				Game.Quit();
				throw new Exception("GunnerBody has no 'BulletHole' object attached to it's gun!" +
				                    " It will not be able to fire bullets!");
			}
		}

		public override void Fire()
		{
			if(AttackCooldown > Attack.Cooldown)
			{
				SpawnBullet();
				AttackCooldown = 0.0f;
			}
		}

		public virtual void SpawnBullet()
		{
			float lookAngle = Transfrom.eulerAngles.z;
			Vector3 bulletForce = Quaternion.AngleAxis(lookAngle, Vector3.forward) * Vector3.right * Attack.Speed;
			GameObject bullet = Instantiate(
				Game.Instance.Bullets[Attack.Bullet],
				BulletHole.position,
				Quaternion.identity);
			Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
			if(bulletRigidbody == null)
			{
				Game.Quit();
				throw new Exception("Found no Rigidbody attached to bullet!");
			}
			bulletRigidbody.velocity = bulletForce;
		}

		protected void Update()
		{
			AttackCooldown += Time.deltaTime;
		}
	}
}