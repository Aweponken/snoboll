using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

	public Button startText;
	private CanvasRenderer flash;
	private float alpha;
	private bool alphaCheck;
	// Use this for initialization
	void Start (){
		alphaCheck = false;
		alpha = 1;
		startText = startText.GetComponent<Button>();
		startText.Select();	
		flash = startText.GetComponent<CanvasRenderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.anyKey)
			StartLevel();
		if (!alphaCheck && alpha > 0.4)
			alpha -= 0.015f;
		else if (!alphaCheck && alpha <= 0.4) {
			alphaCheck = !alphaCheck;
			alpha += 0.015f;
		} else if (alphaCheck && alpha < 1)
			alpha += 0.015f;
		else if (alphaCheck && alpha >= 1) {
			alphaCheck = !alphaCheck;
			alpha -= 0.015f;
		}
		
		flash.SetAlpha (alpha);

	}

	public void StartLevel() //this function will be used on our Play button
	{
		SceneManager.LoadScene("Options"); //this will load our first level from our build settings. "1" is the second scene in our game
	}
}
