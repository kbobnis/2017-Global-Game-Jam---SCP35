using System;
using Controllers;
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
		room.AddComponent<RoomComponent>();

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
						tileGO.transform.rotation = Quaternion.Euler(0, angle , 0);
					}
					if(tile is AgentTileModel) {
						tileGO.AddComponent<AIController>();
					}
					if (tile == ObstacleTileModel.Doors) {
						tileGO.GetComponent<DoorController>().DoorsAreOpening += room.GetComponent<RoomComponent>().MyDoorIsOpening;
					}
				}
			}
		}

		GameObject floorGO = GameObject.Instantiate<GameObject>(FloorPrefab);
		Rigidbody r = floorGO.AddComponent<Rigidbody>();
		r.isKinematic = true;
		floorGO.AddComponent<BoxCollider>();
		floorGO.name = "Floor";
		floorGO.transform.SetParent(room.transform);
		floorGO.transform.position = new Vector3(7.5f, -1.13f, Height / 2f); //middle of the room

		GameObject ceilGO = GameObject.Instantiate<GameObject>(RoomModel.Room0.FloorPrefab);
		Rigidbody rCeil = ceilGO.AddComponent<Rigidbody>();
		rCeil.isKinematic = true;
		ceilGO.AddComponent<BoxCollider>();
		ceilGO.name = "Ceiling";
		ceilGO.transform.SetParent(room.transform);
		ceilGO.transform.position = new Vector3(7.5f, 2, Height / 2f); //middle of the room

		room.GetComponent<RoomComponent>().SetElements(floorGO, ceilGO, elements);

		return room;
	}
}