using System;
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

		public void Generate(Transform parent)
		{
			for(int i = 0; i < Width; i++)
			{
				for(int j = 0; j < Height; j++)
				{
					GameObject go = new GameObject("Room [" + Width + ", " + Height + "]");
					go.transform.SetParent(parent);
					go.transform.position = new Vector3(i * Width, j * Height);
					int n = Level[i][j];
					if(!Rooms.ContainsKey(n))
					{
						Rooms[n] = JsonUtility.FromJson<Room>(RoomNames[n]);
					}
					Rooms[n].Generate(go.transform);
				}
			}
		}
	}
}