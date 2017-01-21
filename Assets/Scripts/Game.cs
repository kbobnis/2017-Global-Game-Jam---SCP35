using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using Characters.Concrete;
using Controllers;
using Structures;
using UnityEngine;

public class Game : MonoBehaviour
{
	public static Game Instance;

	public LevelManager LevelManager { get; private set; }
	public BodyManager BodyManager { get; private set; }
	public TileManager TileManager { get; private set; }
	public PlayerManager PlayerManager { get; private set; }

	public PlayerInput Player;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		} else
		{
			Destroy(gameObject);
		}
	}

	public void Start()
	{
		LevelManager = new LevelManager
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
			RoomFilenames = new string[]
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
		BodyManager = new BodyManager
		{
			BodyManifests = new Dictionary<string, Type>()
			{
				{"Prisoner0", typeof(Prisoner)}
			}
		};
		TileManager = new TileManager()
		{
			Tiles = new Dictionary<int, string>()
			{
				{-1, "Floor"},
				{0, "Wall"},
				{1, "Filler1"},
				{2, "Desk1.W"},
				{3, "Desk1.E"},
				{4, "Desk2.W"},
				{5, "Desk2.E"},
				{6, "Cabinet1"},
				{7, "Cabinet2"},
				{8, "Doors0"}
			}
		};
		PlayerManager = new PlayerManager();
		GameObject go = new GameObject("Level");
		LevelManager.Generate(go.transform);
		StartCoroutine(SpawnPlayer());
	}

	/// <summary>
	/// We need to spawn player async, after rooms have fixated their
	/// positions. See also: <see cref="LevelManager.FirstFrameHack"/>
	/// </summary>
	/// <returns></returns>
	public IEnumerator SpawnPlayer()
	{
		yield return new WaitForSeconds(0.2f);
		PlayerManager.SpawnPlayer(1, 0);
		PlayerManager.SpawnPlayer(1, 1);
		Camera.main.gameObject.AddComponent<CameraMan>();
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
