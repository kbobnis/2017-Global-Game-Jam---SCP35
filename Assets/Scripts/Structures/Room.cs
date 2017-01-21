using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Structures
{
	[Serializable]
	public class Layer
	{
		public int[] data;
	}

	[Serializable]
	public class Room
	{
		public const int Height = 9;
		public const int Width = 16;

		public Layer[] layers;

		public void Generate(Transform parent)
		{
			for(int i = 0; i < Width; i++)
			{
				for(int j = 0; j < Height; j++)
				{
					Object.Instantiate(
						Game.Instance.Tiles[layers[0].data[i + j * Width] - 1],
						new Vector2(Width, Height),
						Quaternion.identity,
						parent);
				}
			}
		}
	}
}