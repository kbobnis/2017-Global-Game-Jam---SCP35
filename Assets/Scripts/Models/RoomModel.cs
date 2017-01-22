using Controllers;
using System;
using UnityEngine;

namespace Models {
	public class RoomModel {
		public const int Height = 9;
		public const int Width = 16;

		public static readonly RoomModel Corridor0 = new RoomModel("levels/Corridor0", "prefabs/floorDefault", false);
		public static readonly RoomModel Corridor1 = new RoomModel("levels/Corridor1", "prefabs/floorDefault", false);
		public static readonly RoomModel Room1 = new RoomModel("levels/Room1", "prefabs/floor1", true);
		public static readonly RoomModel Room2 = new RoomModel("levels/Room2", "prefabs/floor2", true);
		public static readonly RoomModel Room3 = new RoomModel("levels/Room3", "prefabs/floor3", true);
		public static readonly RoomModel Room4 = new RoomModel("levels/Room4", "prefabs/floor4", true);
		public static readonly RoomModel Room5 = new RoomModel("levels/Room5", "prefabs/floor5", true);
		public static readonly RoomModel RoomB = new RoomModel("levels/RoomB", "prefabs/floorB", true);

		public readonly Data RoomData;
		public GameObject FloorPrefab;
		public readonly bool HasCeil;

		public RoomModel(string roomJsonPath, string floorPrefab, bool hasCeil) {
			RoomData = JsonUtility.FromJson<Data>(Resources.Load<TextAsset>(roomJsonPath).text);
			FloorPrefab = Resources.Load<GameObject>(floorPrefab);
			HasCeil = hasCeil;

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
						room.AddComponent<RoomComponent>();
						//we will rotate doors by the corners
						if (tile == ObstacleTileModel.Doors) {
							int angle = 180;
							if (j == 0) {
								angle = 90;
							}
							if (j == Height - 1) {
								angle = 270;
							}
							if (i == Width - 1) {
								angle = 0;
							}
							tileGO.transform.rotation = Quaternion.Euler(0, angle , 0);
						}
						if (tile == ObstacleTileModel.Doors) {
							tileGO.GetComponent<DoorController>().DoorsAreOpening += room.GetComponent<RoomComponent>().MyDoorIsOpening;
						}
						if (tile is AgentTileModel) {
							AIController aic = tileGO.AddComponent<AIController>();
							tileGO.AddComponent<PlayerActionHandler>().InitForAI();
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
			floorGO.transform.position = new Vector3(7.5f, -1, Height / 2f); //middle of the room

			if (HasCeil) {
				GameObject ceilGO = GameObject.Instantiate<GameObject>(RoomModel.Corridor0.FloorPrefab);
				Rigidbody rCeil = ceilGO.AddComponent<Rigidbody>();
				rCeil.isKinematic = true;
				ceilGO.AddComponent<BoxCollider>();
				ceilGO.name = "Ceil";
				ceilGO.transform.SetParent(room.transform);
				ceilGO.transform.position = new Vector3(7.5f, 1.03f, Height / 2f); //middle of the room
				room.GetComponent<RoomComponent>().SetElements(floorGO, ceilGO, elements);
			}
			return room;
		}
	}
}