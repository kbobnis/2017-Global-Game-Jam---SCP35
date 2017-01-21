using System.Runtime.InteropServices;
using Controllers;
using Structures;
using UnityEngine;

namespace Utilities
{
	public static class PlayerExtension
	{
		public static Rect GetCurrentRoomRect(this PlayerInput player)
		{
			Vector2 position = player.transform.position;
			return new Rect(
				Mathf.Floor(position.x / LevelManager.Width) * LevelManager.Width,
				Mathf.Floor(position.y / LevelManager.Height) * LevelManager.Height,
				LevelManager.Width,
				LevelManager.Height);
		}

		public static GameObject GetCurrentRoomObject(this PlayerInput player)
		{
			Vector2 position = player.transform.position;
			int x = Mathf.FloorToInt(position.x / LevelManager.Width);
			int y = Mathf.FloorToInt(position.y / LevelManager.Height);
			return Game.Instance.LevelManager.GetRoomAt(x, y);
		}
	}
}