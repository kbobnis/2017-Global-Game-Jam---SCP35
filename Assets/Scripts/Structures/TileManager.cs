using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Structures
{
	public enum Rotation
	{
		North, East, West, South
	}

	public class TileManager
	{
		public Dictionary<int, string> Tiles;

		public void SpawnTile(int index, Vector3 position, Rotation rotation, Transform parent)
		{
			Object.Instantiate(
				Resources.Load<GameObject>("data/tiles/" + Tiles[index]),
				position,
				rotation.ToQuaternion(),
				parent);
		}
	}
}