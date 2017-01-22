using UnityEngine;
using Controllers;

[RequireComponent(typeof(InGamePosition))]
public class PlayerActionHandler : MonoBehaviour {

	public float _cooldown = 100;

	internal void InitForGamepad(int playerNumber) {
		GamepadInputController pi = GetComponent<GamepadInputController>() ?? gameObject.AddComponent<GamepadInputController>();
		pi.InputSuffix = playerNumber.ToString();
		pi.OnActionClicked += OnAttack;
		pi.OnMoveAngleChanged += OnMoveAngleChanged;
		pi.OnRotateAngleChanged += OnRotateAngleChanged;
	}

	internal void InitForAI() {
		AIController pi = GetComponent<AIController>() ?? gameObject.AddComponent<AIController>();
		pi.OnTryToAttack += OnAttack;
	}

	private void Update() {
		_cooldown += Time.deltaTime;
	}

	private AgentStatsModel GetStats() {
		return GetComponent<StatsComponent>().Stats;
	}

	internal void OnAttack() {
		if (_cooldown > GetStats().Cooldown) {
			if (GetComponent<Animator>() != null) {
				GetComponent<Animator>().SetTrigger("attack");
			}

			foreach (GameObject o in Utility.GetVisibleCharacters(transform, GetStats().AttackRange, 90)) {
				if (o.GetComponent<StatsComponent>() != null) {
					AttackAnother(o);
					_cooldown = 0.0f;
					break;
				}
			}
		}
	}

	private GameObject GetWeapon() {
		return transform.GetChild(1).gameObject;
	}

	public void AttackAnother(GameObject whom) {
		whom.GetComponent<StatsComponent>().Damage();
	}

	internal void OnMoveAngleChanged(Vector2 angle) {
		GetComponent<InGamePosition>().MoveBy(angle, InGamePosition.OneStep);
	}

	internal void OnRotateAngleChanged(Vector2 angle) {
		//zeby postac tylem nie chodzila.
		angle *= -1;
		GetComponent<InGamePosition>().SetRotation( angle);
	}

}

