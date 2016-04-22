using UnityEngine;
using System.Collections;

public class zone : MonoBehaviour {

	private int counter;
	private int MaxCounter;

	// Use this for initialization
	void Start () {
		counter = 400;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(counter == 0){
			//Disable
			counter = 400;
			gameObject.active = false;
			zones.isTwoActive--;
		} 
		else {
			counter--;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
 	{
         Debug.Log("Something has entered this zone.");    
    }
}
