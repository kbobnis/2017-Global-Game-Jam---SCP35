using UnityEngine;
using System.Collections.Generic;
using Utilities;
using System;
using Characters.Abstract;
using Characters;

namespace Controllers
{

	public enum Rotation
	{
		North, East, South, West
	}

	public class Spawner {
		private int _playersSpawned = 0;
		public Dictionary<int, string> Tiles;
		public Dictionary<string, AbstractBody> Bodies = new Dictionary<string, AbstractBody>();
		public Dictionary<string, Type> BodyManifests;

		public void SpawnPlayer(int x, int y)
		{
			Rect roomRect = Game.Instance.LevelManager.GetRoomRect(1, 1);
			GameObject go = SpawnBody("Prisoner0", new Vector3(roomRect.center.x, 0, roomRect.center.y));
			PlayerInput pi = Game.Instance.Player = go.AddComponent<PlayerInput>();
			PlayerActionHandler pm = go.AddComponent<PlayerActionHandler>();
			pi.OnActionClicked += pm.OnActionClicked;
			pi.OnMoveAngleChanged += pm.OnMoveAngleChanged;
			pi.OnRotateAngleChanged += pm.OnRotateAngleChanged;
			go.AddComponent<InGamePosition>();

			Game.Instance.Player.InputSuffix = (++_playersSpawned).ToString();
		}

		public GameObject SpawnBody(string name, Vector3 position)
		{
			string fullName = "data/bodies/" + name;
			if (!Bodies.ContainsKey(name))
			{
				if (!BodyManifests.ContainsKey(name))
				{
					Game.Quit();
					throw new Exception("Unknown body type!");
				}
				string json = Resources.Load<TextAsset>(fullName + "Manifest").text;
				Bodies[name] = (AbstractBody)JsonUtility.FromJson(json, BodyManifests[name]);
			}
			GameObject go = UnityEngine.Object.Instantiate(
				Resources.Load<GameObject>(fullName),
				position,
				Quaternion.identity);
			BodyComponent bodyComponent = go.GetComponent<BodyComponent>() ?? go.AddComponent<BodyComponent>();
			go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
			bodyComponent.Body = Bodies[name];
			return go;
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