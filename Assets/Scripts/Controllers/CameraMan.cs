using UnityEngine;
using Utilities;

namespace Controllers
{
	public class CameraMan : MonoBehaviour
	{

		public Vector3 CameraOffset = new Vector3(7.5f, -2f, -10f);

		private void Update()
		{
			Rect rect = Game.Instance.Player.GetCurrentRoomRect();
			gameObject.transform.position = new Vector3(rect.position.x, rect.position.y) + CameraOffset;
		}
	}
}