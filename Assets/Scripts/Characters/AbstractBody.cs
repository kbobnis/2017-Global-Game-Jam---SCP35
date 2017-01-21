using System;
using UnityEngine;
using UnityEngine.AI;

namespace Characters
{

	[Serializable]
	public enum BehaviourMode
	{
		Runner,
		Chaser
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
	public struct Anim
	{
		public Animation Walk;
		public Animation Attack;
	}

	[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody), typeof(Collider))]
	public abstract class AbstractBody : MonoBehaviour
	{
		public float Hp;
		public float Speed;
		public Behaviour Behaviour;
		public Anim Anim;

		protected float AttackCooldown;

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

		public void Start()
		{
			Collider = GetComponent<Collider>();
			Rigidbody = GetComponent<Rigidbody>();
			NavMeshAgent = GetComponent<NavMeshAgent>();

			IsPlayerControlled = false;

			Init();
		}

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
			Destroy(gameObject);
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
		/// Init components etc.. Called by superclass in Start().
		/// </summary>
		public abstract void Init();

		/// <summary>
		/// Attack with your weapon and try to damage bodies
		/// in your range
		/// </summary>
		public abstract void Fire();


		/// <summary>
		/// Fire a deathrattle
		/// </summary>
		public abstract void Deathrattle();
	}
}