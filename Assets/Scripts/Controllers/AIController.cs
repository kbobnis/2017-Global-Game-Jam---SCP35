using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers {
	public enum State {
		Idle, Alerted, None
	}

	[RequireComponent(typeof(NavMeshAgent))]
	public class AIController : MonoBehaviour {

		public Transform Transform;
		public NavMeshAgent NavMeshAgent;
		public State AIState;
		public Transform Target;

		private float _delay;


		public Action OnTryToAttack;

		public void Start() {
			NavMeshAgent = GetComponent<NavMeshAgent>();
			Transform = transform;
		}

		private AgentStatsModel GetStats() {
			return GetComponent<StatsComponent>().Stats;
		}

		public void Update() {
			if (_delay > 3.5f) {
				AIState = State.Idle;
				Target = null;
				FindNewPatrolDestination();
			}

			if (AIState == State.Idle) {
				Idle();
				LinkedList<GameObject> objs = Utility.GetVisibleCharacters(Transform, GetStats().Range, GetStats().Angle);
				foreach (GameObject o in objs) {
					if (o.GetComponent<GamepadInputController>() != null) {
						AIState = State.Alerted;
						Target = o.transform;
					}
				}

			} else if (AIState == State.Alerted) {
				if (Target != null) {
					NavMeshAgent.SetDestination(Target.position);
				}
			}

			OnTryToAttack();
		}

		public void Idle() {
			if (!NavMeshAgent.pathPending &&
				NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance &&
				(!NavMeshAgent.hasPath || Mathf.Abs(NavMeshAgent.velocity.sqrMagnitude) < 0.001f)) {
				FindNewPatrolDestination();
			}
		}

		public void React(Transform target) {
			Target = target;
			AIState = State.Alerted;
		}

		public void FindNewPatrolDestination() {
			NavMeshAgent.SetDestination(new Vector3(
				UnityEngine.Random.Range(0, 14) + 1.0f,
				0,
				UnityEngine.Random.Range(0, 7) + 1.0f));
		}

	}
}