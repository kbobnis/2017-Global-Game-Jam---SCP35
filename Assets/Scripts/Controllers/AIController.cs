using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

		protected AgentStatsModel ASM;

		private float _delay;
		private float _cooldown;

		public void Start() {
			NavMeshAgent = GetComponent<NavMeshAgent>();
			Transform = transform;
			ASM = GetComponent<StatsComponent>().Stats;
		}

		public void Update() {
			if(_delay > 3.5f) {
				AIState = State.Idle;
				Target = null;
				FindNewPatrolDestination();
			}

			if(AIState == State.Idle) {
				Idle();
				LinkedList<GameObject> objs = Utility.GetVisibleCharacters(Transform, ASM.Range, ASM.Angle);
				foreach(GameObject o in objs) {
					if(o.GetComponent<PlayerController>() != null) {
						AIState = State.Alerted;
						Target = o.transform;
					}
				}

			} else if(AIState == State.Alerted) {
				if(Target != null) {
					NavMeshAgent.SetDestination(Target.position);
				}
			}

			_cooldown += Time.deltaTime;
			if(_cooldown > ASM.Cooldown) {
				foreach(GameObject o in Utility.GetVisibleCharacters(
					Transform,
					ASM.AttackRange,
					360)) {
					if(o.GetComponent<PlayerController>() != null) {
						//StartCoroutine(Attack(o));
						_cooldown = 0.0f;
						break;
					}
				}
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

		public IEnumerator Attack(GameObject o) {
			yield return new WaitForSeconds(ASM.Delay);
			if(gameObject != null &&
			   o != null &&
			   Vector2.Distance(
				   new Vector2(Transform.position.x, Transform.position.z),
				   new Vector2(o.transform.position.x, o.transform.position.z)) < ASM.Range) {
				o.GetComponent<StatsComponent>().Damage();
			}
		}
	}
}