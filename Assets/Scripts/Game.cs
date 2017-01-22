using UnityEngine;

public class Game : MonoBehaviour {

	private int PlayerCount;

	public void Start() {
		GameObject levelObject = LevelModel.Level1.Generate();

		GameObject prisonerGO = AgentTileModel.Prisoner.Spawn(levelObject.transform, new Vector3(11, 0, 1));
		prisonerGO.AddComponent<PlayerActionHandler>().Init(++PlayerCount);

		GameObject prisoner2GO = AgentTileModel.Mech.Spawn(levelObject.transform, new Vector3(11, 0, 2));
		prisoner2GO.AddComponent<PlayerActionHandler>().Init(++PlayerCount);

	}
}
