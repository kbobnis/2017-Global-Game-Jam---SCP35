using UnityEngine;
using UnityEngine.AI;

public class Game : MonoBehaviour {

	private int PlayerCount;
	public GameObject ActualRoom;

	public static Game Instance;

	private void Awake() {
		Instance = this;
	}

	public void Start() {
		GameObject levelObject = LevelModel.Level1.Generate();
		levelObject.transform.position = new Vector3(0, 0, 0); //zeby nav mesh sie stykał

		GameObject prisonerGO = TileModel.Prisoner.Spawn(levelObject.transform, new Vector3(11, 0, 1));
		prisonerGO.AddComponent<PlayerActionHandler>().Init(++PlayerCount);

		GameObject prisoner2GO = TileModel.Prisoner.Spawn(levelObject.transform, new Vector3(11, 0, 2));
		prisoner2GO.AddComponent<PlayerActionHandler>().Init(++PlayerCount);

		UnityEditor.EditorApplication.isPaused = true;
	}

}
