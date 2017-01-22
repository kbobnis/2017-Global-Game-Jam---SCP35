using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class TileModel {

	public static readonly TileModel Wall = new TileModel(3, "prefabs/wall", NavType.Obstacle);
	public static readonly TileModel Doors = new TileModel(4, "prefabs/door", NavType.Obstacle);
	public static readonly TileModel Prisoner = new TileModel(5, "prefabs/enemy", NavType.Agent);

	public enum NavType {
		Agent, Obstacle
	}

	//levelObject.transform, new Vector3(11, 0, 2)
	internal GameObject Spawn(Transform parent, Vector3 pos) {
		GameObject go = GameObject.Instantiate<GameObject>(Prefab, pos, Quaternion.identity, parent);
		Rigidbody r = go.AddComponent<Rigidbody>();
		r.constraints = RigidbodyConstraints.FreezeRotation;
		r.isKinematic = _NavType == NavType.Obstacle;
		go.AddComponent<BoxCollider>();
		if (_NavType == NavType.Agent) {
			go.AddComponent<NavMeshAgent>();
		}
		if (_NavType == NavType.Obstacle) {
			go.AddComponent<NavMeshObstacle>();
		}
		return go;
	}

	public static TileModel[] All = new TileModel[]{
		Wall, Doors, Prisoner
	};

	private readonly int TiledValue;
	public readonly GameObject Prefab;
	public readonly NavType _NavType;

	public TileModel(int tiledValue, string prefabPath, NavType navType) {
		_NavType = navType;
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