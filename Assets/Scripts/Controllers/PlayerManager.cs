using UnityEngine;

namespace Controllers
{
	public class PlayerManager
	{
		public void SpawnPlayer(int x, int y)
		{
			Rect roomRect = Game.Instance.LevelManager.GetRoomRect(1, 1);
			GameObject go = Game.Instance.BodyManager.SpawnBody(
				"Prisoner0",
				new Vector3(roomRect.center.x, 0, roomRect.center.y));
			Game.Instance.Player = go.AddComponent<Player>();
			Game.Instance.Player.inputSuffix = "1";
		}
	}
}