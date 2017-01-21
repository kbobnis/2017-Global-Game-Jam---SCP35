using UnityEngine;
using Utilities;

namespace Controllers
{
	public class CameraMan : MonoBehaviour
	{

		public Vector3 CameraOffset = new Vector3(-.5f, 10f, 2f);

		private void Update()
		{
			Rect rect = Game.Instance.Player.GetCurrentRoomRect();
			gameObject.transform.position = new Vector3(rect.center.x, 0, rect.center.y) + CameraOffset;
		}
	}
}