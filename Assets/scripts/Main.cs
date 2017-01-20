using Structures;
using UnityEngine;

public class Main : MonoBehaviour
{
	public LevelData Data { get; private set; }

	public void Start()
	{
		Data = JsonUtility.FromJson<LevelData>("data/levelData.json");
	}

}
