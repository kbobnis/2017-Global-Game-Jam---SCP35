using UnityEngine;

public class CameraMan : MonoBehaviour {

	public Vector3 Offset = new Vector3(7, 7.5f, 0.49f);

	void Awake() {
		transform.position = Offset;
	}
}
