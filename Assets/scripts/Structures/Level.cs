using System;

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
	}
}