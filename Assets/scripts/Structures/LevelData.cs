using System;
using UnityEngine;

namespace Structures
{
	[Serializable]
	public class LevelData
	{
		public const int Width = 16;
		public const int Height = 9;

		public string[] RoomNames;
		public int[,] Floor;

		public void Generate(Transform parent)
		{
			for(int i = 0; i < Width; i++)
			{
				for(int j = 0; j < Height; j++)
				{
					GameObject go = new GameObject("Room [" + Width + ", " + Height + "]");

				}
			}
		}
	}
}