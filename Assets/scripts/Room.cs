using System;
using UnityEngine;

public class Room {
	public const int Height = 9;
	public const int Width = 16;

	public static readonly Room Corridor = new Room("data/levels/Corridor0");
	public static readonly Room Room0 = new Room("data/levels/Start0");

	public readonly Data RoomData;

	[Serializable]
	public class Data {

		public Layer[] layers;

		[Serializable]
		public class Layer {
			public int[] data;
		}

		internal int GetTileAt(int i, int j) {
			return layers[0].data[i + j * Width];
		}
	}

	public Room(string roomJsonPath) {
		RoomData = JsonUtility.FromJson<Data>(Resources.Load<TextAsset>(roomJsonPath).text);

		if (RoomData == null) {
			throw new Exception("There is no prefab at path: " + roomJsonPath);
		}
	}

	public GameObject Generate() {
		GameObject room = new GameObject("Room");

		GameObject elements = new GameObject("Elements");
		elements.transform.SetParent(room.transform);
		for (int i = 0; i < Width; i++) {
			for (int j = 0; j < Height; j++) {
				Tile tile = Tile.FromTiled(RoomData.GetTileAt(i, j));
				if (tile != null) {
					GameObject tileGO = tile.Spawn();
					tileGO.transform.SetParent(elements.transform);
					tileGO.transform.position = new Vector3(i, 0, j);
				}
			}
		}

		GameObject floor = new GameObject("Floor");
		floor.transform.SetParent(room.transform);

		for (int i = 0; i < Width; i++) {
			for (int j = 0; j < Height; j++) {
				Tile tile = Tile.Floor;
				GameObject tileGO = tile.Spawn();
				tileGO.transform.SetParent(floor.transform);
				tileGO.transform.position = new Vector3(i, -1, j);
			}
		}

		return room;
	}
}