using UnityEngine;

public class Level {

	public static readonly Level Level1 = new Level(new Room[][] {
		new Room[] { Room.Corridor, Room.Room0, Room.Room0, Room.Room0},
		new Room[] { Room.Corridor, Room.Room0, Room.Room0 },
		new Room[] { Room.Corridor, Room.Room0 },
		new Room[] { Room.Corridor, Room.Room0 },
	});

	public readonly Room[][] Rooms;

	public Level(Room[][] rooms) {
		Rooms = rooms;
	}

	internal GameObject Generate() {
		GameObject levelObject = new GameObject("Level");
		int i = 0;
		foreach (Room[] rooms in Rooms) {
			int j = 0;
			foreach (Room room in rooms) {
				GameObject roomObject = room.Generate();
				roomObject.transform.SetParent(levelObject.transform);
				roomObject.transform.position = new Vector3(j * Room.Width, 0, i * Room.Height);
				j++;
			}
			i++;
		}
		return levelObject;
	}
}