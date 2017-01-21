﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Characters.Abstract
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
		/// How far will bullet fly.
		/// </summary>
		public float Range;

		/// <summary>
		/// Bullet prefab name.
		/// </summary>
		public string Bullet;
	}

	[Serializable]
	public abstract class GunnerBody : AbstractBody
	{
		public const string BulletHoleGOName = "BulletHole";

		public RangedAttack Attack;

		protected Transform Transfrom;
		protected Transform BulletHole;

		public override void Init(BodyComponent parent)
		{
			base.Init(parent);
			Transfrom = parent.transform;
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
			GameObject bullet = Object.Instantiate(
				Resources.Load<GameObject>(Attack.Bullet),
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

		public override string ToString()
		{
			return JsonUtility.ToJson(this, true);
		}
	}
}