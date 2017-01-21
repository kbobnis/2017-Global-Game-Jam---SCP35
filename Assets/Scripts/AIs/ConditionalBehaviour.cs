using Characters;
using UnityEngine;

namespace AIs {
	public abstract class ConditionalBehaviour {
		public abstract void React(Transform playerTransform);
		public abstract void Idle();
	}
}