using System.Collections;
using Structures;
using UnityEngine;

public class Game : MonoBehaviour
{
	public static Game Instance;

	public LevelData Data { get; private set; }
	public GameObject[] Tiles;
	public GameObject Floor;
	public GameObject[] Bullets;

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
				new int[] {0, 3, 5},
				new int[] {0, 6},
				new int[] {0, 7, 3, 5},
				new int[] {0, 1, 6},
				new int[] {0, 7, 8},
				new int[] {0, 9, 10, 4, 2}
			},
			RoomNames = new string[]
			{
				"data/levels/Corridor0", // 0
				"data/levels/Start0",    // 1
				"data/levels/Boss0",     // 2
				"data/levels/Room0",     // 3
				"data/levels/Room1",     // 4
				"data/levels/Room2",     // 5
				"data/levels/Room3",     // 6
				"data/levels/Room4",     // 7
				"data/levels/Room5",     // 8
				"data/levels/Room6",     // 9
				"data/levels/Room7"      // 10
			}
		};
		GameObject go = new GameObject("Level");
		Data.Generate(go.transform);
	}

	public static void StartAsync(IEnumerator coroutine)
	{
		Game.Instance.StartCoroutine(coroutine);
	}
	public static void Quit()
	{
	#if UNITY_EDITOR
		Debug.Break();
	#else
		Application.Quit();
	#endif
	}
}
