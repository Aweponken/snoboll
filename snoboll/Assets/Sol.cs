﻿using UnityEngine;
using System.Collections;

public class Sol : MonoBehaviour {
	Light light;
	float diff;
	float start = 430;
	float stop = 300;
	float angle;

	void Start () {
		light = GetComponent<Light> ();
		diff = GameWideScript.Instance.setTime - 2014;
		diff = 130 / diff;
	}

	void FixedUpdate () {
		light.transform.RotateAround (light.transform.position,new Vector3(0,1,0),diff * Time.deltaTime);
		print (light.transform.position.y);
	}
}