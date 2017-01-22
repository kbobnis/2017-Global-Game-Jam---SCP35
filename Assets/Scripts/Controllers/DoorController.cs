using Controllers;
using System;
using UnityEngine;

public class DoorController : MonoBehaviour {

	public Action DoorsAreOpening;

	public bool IsOpening;

	void OnTriggerEnter(Collider other) {

		if (other.transform.parent.GetComponent<GamepadInputController>() != null && !IsOpening) {
			IsOpening = true;
			OpenMyself();
		}
	}

	private void OpenMyself() {
		if (DoorsAreOpening != null) {
			DoorsAreOpening();
		}
		Vector3 pos = transform.position;
		gameObject.AddComponent<Changer>().Change(pos.y, pos.y - 3, 0.5f, (float value) => {
			transform.position = new Vector3(pos.x, value, pos.z);
		});
	}
}
