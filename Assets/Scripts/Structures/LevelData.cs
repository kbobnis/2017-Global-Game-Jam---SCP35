using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Structures
{
	[Serializable]
	public class LevelData
	{
		public const int Width = 16;
		public const int Height = 9;

		public string[] RoomNames;
		public int[][] Level;
		public Dictionary<int, Room> Rooms;

		public LevelData()
		{
			Rooms = new Dictionary<int, Room>();
		}

		public void Generate(Transform parent)
		{
			int i = 0;
			foreach(int[] lvlArr in Level)
			{
				int j = 0;
				foreach(int lvl in lvlArr)
				{
					GameObject go = new GameObject("Room [" + i + ", " + j + "]");
					go.transform.SetParent(parent);
					Game.StartAsync(FirstFrameHack(go, new Vector3(j * Width, i * Height)));
					if(!Rooms.ContainsKey(lvl))
					{
						string json = Resources.Load<TextAsset>(RoomNames[lvl]).text;
						Rooms[lvl] = JsonUtility.FromJson<Room>(json);
					}
					Rooms[lvl].Generate(go.transform);
					j++;
				}
				i++;
			}
		}

		/// <summary>
		/// Ok, so if I won't move go to it's position async
		/// it stays in place despite changed position in transform
		/// component. Maybe on Windows this will have different behaviour,
		/// but I can't test. Must stay this way.
		/// </summary>
		/// <param name="go">Game object to move</param>
		/// <param name="position">Position to move GameObject</param>
		/// <returns></returns>
		public IEnumerator FirstFrameHack(GameObject go, Vector3 position)
		{
			yield return new WaitForEndOfFrame();
			go.transform.position = position;
		}
	}
}