using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

namespace Structures
{
	public class LevelManager
	{
		public const int Width = 16;
		public const int Height = 9;

		public string[] RoomFilenames;
		public int[][] Level;
		public Dictionary<int, Room> Rooms;
		public Dictionary<string, GameObject> RoomObjects;

		public LevelManager()
		{
			Rooms = new Dictionary<int, Room>();
			RoomObjects = new Dictionary<string, GameObject>();
		}

		public void Generate(Transform parent)
		{
 			int i = 0;
			foreach(int[] lvlArr in Level)
			{
				int j = 0;
				foreach(int lvl in lvlArr)
				{
					GameObject go = new GameObject("Room [" + j + ", " + i + "]");
					go.transform.SetParent(parent);
					Game.StartAsync(FirstFrameHack(go, new Vector3(j * Width, 0,  i * Height)));
					if(!Rooms.ContainsKey(lvl))
					{
						string json = Resources.Load<TextAsset>(RoomFilenames[lvl]).text;
						Rooms[lvl] = JsonUtility.FromJson<Room>(json);
					}
					Rooms[lvl].Generate(go.transform);
					RoomObjects[j + ":" + i] = go;
					j++;
				}
				i++;
			}
		}

		public GameObject GetRoomAt(int x, int y)
		{
			return RoomObjects[x + ":" + y];
		}

		public Rect GetRoomRect(int x, int y)
		{
			return new Rect(GetRoomAt(x, y).transform.position, new Vector2(Width, Height));
		}

		/// <summary>
		/// Ok, so if I won't move GameObject to it's position async
		/// it stays in place despite changed position in transform
		/// component. Maybe on Windows this will have different behaviour,
		/// but I can't test it. Must stay this way.
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