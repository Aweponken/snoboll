using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	static private bool menuIsActive = true;
	AudioSource menu;
	AudioSource game;

	void Awake()
	{
		if (menuIsActive) {
			AudioSource[] audios = GetComponents<AudioSource>(); //Hämta alla ljudfiler
			menu = audios[0]; 
			game = audios[1];
            menu.volume = 0.15f;
            game.volume = 0.15f;
			menu.Play ();
			DontDestroyOnLoad (gameObject);
		} 
	}
	void Update () {
		if(Application.loadedLevelName == "Map" || Application.loadedLevelName == "Map2" || Application.loadedLevelName == "Map3" || Application.loadedLevelName == "Map4")
		{
			menu.loop = false;
			if (!menu.isPlaying  && menuIsActive) {
				game.loop = true;
				game.Play ();
				menuIsActive = false;
			}	
		}
		if(Application.loadedLevelName == "Options")
		{
			game.loop = false;
			if (!game.isPlaying && !menuIsActive) {
				menu.loop = true;
				menu.Play ();
				menuIsActive = true;
			}	
		} 
	}
}
