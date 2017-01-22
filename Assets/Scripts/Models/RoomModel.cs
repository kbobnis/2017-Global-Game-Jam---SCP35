using System;
using UnityEngine;

public class RoomModel {
	public const int Height = 9;
	public const int Width = 16;

	public static readonly RoomModel Corridor0 = new RoomModel("levels/Corridor0", "prefabs/floor1");
	public static readonly RoomModel Corridor1 = new RoomModel("levels/Corridor0", "prefabs/floor");
	public static readonly RoomModel Room0 = new RoomModel("levels/Start0", "prefabs/floorDefault");

	public readonly Data RoomData;
	public GameObject FloorPrefab;

	public RoomModel(string roomJsonPath, string floorPrefab) {
		RoomData = JsonUtility.FromJson<Data>(Resources.Load<TextAsset>(roomJsonPath).text);
		FloorPrefab = Resources.Load<GameObject>(floorPrefab);

		if (FloorPrefab == null) {
			throw new Exception("There is no prefab at path: " + floorPrefab);
		}

		if (RoomData == null) {
			throw new Exception("There is no prefab at path: " + roomJsonPath);
		}
	}

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

	public enum Side {
		North, East, South, West
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

					//we will rotate doors by the corners
					if (tile == ObstacleTileModel.Doors) {
						int angle = 180;
						if (i == Width - 1) {
							angle = 0;
						}
						if (j == 0) {
							angle = 90;
						}
						if (j == Height - 1) {
							angle = 270;
						}
						Debug.Log("angle: " + angle);
						//transform.rotation = Quaternion.Euler(0, angle * 180 / Mathf.PI, 0);
						tileGO.transform.rotation = Quaternion.Euler(0, angle , 0);
					}
				}
			}
		}

		GameObject go = GameObject.Instantiate<GameObject>(FloorPrefab);
		Rigidbody r = go.AddComponent<Rigidbody>();
		r.isKinematic = true;
		go.AddComponent<BoxCollider>();
		go.name = "Floor";
		go.transform.SetParent(room.transform);
		go.transform.position = new Vector3(Width / 2, -1.13f, Height / 2); //middle of the room

		return room;
	}
}