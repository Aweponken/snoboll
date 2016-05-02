using UnityEngine;
using System.Collections;
/// <summary>
/// Script handeling number of players
/// </summary>
public class PlayersOption : MonoBehaviour {

	// Use this for initialization
	void Start () {
		switch(GameWideScript.Instance.setPlayers){
		case 2:
			GameObject.Find ("Boll 3").SetActive(false);
			GameWideScript.Player3.isActive = false;
			GameObject.Find ("Boll 4").SetActive(false);
			GameWideScript.Player4.isActive = false;
			break;
		case 3:
			GameObject.Find ("Boll 4").SetActive(false);
			GameWideScript.Player4.isActive = false;
			break;
	}

	}
}
