using Controllers;
using Models;
using UnityEngine;

public class Game : MonoBehaviour {

	private int PlayerCount;

	public void Start() {
		GameObject levelObject = LevelModel.Level1.Generate();

		GameObject prisonerGO = AgentTileModel.Prisoner.Spawn(levelObject.transform, new Vector3(11, 0, 35));
		prisonerGO.AddComponent<PlayerActionHandler>().InitForGamepad(++PlayerCount);
		prisonerGO.name = "Player 1";
		Camera.main.transform.gameObject.AddComponent<CameraMan>().Follow(prisonerGO.transform);

		GameObject prisoner2GO = AgentTileModel.Mech.Spawn(levelObject.transform, new Vector3(11, 0, 34));
		prisoner2GO.AddComponent<PlayerActionHandler>().InitForGamepad(++PlayerCount);
		prisoner2GO.name = "Player 2";
		prisoner2GO.AddComponent<AudioListener>();

		GameObject go = levelObject.transform.GetChild(0).gameObject;
		go.GetComponent<RoomComponent>().UnravelRoom();
	}
}
