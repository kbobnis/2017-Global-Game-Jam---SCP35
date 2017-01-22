using System;
using Interface;
using UnityEngine;

namespace Controllers {
	public class GamepadInputController : MonoBehaviour {
		public string InputSuffix;

		public Action<Vector2> OnMoveAngleChanged;
		public Action<Vector2> OnRotateAngleChanged;
		public Action OnActionClicked;

		void Update() {

			if (MoonzInput.GetKeyDown(MoonzInput.RB, InputSuffix)) {
				if (OnActionClicked != null) {
					OnActionClicked.Invoke();
				}
			}

			Vector2 angle = new Vector2(MoonzInput.GetAxis("H", InputSuffix), MoonzInput.GetAxis("V", InputSuffix));
			if (Mathf.Abs(angle.x) + Mathf.Abs(angle.y) > 0.5f) {
				if (OnMoveAngleChanged != null) {
					OnMoveAngleChanged.Invoke(angle);
				}
			}

			Vector2 angle2 = new Vector2(MoonzInput.GetAxis("FH", InputSuffix), MoonzInput.GetAxis("FV", InputSuffix));
			if (Mathf.Abs(angle2.x) + Mathf.Abs(angle2.y) > 0.5f) {
				if (OnRotateAngleChanged != null) {
					OnRotateAngleChanged.Invoke(angle2);
				}
			}
		}
	}
}
