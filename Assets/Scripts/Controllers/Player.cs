using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public string InputSuffix;

	public Action<Vector2> OnMoveAngleChanged;
	public Action<Vector2> OnRotateAngleChanged;
	public Action OnActionClicked;

	private float? GetAngle(float h, float v)
	{
		float? angle = null;
		if (Mathf.Abs(h) + Mathf.Abs(v) > 0.5f)
		{
			angle = Mathf.Atan2(h, v);
			if (angle < 0)
			{
				angle = angle + 2 * Mathf.PI;
			}
			angle /= 2 * Mathf.PI;
		}
		return angle;
	}

	void Update()
	{

		if (MoonzInput.GetKeyDown(MoonzInput.RB, InputSuffix))
		{
			Debug.Log("RB ");
			if (OnActionClicked != null)
			{
				OnActionClicked.Invoke();
			}
		}

		Vector2 angle = new Vector2(MoonzInput.GetAxis("H", InputSuffix), MoonzInput.GetAxis("V", InputSuffix));
		if (Mathf.Abs(angle.x) + Mathf.Abs(angle.y) > 0.5f)
		{
			Debug.Log("Angle " + angle);
			if (OnMoveAngleChanged != null)
			{
				OnMoveAngleChanged.Invoke(angle);
			}
		}

		Vector2 angle2 = new Vector2(MoonzInput.GetAxis("FH", InputSuffix), MoonzInput.GetAxis("FV", InputSuffix));
		if (Mathf.Abs(angle2.x) + Mathf.Abs(angle2.y) > 0.5f)
		{
			Debug.Log("angle2: " + angle2);
			if (OnRotateAngleChanged != null)
			{
				OnRotateAngleChanged.Invoke(angle2);
			}
		}
	}
}
