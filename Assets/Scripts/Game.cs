using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour {

	private int PlayerCount;

	public static Game Instance;

	private void Awake() {
		Instance = this;
	}

	public void Start() {
		GameObject levelObject = Level.Level1.Generate();

		GameObject prisonerGO = Tile.Prisoner.Spawn();
		prisonerGO.AddComponent<PlayerActionHandler>().Init(++PlayerCount);
		prisonerGO.transform.position = new Vector3(11, 1, 0);

		GameObject prisoner2GO = Tile.Prisoner.Spawn();
		prisoner2GO.AddComponent<PlayerActionHandler>().Init(++PlayerCount);
		prisoner2GO.transform.position = new Vector3(11, 2, 0);
	}
}
