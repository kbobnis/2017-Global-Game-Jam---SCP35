using System;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

public class AgentTileModel : TileModel{

	public static readonly AgentTileModel Prisoner = new AgentTileModel(5, "prefabs/enemy", AgentStatsModel.Prisoner);
	public static readonly AgentTileModel Mech = new AgentTileModel(7, "prefabs/mech", AgentStatsModel.Mech);

	public readonly AgentStatsModel StatsModel;

	public AgentTileModel(int tiledValue, string prefabPath, AgentStatsModel stats) : base (tiledValue, prefabPath) {
		StatsModel = stats;
	}

	protected override void InnerSpawn(GameObject go) {
		NavMeshAgent nma = go.AddComponent<NavMeshAgent>();
		nma.baseOffset = 0;
		nma.height = 1;
		go.AddComponent<StatsComponent>().Stats = StatsModel;
	}
}

public class ObstacleTileModel : TileModel {

	public static readonly ObstacleTileModel Wall = new ObstacleTileModel(3, "prefabs/wall");
	public static readonly ObstacleTileModel Doors = new ObstacleTileModel(4, "prefabs/door");
	public static readonly ObstacleTileModel Vial = new ObstacleTileModel(6, "prefabs/vial");

	public ObstacleTileModel(int tiledValue, string prefabPath) : base(tiledValue, prefabPath) {
	}

	protected override void InnerSpawn(GameObject go) {
		go.GetComponent<Rigidbody>().isKinematic = true;
		go.AddComponent<NavMeshObstacle>();
	}

}

public abstract class TileModel {

	private readonly int TiledValue;
	public readonly GameObject Prefab;

	public TileModel(int tiledValue, string prefabPath) {
		TiledValue = tiledValue;
		Prefab = Resources.Load<GameObject>(prefabPath);
		if (Prefab == null) {
			throw new Exception("There is no prefab at path: " + prefabPath);
		}
	}

	protected abstract void InnerSpawn(GameObject go);

	internal GameObject Spawn(Transform parent, Vector3 pos) {
		GameObject go = Object.Instantiate<GameObject>(Prefab, pos, Quaternion.identity, parent);
		Rigidbody r = go.AddComponent<Rigidbody>();
		r.constraints = RigidbodyConstraints.FreezeAll;
		go.AddComponent<BoxCollider>();
		InnerSpawn(go);
		return go;
	}

	public static TileModel FromTiled( int v) {
		if (v == 0) {
			return null;
		}

		TileModel[] all = new TileModel[] {
			ObstacleTileModel.Doors,
			ObstacleTileModel.Vial,
			ObstacleTileModel.Wall,
			AgentTileModel.Prisoner,
			AgentTileModel.Mech,
		};

		foreach (TileModel t in all ) {
			if (t.TiledValue == v) {
				return t;
			}
		}
		throw new Exception("There is no tile with tiled value " + v);
	}
}