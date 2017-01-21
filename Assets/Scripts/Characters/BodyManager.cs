using System;
using System.Collections.Generic;
using Characters.Abstract;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Characters
{
	public class BodyManager
	{
		public Dictionary<string, AbstractBody> Bodies;
		public Dictionary<string, Type> BodyManifests;

		public BodyManager()
		{
			Bodies = new Dictionary<string, AbstractBody>();
		}

		public GameObject SpawnBody(string name, Vector3 position)
		{
			string fullName = "data/bodies/" + name;
			if(!Bodies.ContainsKey(name))
			{
				if(!BodyManifests.ContainsKey(name))
				{
					Game.Quit();
					throw new Exception("Unknown body type!");
				}
				string json = Resources.Load<TextAsset>(fullName + "Manifest").text;
				Bodies[name] = (AbstractBody)JsonUtility.FromJson(json, BodyManifests[name]);
			}
			GameObject go = Object.Instantiate(
				Resources.Load<GameObject>(fullName),
				position,
				Quaternion.identity);
			BodyComponent bodyComponent = go.GetComponent<BodyComponent>() ?? go.AddComponent<BodyComponent>();
			bodyComponent.Body = Bodies[name];
			return go;
		}
	}
}