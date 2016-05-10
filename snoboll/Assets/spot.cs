using UnityEngine;
using System.Collections;

public class spot : MonoBehaviour {
	int count = 0;
	int blink = 0;
	Color[] farg={Color.red,Color.blue,Color.cyan,Color.green,Color.magenta,Color.yellow};
	[SerializeField]
	bool up = true;
	bool on=true;
	[SerializeField]
	Light light,plight;
	int i=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (count < 150 && up) {
			if (i < 5) {
				plight.color = farg [i];
				light.color = farg [i];
				i++;
			} else {
				i = 0;
			}
			transform.Rotate (0.1f, 0, 0); 
			transform.Rotate (0, 0.1f, 0); 
			count++;
		} else if (count < 150) {
			if (i < 5) {
				plight.color = farg [i];
				light.color = farg [i];
				i++;
			} else {
				i = 0;
			}
			transform.Rotate (-0.1f, 0, 0); 
			transform.Rotate (0, -0.1f, 0); 
			count++;
		} else {
			count = 0;
			up = !up;
		}
		if (blink > 25 && on) {
			plight.intensity = 0;
			light.intensity = 0;
			on = false;
			blink = 0;
		} else if (blink > 5 && !on) {
			plight.intensity = 8;
			light.intensity = 8;
			on = true;
			blink = 0;
		}else {
			blink++;
		}
	}
}
