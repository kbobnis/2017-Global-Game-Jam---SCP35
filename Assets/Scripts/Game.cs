using UnityEngine;

public class Game : MonoBehaviour {

	private int PlayerCount;
	public GameObject ActualRoom;

	public static Game Instance;

	private void Awake() {
		Instance = this;
	}

	public void Start() {
		GameObject levelObject = LevelModel.Level1.Generate();

		GameObject prisonerGO = TileModel.Prisoner.Spawn();
		prisonerGO.AddComponent<PlayerActionHandler>().Init(++PlayerCount);
		prisonerGO.transform.position = new Vector3(11, 1, 0);

		GameObject prisoner2GO = TileModel.Prisoner.Spawn();
		prisoner2GO.AddComponent<PlayerActionHandler>().Init(++PlayerCount);
		prisoner2GO.transform.position = new Vector3(11, 2, 0);

		Camera.main.gameObject.AddComponent<CameraMan>();
	}
}
