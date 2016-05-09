using UnityEngine;
using System.Collections;

public class spot : MonoBehaviour {
	int count = 0;
	int blink = 0;
	[SerializeField]
	bool up = true;
	bool on=true;
	[SerializeField]
	Light light;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (count < 150 && up) {
			transform.Rotate (0.1f, 0, 0); 
			transform.Rotate (0, 0.1f, 0); 
			count++;
		} else if (count < 150) {
			transform.Rotate (-0.1f, 0, 0); 
			transform.Rotate (0, -0.1f, 0); 
			count++;
		} else {
			count = 0;
			up = !up;
		}
		if (blink > 25 && on) {
			light.intensity = 0;
			on = false;
			blink = 0;
		} else if (blink > 5 && !on) {
			light.intensity = 8;
			on = true;
			blink = 0;
		}else {
			blink++;
		}
	}
}
