using UnityEngine;

public class CameraMan : MonoBehaviour {

	public Transform Following;
	public Vector3 Offset = new Vector3(-4, 11, -4f);

	private void Update() {
		if (Following != null) {
			transform.position = Following.position + Offset;
		}
	}

	internal void Follow(Transform transform) {
		Following = transform;
	}
}
