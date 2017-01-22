
using UnityEngine;

public class StatsComponent : MonoBehaviour {
	public AgentStatsModel Stats;
}


public class AgentStatsModel {

	public static readonly AgentStatsModel Prisoner = new AgentStatsModel(1f);
	public static readonly AgentStatsModel Mech = new AgentStatsModel(2f);

	public readonly float Speed;

	public AgentStatsModel(float speed) {
		Speed = speed;
	}

}
