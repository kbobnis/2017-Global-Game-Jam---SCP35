
using System;
using UnityEngine;

public class PlayerActionHandler : MonoBehaviour
{
	internal void OnActionClicked()
	{
		Debug.Log("On action clicked in player mover");
	}

	internal void OnMoveAngleChanged(Vector2 angle)
	{
		GetComponent<InGamePosition>().MoveBy(angle, InGamePosition.OneStep);
	}

	internal void OnRotateAngleChanged(Vector2 angle)
	{
		GetComponent<InGamePosition>().SetRotation(angle);
	}
}
