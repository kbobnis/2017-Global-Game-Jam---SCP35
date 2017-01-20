using System.ComponentModel;
using Structures;
using UnityEngine;

public class Main : MonoBehaviour
{
	public static Main Instance;

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
		Data = JsonUtility.FromJson<LevelData>("data/levelData.json");
	}

}
