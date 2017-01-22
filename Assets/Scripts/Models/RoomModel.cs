using System;
using UnityEngine;

public class RoomModel {
	public const int Height = 9;
	public const int Width = 16;

	public static readonly RoomModel Corridor = new RoomModel("levels/Corridor0");
	public static readonly RoomModel Room0 = new RoomModel("levels/Start0");

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

	public RoomModel(string roomJsonPath) {
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
				TileModel tile = TileModel.FromTiled(RoomData.GetTileAt(i, j));
				if (tile != null) {
					GameObject tileGO = tile.Spawn(elements.transform, new Vector3(i, 0, j));
					if (tile == TileModel.Doors) {
						//tileGO.transform.Rotate()
					}
				}
			}
		}

		//public static readonly TileModel Floor = new TileModel(2, "prefabs/floor", true, false, false);
		GameObject go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/floor"));
		Rigidbody r = go.AddComponent<Rigidbody>();
		r.isKinematic = true;
		go.AddComponent<BoxCollider>();
		go.name = "Floor";
		go.transform.SetParent(room.transform);
		go.transform.position = new Vector3(Width / 2, -1.13f, Height / 2); //middle of the room

		return room;
	}
}