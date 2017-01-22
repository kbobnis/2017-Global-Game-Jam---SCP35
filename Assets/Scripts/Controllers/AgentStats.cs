using UnityEngine;

namespace Controllers {
	public class StatsComponent : MonoBehaviour {
		public AgentStatsModel Stats;

		public AgentStatsModel Damage() {
			Sound.Play("sounds/death", 0.2f);
			Destroy(gameObject);
			return Stats;
		}
	}

	public class AgentStatsModel {

		public static readonly AgentStatsModel Prisoner = new AgentStatsModel(0.8f, 3f, 45f, 1.5f, 0.5f, 1f);
		public static readonly AgentStatsModel Mech = new AgentStatsModel(0.9f, 0.1f, 30f, 2f, 1f, 2f);

		public readonly float Speed;
		public readonly float Range;
		public readonly float Angle;
		public readonly float AttackRange;
		public readonly float Cooldown;
		public readonly float Delay;

		public AgentStatsModel(
			float speed,
			float range,
			float angle,
			float attackRange,
			float delay,
			float cooldown) {
			Speed = speed;
			Cooldown = cooldown;
			Range = range;
			Angle = angle;
			AttackRange = attackRange;
			Delay = delay;
		}

	}
}