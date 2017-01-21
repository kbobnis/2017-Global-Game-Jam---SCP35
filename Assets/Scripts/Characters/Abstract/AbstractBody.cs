using System;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Abstract
{

	[Serializable]
	public enum BehaviourMode
	{
		Runner = 0,
		Chaser = 1
	}

	[Serializable]
	public struct Behaviour
	{
		public BehaviourMode Mode;
		public float Range;
		public float Angle;
		public bool SeeThroughObstacles;
		public bool ChaseThroughDoors;

		public Collider Collider;
	}

	[Serializable]
	public abstract class AbstractBody
	{
		public float Speed;
		public Behaviour Behaviour;

		protected float AttackCooldown;

		protected BodyComponent Parent;
		protected GameObject GameObject;

		protected NavMeshAgent NavMeshAgent;
		protected Rigidbody Rigidbody;
		protected Collider Collider;

		private bool _isPlayerControlled;
		public bool IsPlayerControlled
		{
			get { return _isPlayerControlled; }
			set
			{
				_isPlayerControlled = value;
				NavMeshAgent.enabled = !value;
			}
		}

		public int Hp { get; private set; }

		/// <summary>
		/// Take control of body.
		/// </summary>
		/// <param name="hAxis">Horizontal move axis</param>
		/// <param name="vAxis">Vertical move axis</param>
		public virtual void Control(float hAxis, float vAxis)
		{
			if(NavMeshAgent.enabled || IsPlayerControlled)
			{
				throw new Exception("Cannot control body with NavMeshAgent enabled!");
			}
			Rigidbody.velocity = new Vector3(hAxis * Speed, vAxis * Speed);
		}

		/// <summary>
		/// Die
		/// </summary>
		public virtual void Die()
		{

			if(IsPlayerControlled)
			{
				// TODO: Do something if player was controlling this character.
			}
		}

		/// <summary>
		/// Receive damage
		/// </summary>
		/// <param name="strength">Amount of hp to lose</param>
		/// <param name="other">Attacker</param>
		public virtual void Hit(int strength, AbstractBody other)
		{
			Hp -= strength;
			if(Hp <= 0)
			{
				Die();
			}
		}

		/// <summary>
		/// Init components etc...
		/// </summary>
		///
		public virtual void Init(BodyComponent parent)
		{
			Parent = parent;
			GameObject = parent.gameObject;
			Collider = parent.GetComponent<Collider>();
			Rigidbody = parent.GetComponent<Rigidbody>();
			NavMeshAgent = parent.GetComponent<NavMeshAgent>();

			IsPlayerControlled = false;
		}

		/// <summary>
		/// Attack with your weapon and try to damage bodies
		/// in your range
		/// </summary>
		public abstract void Fire();


		/// <summary>
		/// Fire a deathrattle
		/// </summary>
		public abstract void Deathrattle();

		public override string ToString()
		{
			return JsonUtility.ToJson(this, true);
		}
	}
}