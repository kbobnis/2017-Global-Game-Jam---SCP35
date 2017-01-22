using AIs;
using UnityEngine;
using UnityEngine.AI;

namespace Characters {
	public struct CharacterPosition {
		public int X;
		public int Y;
	}

	[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody), typeof(Collider))]
	public class BodyComponent : MonoBehaviour {
		public AgentStatsModel Body;
		public CharacterPosition Room;
		protected Transform Transform;

		public void Start() {
			Transform = transform;
		}

	}
}