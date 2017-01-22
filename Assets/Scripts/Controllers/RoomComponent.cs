using UnityEngine;

public class RoomComponent : MonoBehaviour {

	public GameObject FloorGO;
	public GameObject CeilGO;
	public GameObject ElementsGO;

	internal void Init(GameObject floorGO, GameObject ceilGO, GameObject elementsGO) {
		FloorGO = floorGO;
		CeilGO = ceilGO;
		ElementsGO = elementsGO;
	}

	internal void UnravelRoom() {
		float y = CeilGO.transform.position.y;
		gameObject.AddComponent<Changer>().Change(y, -2, 1.5f, (float actual) => {
			Vector3 pos = CeilGO.transform.position;
			CeilGO.transform.position = new Vector3(pos.x, actual, pos.z);
		});
	}
}
