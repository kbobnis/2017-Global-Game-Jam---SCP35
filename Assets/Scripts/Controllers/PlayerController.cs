using UnityEngine;

namespace Controllers
{
	public class PlayerController : MonoBehaviour
	{
		public delegate void OnPlayerRoomEnterAction(int x, int y);
		public delegate void OnPlayerRoomExitAction(int x, int y);

		public event OnPlayerRoomEnterAction OnPlayerRoomEnter;
		public event OnPlayerRoomExitAction OnPlayerRoomExit;

		internal void OnActionClicked() {
			Debug.Log("On action clicked in player mover");
		}

		internal void OnMoveAngleChanged(Vector2 angle) {
			GetComponent<InGamePosition>().MoveBy(angle, InGamePosition.OneStep);
		}

		internal void OnRotateAngleChanged(Vector2 angle) {
			GetComponent<InGamePosition>().SetRotation(angle);
		}

		public void PlayerRoomEnter(int x, int y) {
			var handler = OnPlayerRoomEnter;
			if(handler != null) handler(x, y);
		}

		public void PlayerRoomExit(int x, int y) {
			var handler = OnPlayerRoomExit;
			if(handler != null) handler(x, y);
		}
	}
}

