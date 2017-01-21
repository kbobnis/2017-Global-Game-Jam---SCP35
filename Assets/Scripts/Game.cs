using Structures;
using UnityEngine;

public class Game : MonoBehaviour
{
	public static Game Instance;

	public LevelData Data { get; private set; }
	public GameObject[] Tiles;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void Start()
	{
		Data = new LevelData
		{
			Level = new int[][]
			{
				new int[] {0, 1, 10},
				new int[] {0, 3, 4, 5},
				new int[] {0, 10},
				new int[] {0, 4, 6},
				new int[] {0, 7, 1, 3, 2},
				new int[] {0, 5, 8, 9, 10, 4, 2}
			},
			RoomNames = new string[]
			{
				"data/level/Corridor0.json",
				"data/level/Start0.json",
				"data/level/Boss0.json",
				"data/level/Room0.json",
				"data/level/Room1.json",
				"data/level/Room2.json",
				"data/level/Room3.json",
				"data/level/Room4.json",
				"data/level/Room5.json",
				"data/level/Room6.json",
				"data/level/Room7.json"
			}
		};
		GameObject go = new GameObject("Level");
		Data.Generate(go.transform);
	}

}
