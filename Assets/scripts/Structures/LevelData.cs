using System;
using System.Security.Cryptography;
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

		public void Generate(GameObject parent)
		{
			for(int i = 0; i < Width; i++)
			{
				for(int j = 0; j < Height; j++)
				{ // Generate room
				}
			}
		}
	}
}