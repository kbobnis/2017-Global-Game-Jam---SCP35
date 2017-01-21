using System;
using Structures;
using UnityEngine;
using Controllers;

namespace Utilities
{
	public static class RotationExtension
	{
		public static Quaternion ToQuaternion(this Rotation rotation)
		{
			switch(rotation)
			{
				case Rotation.North:
					return Quaternion.identity;
				case Rotation.East:
					return Quaternion.Euler(0, 0, -90);
				case Rotation.West:
					return Quaternion.Euler(0, 0, 90);
				case Rotation.South:
					return Quaternion.Euler(0, 0, 180);
				default:
					throw new ArgumentOutOfRangeException("rotation", rotation, null);
			}
		}
	}
}