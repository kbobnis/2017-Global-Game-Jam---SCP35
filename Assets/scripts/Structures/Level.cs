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
	public class Level
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
						Main.Instance.Tiles[layers[0].data[i + j * Width]],
						new Vector2(Width, Height),
						Quaternion.identity,
						parent);
				}
			}
		}
	}
}