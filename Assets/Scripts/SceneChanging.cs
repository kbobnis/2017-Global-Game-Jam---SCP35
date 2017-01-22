using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanging : MonoBehaviour {
	//public float changeDelay = 1.0;


	private IEnumerator SceneChange(float delay)
	{
		float fadeTime = GameObject.Find ("sceneSwitcher").GetComponent<SceneFading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel ("Main");
	}

	void OnGUI()
	{
		if (Input.anyKey) {
			StartCoroutine (SceneChange (1f));

		}
	}
}
