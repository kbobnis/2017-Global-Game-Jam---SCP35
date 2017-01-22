using UnityEngine;
using UnityEngine.AI;

namespace Controllers {
	public enum State {
		Idle, Alerted
	}

	[RequireComponent(typeof(NavMeshAgent))]
	public class AIController : MonoBehaviour {

		public Transform Transform;
		public NavMeshAgent NavMeshAgent;
		public State AIState;
		public Transform Target;
		public float Range;
		public float Angle;
		private float _delay;

		public void Start() {
			NavMeshAgent = GetComponent<NavMeshAgent>();
			Transform = transform;
		}

		public void Update() {
			if(_delay > 3.5f) {
				AIState = State.Idle;
				Target = null;
				FindNewPatrolDestination();
			}

			if(AIState == State.Idle) {
				Idle();

				Collider[] colliders = Physics.OverlapSphere(Transform.position, Range);
				foreach(Collider col in colliders) {
					Vector2 from = new Vector2(Transform.forward.x, Transform.forward.z);
					Transform colliderTransform = col.transform;
					Vector3 colliderPosition = colliderTransform.position;

					Vector2 to = new Vector2(
						colliderPosition.x - Transform.position.x,
						colliderPosition.z - Transform.position.z);

					Ray ray = new Ray(Transform.position, colliderPosition - Transform.position);
					RaycastHit raycastHit;
					if(Physics.Raycast(ray, out raycastHit, Range)) {
						if(raycastHit.transform != colliderTransform) {
							continue;
						}
					}
					if(Mathf.Abs(Vector2.Angle(from, to)) < Angle / 2) {
						if(col.GetComponent<PlayerController>() != null) {
							React(colliderTransform);
							break;
						}
					}
				}
			} else if(AIState == State.Alerted) {
				NavMeshAgent.SetDestination(Target.position);
			}
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
				Random.Range(0, 14) + 1.0f,
				0,
				Random.Range(0, 7) + 1.0f));
		}
	}
}