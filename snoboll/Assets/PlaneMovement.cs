//750 -> -750

using UnityEngine;
using System.Collections;

public class PlaneMovement : MonoBehaviour {
	GameObject p;
	float diff;
	int y = 76;
	int z = 716;

	void Start () {
		p = GameObject.FindWithTag ("Plane");
		p.transform.position = new Vector3 (750, y, z);
		diff = GameWideScript.Instance.setTime - 2014;
		diff = 1100 / diff;
		diff = diff / 60;
	}

	void FixedUpdate () {		
		p.transform.position = new Vector3 (p.transform.position.x - diff, y, z);
	}
}