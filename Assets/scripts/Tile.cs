using System;
using UnityEngine;

public class Tile {
	public static readonly Tile Floor = new Tile(2, "data/tiles/Floor", true);
	public static readonly Tile Wall = new Tile(3, "data/tiles/Filler1", true);
	public static readonly Tile Doors = new Tile(4, "data/tiles/Doors0", true);
	public static readonly Tile Prisoner = new Tile(5, "data/bodies/Prisoner0", false);

	internal GameObject Spawn() {
		GameObject go = GameObject.Instantiate<GameObject>(Prefab);
		Rigidbody r = go.AddComponent<Rigidbody>();
		r.constraints = RigidbodyConstraints.FreezeRotation;
		r.isKinematic = ShouldBeKinematic;
		go.AddComponent<Collider>();
		return go;
	}

	public static Tile[] All = new Tile[]{
		Floor, Wall, Doors, Prisoner
	};

	public readonly bool ShouldBeKinematic;
	private readonly int TiledValue;
	public readonly GameObject Prefab;

	public Tile(int tiledValue, string prefabPath, bool shouldBeKinematic) {
		ShouldBeKinematic = shouldBeKinematic;
		TiledValue = tiledValue;
		Prefab = Resources.Load<GameObject>(prefabPath);
		if (Prefab == null) {
			throw new Exception("There is no prefab at path: " + prefabPath);
		}
	}

	internal static Tile FromTiled(int v) {
		if (v == 0) {
			return null;
		}
		foreach (Tile t in All) {
			if (t.TiledValue == v) {
				return t;
			}
		}
		throw new Exception("There is no tile with tiled value " + v);
	}
}