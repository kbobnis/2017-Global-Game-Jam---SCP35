using UnityEngine;
using System.Collections.Generic;
using Utilities;

namespace Controllers
{

	public enum Rotation
	{
		North, East, South, West
	}

	public class Spawner
	{
		private int PlayersSpawned = 0;
		public Dictionary<int, string> Tiles;

		public void SpawnPlayer(int x, int y)
		{
			Rect roomRect = Game.Instance.LevelManager.GetRoomRect(1, 1);
			GameObject go = Game.Instance.BodyManager.SpawnBody(
				"Prisoner0",
				new Vector3(roomRect.center.x, 0, roomRect.center.y));
			PlayerInput pi = Game.Instance.Player = go.AddComponent<PlayerInput>();
			PlayerActionHandler pm = go.AddComponent<PlayerActionHandler>();
			pi.OnActionClicked += pm.OnActionClicked;
			pi.OnMoveAngleChanged += pm.OnMoveAngleChanged;
			pi.OnRotateAngleChanged += pm.OnRotateAngleChanged;
			go.AddComponent<InGamePosition>();

			Game.Instance.Player.InputSuffix = (++PlayersSpawned).ToString();
		}

		public void SpawnTile(int index, Vector3 position, Rotation rotation, Transform parent)
		{
			UnityEngine.Object.Instantiate(
				Resources.Load<GameObject>("data/tiles/" + Tiles[index]),
				position,
				rotation.ToQuaternion(),
				parent);
		}

	}
}