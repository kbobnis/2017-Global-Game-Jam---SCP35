﻿using UnityEngine;

public class LevelModel {

	public static readonly LevelModel Level1 = new LevelModel(new RoomModel[][] {
		new RoomModel[] { RoomModel.Corridor, RoomModel.Room0, RoomModel.Room0, RoomModel.Room0},
		new RoomModel[] { RoomModel.Corridor, RoomModel.Room0, RoomModel.Room0 },
		new RoomModel[] { RoomModel.Corridor, RoomModel.Room0 },
		new RoomModel[] { RoomModel.Corridor, RoomModel.Room0 },
	});

	public readonly RoomModel[][] Rooms;

	public LevelModel(RoomModel[][] rooms) {
		Rooms = rooms;
	}

	internal GameObject Generate() {
		GameObject levelObject = new GameObject("Level");
		int i = 0;
		foreach (RoomModel[] rooms in Rooms) {
			int j = 0;
			foreach (RoomModel room in rooms) {
				GameObject roomObject = room.Generate();
				roomObject.transform.SetParent(levelObject.transform);
				roomObject.transform.position = new Vector3(j * RoomModel.Width, 0, i * RoomModel.Height);
				j++;
			}
			i++;
		}
		return levelObject;
	}
}