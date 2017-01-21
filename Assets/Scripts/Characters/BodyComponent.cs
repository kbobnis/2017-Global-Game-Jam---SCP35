using Characters.Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace Characters
{
	[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody), typeof(Collider))]
	public class BodyComponent : MonoBehaviour
	{
		public AbstractBody Body;

		public void Start()
		{
			Body.Init(this);
		}
	}
}