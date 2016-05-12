﻿//750 -> -750

using UnityEngine;
using System.Collections;

public class PlaneMovement : MonoBehaviour {
	GameObject p;
	float diff;
	int y = 76;
	int z = 716;

	// Use this for initialization
	void Start () {
		p = GameObject.FindWithTag ("Plane");
		p.transform.position = new Vector3 (750, y, z);
		diff = GameWideScript.Instance.setTime - 2014;
		print (diff);
		diff = 1500 / diff;
		diff = diff / 60;
		print (diff);
	}
	
	// Update is called once per frame
	void FixedUpdate () {		
		p.transform.position = new Vector3 (p.transform.position.x - diff, y, z);
	}
}
