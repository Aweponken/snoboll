﻿using UnityEngine;
using System.Collections;

public class GameWideScript : MonoBehaviour {

	public static GameWideScript Instance;
	public bool setCostum;
	public bool setSound;
	public bool setPow;
	public bool setZone;
	public int setTime;
	public int setPlayers;
	public int setMap;

	void Awake(){
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
			Instance.setCostum = false;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	public static Player Player1 = new Player ("Player1", 0);
	public static Player Player2 = new Player ("Player2", 0);
	public static Player Player3 = new Player ("Player3", 0);
	public static Player Player4 = new Player ("Player4", 0);

	public class Player {

		public string name;
		public float size;

		public Player(string name, float size) {
			this.name = name;
			this.size = size;
		}
	}
}
