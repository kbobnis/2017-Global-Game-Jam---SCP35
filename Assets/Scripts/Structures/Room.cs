﻿using System;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Structures
{
	[Serializable]
	public class Layer
	{
		public int[] data;

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			for(int i = 0; i < data.Length - 1; i++)
			{
				sb.Append(data[i]);
				sb.Append(", ");
			}
			sb.Append(data.Last());
			sb.Append(" }");
			return sb.ToString();
		}
	}

	[Serializable]
	public class Room
	{
		public const int Height = 9;
		public const int Width = 16;



		public Layer[] layers;

		public void Generate(Transform parent)
		{
			GameObject room = new GameObject("RoomObjects");
			Transform roomTransform = room.transform;
			roomTransform.SetParent(parent);
			GameObject floor = new GameObject("Floor");
			Transform floorTransform = floor.transform;
			floorTransform.SetParent(parent);

			for(int i = 0; i < Width; i++)
			{
				for(int j = 0; j < Height; j++)
				{
					int n = layers[0].data[i + j * Width];
					if(n > 0)
					{
						Game.Instance.TileManager.SpawnTile(n-1, new Vector3(i, 0, j), Rotation.North, roomTransform);
					}
					{
						Game.Instance.TileManager.SpawnTile(-1, new Vector3(i, -1, j), Rotation.North, floorTransform);
					}
				}
			}
		}
	}
}