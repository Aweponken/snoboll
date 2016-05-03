using UnityEngine;
using System.Collections;

public class SoundFXController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioSource FX = GetComponent<AudioSource>(); 
	}

	/*void OnCollisionEnter2D(Collider coll) {
		if (coll.tag == "Boll")
			print ("AJAJ");
	}*/
}
