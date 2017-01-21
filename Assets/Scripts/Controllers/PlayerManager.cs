using UnityEngine;

namespace Controllers
{
	public class PlayerManager
	{
		public void SpawnPlayer(int x, int y)
		{
			Rect roomRect = Game.Instance.LevelManager.GetRoomRect(1, 1);
			GameObject go = Game.Instance.BodyManager.SpawnBody("Prisoner0", roomRect.center);
			Game.Instance.Player = go.AddComponent<Player>();
		}
	}
}