
using Controllers;
using System;
using UnityEngine;

public class DoorController : MonoBehaviour {

	private bool IsOpening;

	void OnTriggerEnter(Collider other) {

		if (other.transform.parent.GetComponent<PlayerInput>() != null && !IsOpening) {
			IsOpening = true;
			OpenMyself();
		}
	}

	private void OpenMyself() {
		Vector3 pos = transform.position;
		gameObject.AddComponent<Changer>().Change(pos.y, pos.y - 3, 0.5f, (float value) => {
			transform.position = new Vector3(pos.x, value, pos.z);
		});
	}
}
