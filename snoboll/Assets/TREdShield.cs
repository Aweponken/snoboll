using UnityEngine;
using System.Collections;

public class TREdShield : MonoBehaviour {
	public GameObject sköld;
	// Use this for initialization
	void Start () {
		
		 sköld = GameObject.Find("PowerUp_Shield");
		sköld.active = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		//transform.position = new Vector3(sköld.transform.position.x, gameObject.transform.position.y-1,-100);
		//transform.Translate(Vector3.up * Time.deltaTime*10);

		if (sköld.active) {
			Debug.Log (sköld.transform.position.x);
			transform.position = new Vector2 (sköld.transform.position.x, sköld.transform.position.y);
		}
	}
}
