using System;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

public class TileModel {
	public static readonly TileModel Floor = new TileModel(2, "prefabs/floor", true, false, false);
	public static readonly TileModel Wall = new TileModel(3, "prefabs/wall", true, false, true);
	public static readonly TileModel Doors = new TileModel(4, "prefabs/door", true, false, true);
	public static readonly TileModel Prisoner = new TileModel(5, "prefabs/enemy", false, true,  false);

	internal GameObject Spawn() {
		GameObject go = Object.Instantiate(Prefab);
		Rigidbody r = go.AddComponent<Rigidbody>();
		r.constraints = RigidbodyConstraints.FreezeRotation;
		r.isKinematic = ShouldBeKinematic;
		go.AddComponent<BoxCollider>();
		if (ShouldBeAgent) {
			go.AddComponent<NavMeshAgent>();
		}
		if (ShouldBeObstacle) {
			go.AddComponent<NavMeshObstacle>();
		}
		return go;
	}

	public static TileModel[] All = new TileModel[]{
		Floor, Wall, Doors, Prisoner
	};

	public readonly bool ShouldBeKinematic;
	public readonly bool ShouldBeAgent;
	public readonly bool ShouldBeObstacle;
	private readonly int TiledValue;
	public readonly GameObject Prefab;

	public TileModel(int tiledValue, string prefabPath, bool shouldBeKinematic, bool isAgent, bool isObstacle) {
		ShouldBeKinematic = shouldBeKinematic;
		ShouldBeAgent = isAgent;
		ShouldBeObstacle = isObstacle;
		TiledValue = tiledValue;
		Prefab = Resources.Load<GameObject>(prefabPath);
		if (Prefab == null) {
			throw new Exception("There is no prefab at path: " + prefabPath);
		}
	}

	internal static TileModel FromTiled(int v) {
		if (v == 0) {
			return null;
		}
		foreach (TileModel t in All) {
			if (t.TiledValue == v) {
				return t;
			}
		}
		throw new Exception("There is no tile with tiled value " + v);
	}
}