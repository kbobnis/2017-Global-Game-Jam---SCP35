using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class Utility {
	public static LinkedList<GameObject> GetVisibleCharacters(
		Transform viewer,
		float sightRange,
		float sightAngle) {
		Collider[] colliders = Physics.OverlapSphere(viewer.position, sightRange);
		LinkedList<GameObject> objects = new LinkedList<GameObject>();
		foreach(Collider col in colliders) {
			if(col.GetComponent<AIController>() == null && col.GetComponent<PlayerController>() == null) {
				continue;
			}
			Vector2 from = new Vector2(viewer.forward.x, viewer.forward.z);
			Transform colliderTransform = col.transform;
			Vector3 colliderPosition = colliderTransform.position;

			Vector2 to = new Vector2(
				colliderPosition.x - viewer.position.x,
				colliderPosition.z - viewer.position.z);

			Ray ray = new Ray(viewer.position, colliderPosition - viewer.position);
			RaycastHit raycastHit;
			if(Physics.Raycast(ray, out raycastHit, sightRange)) {
				if(raycastHit.transform != colliderTransform) {
					continue;
				}
			}
			if(Mathf.Abs(Vector2.Angle(from, to)) < sightAngle / 2) {
				objects.AddFirst(col.gameObject);
			}
		}
		return objects;
	}
}
