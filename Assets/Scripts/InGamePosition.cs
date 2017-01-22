using UnityEngine;

public class InGamePosition : MonoBehaviour {

	public const float OneStep = 0.1f;

	internal void MoveBy(Vector2 vector, float oneStep) {
		oneStep *= GetComponent<StatsComponent>().Stats.Speed;
		transform.localPosition += new Vector3(vector.x * oneStep , 0, vector.y * oneStep);
	}

	internal void SetRotation(Vector2 vector) {
		float angle = Mathf.Atan2(vector.x, vector.y);
		transform.rotation = Quaternion.Euler(0, angle * 180 / Mathf.PI, 0);
	}
}

