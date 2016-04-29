using UnityEngine;
using System.Collections;

public class PlayersOption : MonoBehaviour {

	// Use this for initialization
	void Start () {
		switch(GameWideScript.Instance.setPlayers){
		case 2:
			GameObject.Find ("Boll 3").active = false;
			GameWideScript.Player3.isActive = false;
			GameObject.Find ("Boll 4").active = false;
			GameWideScript.Player4.isActive = false;
			break;
		case 3:
			GameObject.Find ("Boll 4").active = false;
			GameWideScript.Player4.isActive = false;
			break;
	}

	}
}
