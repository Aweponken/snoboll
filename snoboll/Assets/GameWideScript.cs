using UnityEngine;
using System.Collections;
/// <summary>
/// Script to hold Game wide global variables
/// </summary>
public class GameWideScript : MonoBehaviour {

	/// <summary>
	/// The instance
	/// </summary>
	public static GameWideScript Instance;
	/// <summary>
	/// Boolean that is set to true if custom settings are set
	/// </summary>
	public bool setCostum;
	/// <summary>
	/// Boolean to toggle sound
	/// </summary>
	public bool setSound;
	/// <summary>
	/// Boolean to toggle powerups 
	/// </summary>
	public bool setKrymp=true;
	public bool setPow;
	/// <summary>
	/// Boolean to toggle zones
	/// </summary>
	public bool setZone;
	/// <summary>
	/// Sets the length of the game in 'years'
	/// </summary>
	public int setTime;
	/// <summary>
	/// Sets number of players
	/// </summary>
	public int setPlayers;
	/// <summary>
	/// Sets map
	/// </summary>
	public int setMap;
	public bool EndOfGame;

	void Awake(){
		EndOfGame = false;
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
			Instance.setCostum = false;
			Instance.setSound = true;
			Instance.setKrymp = true;
			Instance.setPow = true;
			Instance.setZone = true;
			Instance.setTime = 210;
			Instance.setPlayers = 2;
			Instance.setMap = 1;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}
	/// <summary>
	/// object to handle player 1
	/// </summary>
	public static Player Player1 = new Player ("Player1", 0);
	/// <summary>
	/// object to handle player 2
	/// </summary>
	public static Player Player2 = new Player ("Player2", 0);
	/// <summary>
	/// object to handle player 3
	/// </summary>
	public static Player Player3 = new Player ("Player3", 0);
	/// <summary>
	/// object to handle player 4
	/// </summary>
	public static Player Player4 = new Player ("Player4", 0);

	/// <summary>
	/// Class to create the objects that handles the players
	/// </summary>
	public class Player {
		/// <summary>
		/// handles the players name 
		/// --- visable on the scoreboard ---
		/// </summary>
		public string name;
		/// <summary>
		/// handles the players size
		/// --- used as score ---
		/// </summary>
		public float size;
		/// <summary>
		/// toggles the player on/off depending on settings
		/// </summary>
		public bool isActive;
		/// <summary>
		/// Set to represent the player on the scoreboard
		/// --- easier to know who won ---
		/// </summary>
		public string Color;

		/// <summary>
		/// Initializes a new instance of the Player class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="size">Size.</param>
		/// <param name="isActive">If set to <c>true</c> is active.</param>
		public Player(string name, float size, bool isActive = true) {
			this.name = name;
			this.size = size;
			this.isActive = isActive;
		}
	}
}
