using UnityEngine;
using System.Collections.Generic;
using Utilities;
using System;
using Characters.Abstract;
using Characters;
using Structures;

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

		public GameObject SpawnPlayer(int x, int y)
		{
			Rect roomRect = Game.Instance.LevelManager.GetRoomRect(1, 1);
			GameObject go = SpawnBody("Prisoner0", new Vector3(roomRect.center.x, 0, roomRect.center.y));
			BodyComponent bc = go.GetComponent<BodyComponent>();
			bc.Body.IsPlayerControlled = true;
			PlayerInput pi = Game.Instance.Player = go.AddComponent<PlayerInput>();
			PlayerController pm = go.AddComponent<PlayerController>();
			pi.OnActionClicked += pm.OnActionClicked;
			pi.OnMoveAngleChanged += pm.OnMoveAngleChanged;
			pi.OnRotateAngleChanged += pm.OnRotateAngleChanged;
			pm.OnPlayerRoomEnter += Game.Instance.LevelManager.OnRoomEnter;
			pm.OnPlayerRoomExit += Game.Instance.LevelManager.OnRoomExit;
			pm.OnPlayerRoomEnter += (int xx, int yy) => {
				bc.Room.X = xx;
				bc.Room.Y = yy;
			};
			go.AddComponent<InGamePosition>();

			Game.Instance.Player.InputSuffix = (++_playersSpawned).ToString();
			pm.PlayerRoomEnter(x, y);
			return go;
		}

		public GameObject SpawnBody(string name, Vector3 position) {
			int x = Mathf.FloorToInt(position.x / LevelManager.Width) * LevelManager.Width;
			int y = Mathf.FloorToInt(position.y / LevelManager.Height) * LevelManager.Height;
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
			bodyComponent.Body.Init(bodyComponent);
			bodyComponent.Room.X = x;
			bodyComponent.Room.Y = y;

			return go;
		}
		public GameObject SpawnTile(int index, Vector3 position, Rotation rotation, Transform parent)
		{
			return UnityEngine.Object.Instantiate(
				Resources.Load<GameObject>("data/tiles/" + Tiles[index]),
				position,
				rotation.ToQuaternion(),
				parent);
		}

	}
}