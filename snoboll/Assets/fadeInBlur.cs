using UnityEngine;
using System.Collections;

public class fadeInBlur : MonoBehaviour {

	public static int count;
	public static bool OnOff;
	public float takeAway;
	// Use this for initialization
	void Start () {
		count = 0;
		OnOff = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (count < 100) {
			gameObject.GetComponent <UnityStandardAssets.ImageEffects.Blur> ().blurSpread += 0.008f;
			count++;
		}
	}
}
