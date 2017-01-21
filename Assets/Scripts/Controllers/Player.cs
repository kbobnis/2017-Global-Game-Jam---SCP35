using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public string inputSuffix;

	void Update()
	{

		if (MoonzInput.GetKeyDown(MoonzInput.RB, inputSuffix)) 
		{
			Debug.Log("RB ");
		}

		float h = MoonzInput.GetAxis("H", inputSuffix);
		float v = MoonzInput.GetAxis("V", inputSuffix);

		float angle;
		if (Mathf.Abs(h) + Mathf.Abs(v) > 0.5f)
		{
			angle = Mathf.Atan2(h, v);
			Quaternion rotation = Quaternion.Euler(0, angle * 180 / Mathf.PI, 0);
			Debug.Log("Angle " + angle + " rotation: " + rotation);
		}

		float fh = MoonzInput.GetAxis("FH", inputSuffix);
		float fv = MoonzInput.GetAxis("FV", inputSuffix);
		angle = Mathf.Atan2(fh, fv);

		if (Mathf.Abs(fh) + Mathf.Abs(fv) > 0.5)
		{
			Vector3 shootDirection = Camera.main.transform.up * fv + Camera.main.transform.right * fh;
			Quaternion rotation = Quaternion.Euler(0, angle * 180 / Mathf.PI, 0);
			Debug.Log("direction " + shootDirection + ", rotation: " + rotation);
		}

		if (MoonzInput.GetKeyDown(MoonzInput.RB, inputSuffix) || MoonzInput.GetKeyDown(MoonzInput.X, inputSuffix))
		{
			Debug.Log("RB");
		}
	}
}
