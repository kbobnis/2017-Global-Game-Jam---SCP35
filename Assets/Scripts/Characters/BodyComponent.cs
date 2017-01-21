using AIs;
using Characters.Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace Characters
{
	public struct CharacterPosition {
		public int X;
		public int Y;
	}

	[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody), typeof(Collider))]
	public class BodyComponent : MonoBehaviour {
		public AbstractBody Body;
		public CharacterPosition Room;
		protected Transform Transform;

		public void Start() {
			Body.Init(this);
			Transform = transform;
		}

		public void Update() {
			// Check if NPC can see player
			if(!Body.IsPlayerControlled) {
				if(Body.Behaviour.StateMachine.State == State.Idle) {
					Body.Behaviour.StateMachine.Idle();
				} else {
					Collider[] colliders = Physics.OverlapSphere(Transform.position, Body.Behaviour.Range);
					foreach(Collider col in colliders) {
						Vector2 from = new Vector2(Transform.forward.x, Transform.forward.z);
						Transform colliderTransform = col.transform;
						Vector3 colliderPosition = colliderTransform.position;

						Vector2 to = new Vector2(
							colliderPosition.x - Transform.position.x,
							colliderPosition.z - Transform.position.z);
						if(!Body.Behaviour.SeeThroughObstacles) {
							Ray ray = new Ray(Transform.position, colliderPosition - Transform.position);
							RaycastHit raycastHit;
							if(Physics.Raycast(ray, out raycastHit, Body.Behaviour.Range)) {
								if(raycastHit.transform != colliderTransform) {
									continue;
								}
							}
						}
						if(Mathf.Abs(Vector2.Angle(from, to)) < Body.Behaviour.Angle / 2) {
							Body.Behaviour.StateMachine.React(colliderTransform);
						}
					}
				}
			}
		}
	}
}