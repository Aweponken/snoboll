using UnityEngine;
using System.Collections;

public class snobollController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		switch(GameWideScript.Instance.setPlayers){
			case 2: 
				GameObject.Find ("Boll 3").active = false;
				GameObject.Find ("Boll 4").active = false;
				break;
			case 3:
				GameObject.Find ("Boll 4").active = false;
				break;
		}
	}
}
