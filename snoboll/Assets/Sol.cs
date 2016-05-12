using UnityEngine;
using System.Collections;

public class Sol : MonoBehaviour {
	GameObject s;
	float diff;
	int x = 50;
	int z = 0;

	void Start () {
		s = GameObject.FindWithTag ("Sol");
		s.transform.localRotation = new Vector3 (x, -290, z);
		diff = GameWideScript.Instance.setTime - 2014;
		diff = 130 / diff;
		diff = diff / 60;
	}

	void FixedUpdate () {
		s.transform.localRotation = new Vector3 (x, s.transform.localRotation.y - diff, z);
	}
}
