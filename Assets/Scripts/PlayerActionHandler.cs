using UnityEngine;
using Controllers;

[RequireComponent(typeof(PlayerController), typeof(InGamePosition))]
public class PlayerActionHandler : MonoBehaviour {

	internal void Init(int playerNumber) {
		PlayerController pi = GetComponent<PlayerController>() ?? gameObject.AddComponent<PlayerController>();
		pi.InputSuffix = playerNumber.ToString();
		pi.OnActionClicked += OnActionClicked;
		pi.OnMoveAngleChanged += OnMoveAngleChanged;
		pi.OnRotateAngleChanged += OnRotateAngleChanged;
	}

	internal void OnActionClicked() {
		Debug.Log("On action clicked in player mover");
	}

	internal void OnMoveAngleChanged(Vector2 angle) {
		GetComponent<InGamePosition>().MoveBy(angle, InGamePosition.OneStep);
	}

	internal void OnRotateAngleChanged(Vector2 angle) {
		GetComponent<InGamePosition>().SetRotation(angle);
	}
}

