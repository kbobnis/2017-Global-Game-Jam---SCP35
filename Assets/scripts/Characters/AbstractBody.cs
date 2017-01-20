using System;
using UnityEngine;
using UnityEngine.AI;

namespace Characters
{
	public struct Attack
	{
		public int Strength;
		public float Range;
		public float Angle;
		public float Delay;
		public float Cooldown;
	}

	public enum BehaviourMode
	{
		Runner,
		Chaser
	}

	public struct Behaviour
	{
		public BehaviourMode Mode;
		public float Range;
		public float Angle;
		public bool SeeThroughObstacles;
		public bool ChaseThroughDoors;
	}

	public struct Anim
	{
		public Animation Walk;
		public Animation Attack;
	}

	[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody), typeof(Collider))]
	public abstract class AbstractBody : MonoBehaviour
	{
		public int Hp;
		public int Speed;
		public Attack Attack;
		public Behaviour Behaviour;
		public Anim Anim;

		public NavMeshAgent NavMeshAgent;
		public Rigidbody Rigidbody;
		public Collider Collider;

		private bool _isUserControlled;
		public bool IsUserControlled
		{
			get { return _isUserControlled; }
			set
			{
				_isUserControlled = value;
				NavMeshAgent.enabled = value;
			}
		}

		public void Start()
		{
			IsUserControlled = false;
			Collider = GetComponent<Collider>();
			Rigidbody = GetComponent<Rigidbody>();
			NavMeshAgent = GetComponent<NavMeshAgent>();
		}

		/// <summary>
		/// Take control of body.
		/// </summary>
		/// <param name="hAxis">Horizontal move axis</param>
		/// <param name="vAxis">Vertical move axis</param>
		public virtual void Control(float hAxis, float vAxis)
		{
			if(NavMeshAgent.enabled || IsUserControlled)
			{
				throw new Exception("Cannot control body with NavMeshAgent enabled!");
			}
			Rigidbody.velocity = new Vector3(hAxis * Speed, vAxis * Speed);
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
		/// Attack with your weapon and try to damage bodies
		/// in your range
		/// </summary>
		public abstract void Fire();

		/// <summary>
		/// Die
		/// </summary>
		public abstract void Die();

		/// <summary>
		/// Fire a deathrattle
		/// </summary>
		public abstract void Deathrattle();
	}
}