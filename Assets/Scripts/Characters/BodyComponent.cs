using UnityEngine;
using UnityEngine.AI;

namespace Characters {
	[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody), typeof(Collider))]
	public class BodyComponent : MonoBehaviour {

		public void Start() {
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		}
	}
}