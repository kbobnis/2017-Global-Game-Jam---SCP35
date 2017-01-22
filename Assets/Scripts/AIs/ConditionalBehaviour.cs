using System;
using Characters;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AIs {
	public enum State {
		Idle,
		Spotted
	}

	public abstract class ConditionalBehaviour {
		public State State;
		protected NavMeshAgent NavMeshAgent;

		public void Init(NavMeshAgent agent) {
			NavMeshAgent = agent;
		}

		public abstract void React(Transform playerTransform);

		public virtual void Idle() {
			if (!NavMeshAgent.pathPending &&
				NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance &&
				(!NavMeshAgent.hasPath || Math.Abs(NavMeshAgent.velocity.sqrMagnitude) < 0.001f)) {
				FindNewPatrolDestination();
			}
		}

		public void FindNewPatrolDestination() {
			NavMeshAgent.SetDestination(new Vector3(Random.Range(0, 14) + 1.0f, 0, Random.Range(0, 7) + 1.0f));
		}

		public void Update() {

		}
	}
}