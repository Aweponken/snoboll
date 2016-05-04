using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// Script to handle the settings-menu
/// </summary>
public class menuScript : MonoBehaviour
{
	private bool Sound;
	private bool Pow;
	private bool Zone;
	private bool Shrink;
	private int Time;
	private int PowUpOcc;
	private int Players;
	private int Map;

	/// <summary>
	/// The button that starts the game
	/// </summary>
	private int PowUpDelay;
	public Button startText;
	/// <summary>
	/// The button that exits the game
	/// </summary>
	public Button exitText;
	/// <summary>
	/// The button that links to the help-canvas
	/// </summary>
	public Button HowToPlay;
	/// <summary>
	/// The button that closes the help-canvas
	/// </summary>
	public Button HowToPlayClose;
	/// <summary>
	/// The button that toggles sound
	/// </summary>
	public Button SoundButton;
	/// <summary>
	/// The button that toggles powerups
	/// </summary>
	public Button ShrinkButton;
	/// <summary>
	/// The Btton that toggles zones
	/// </summary>
	public Button ZonesButton;
	/// <summary>
	/// Canvas that displays contols
	/// </summary>
	public CanvasGroup HowToPlayCanvas;
	/// <summary>
	/// The slider that changes the lentgh of the game in 'years'
	/// </summary>
	public Slider time;
	/// <summary>
	/// The slider that changes the number of players
	/// </summary>
	public Slider players;
	/// <summary>
	/// Text that displays 'times' value
	/// </summary>
	public Slider powerUpOcc;
	public Text timeValue;
	/// <summary>
	/// Text that displays 'players' value
	/// </summary>
	public Text playersValue;
	/// <summary>
	/// 'button' to toggle map1
	/// </summary>
	public Text powerUpOccValue;
	public GameObject map1;
	/// <summary>
	/// 'button' to toggle map2
	/// </summary>
	public GameObject map2;
	/// <summary>
	/// 'button' to toggle map3
	/// </summary>
	public GameObject map3;

	/// <summary>
	/// Get Components and select the Start-text.
	/// </summary>
	void Start()
	{
		if (GameWideScript.Instance.setCostum) {
			Sound = GameWideScript.Instance.setSound;
			Pow = GameWideScript.Instance.setPow;
			Zone = GameWideScript.Instance.setZone;
			Time = GameWideScript.Instance.setTime/10;
			Players = GameWideScript.Instance.setPlayers;
			Map = GameWideScript.Instance.setMap;
			PowUpOcc = GameWideScript.Instance.setPowUpOcc;
			PowUpDelay = (GameWideScript.Instance.setPowUpDelay);
			Shrink = GameWideScript.Instance.setKrymp;
		}
		else {
			Sound = true;
			Pow = true;
			Zone = true;
			Time = 210;
			Players = 2;
			Map = 1;
			PowUpOcc = 2;
			PowUpDelay = 10;
			Shrink = false;
		}
		SetGUI ();
		HowToPlayCanvas.interactable = false;
		HowToPlayCanvas.alpha = 0;
		HowToPlayCanvas.gameObject.SetActive(false);
		SoundButton.Select ();
	}

	/// <summary>
	/// If triggered, goto scene "Map".
	/// </summary>
	public void StartLevel() //this function will be used on our Play button
	{
		GameWideScript.Instance.setMap = Map;
		GameWideScript.Instance.setSound = Sound;
		GameWideScript.Instance.setPow = Pow;
		GameWideScript.Instance.setZone = Zone;
		GameWideScript.Instance.setTime = 10 * Time;
		GameWideScript.Instance.setPlayers = Players;
		GameWideScript.Instance.setPowUpOcc = PowUpOcc;
		GameWideScript.Instance.setPowUpDelay = PowUpDelay;
		GameWideScript.Instance.setKrymp = Shrink;

		GameWideScript.Instance.setCostum = true;

        if (Map == 1)
			SceneManager.LoadScene("Map"); //this will load our first level from our build settings. "1" is the second scene in our game
        else if (Map == 2)
            SceneManager.LoadScene("Map2"); //this will load our first level from our build settings. "1" is the second scene in our game
        else
            SceneManager.LoadScene("Map3"); //this will load our first level from our build settings. "1" is the second scene in our game
    }
	/// <summary>
	/// If triggered, shutdown the game.
	/// </summary>
	public void ExitGame() //This function will be used on our "Yes" button in our Quit menu

	{
		Application.Quit(); //this will quit our game. Note this will only work after building the game
	}
	/// <summary>
	/// uses the slider 'time' to set a variable that later changes the global setTime variable in GameWideScript
	/// </summary>
	public void timeChange(){
		Time = (int)time.value;
		timeValue.text =(10 * Time).ToString();
	}
	/// <summary>
	/// uses the slider 'players' to set a variable that later changes the global setPlayers variable in GameWideScript.
	/// </summary>
	public void playersChange(){
		Players = (int)players.value;
		playersValue.text = Players.ToString();
	}

	public void PowUpOccChange(){
		PowUpOcc = (int)powerUpOcc.value;
		switch (PowUpOcc) {
		case 1:
			powerUpOccValue.text = "NONE";
			Pow = false;
			break;
		case 2: 
			powerUpOccValue.text = "LOW";
			PowUpDelay = 10;
			break;
		case 3: 
			powerUpOccValue.text = "MEDIUM";
			PowUpDelay = 5;
			break;
		case 4:
			powerUpOccValue.text = "HIGH";
			PowUpDelay = 0;
			break;	
		}

	}
	/// <summary>
	/// Shows the howToPlay canvas
	/// </summary>
	public void LoadHowToPlay () {
		HowToPlayCanvas.gameObject.SetActive(true);
		HowToPlayCanvas.interactable = true;
		HowToPlayCanvas.alpha = 0.8f;
		HowToPlayClose.Select ();
	}
	/// <summary>
	/// Closes the HowToPlay canvas
	/// </summary>
	public void closeHowToPlay () {
		HowToPlayCanvas.interactable = false;
		HowToPlayCanvas.alpha = 0;
		HowToPlayCanvas.gameObject.SetActive(false);
	}

	private void SetGUI (){
		time.value = Time;
		players.value = Players;
		powerUpOcc.value = PowUpOcc;
		timeValue.text = (10 * Time).ToString();
		playersValue.text = Players.ToString();
		switch (PowUpOcc) {
		case 1:
			powerUpOccValue.text = "NONE";
			break;
		case 2: 
			powerUpOccValue.text = "LOW";
			break;
		case 3: 
			powerUpOccValue.text = "MEDIUM";
			break;
		case 4:
			powerUpOccValue.text = "HIGH";
			break;	
		}

		Button b = SoundButton.GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		cb.normalColor = timeValue.color;

		if (Sound) {
			SoundButton.GetComponent<Image> ().color = timeValue.color;
		}
			
		if (Zone) {
			ZonesButton.GetComponent<Image> ().color  = timeValue.color;
		}

		if (Shrink) {
			ShrinkButton.GetComponent<Image> ().color = timeValue.color;
		}

		// Custom map could be set
		if (GameWideScript.Instance.setCostum) {
			switch (GameWideScript.Instance.setMap) {
			case 1: 
				map1.transform.localScale = new Vector2 (map1.transform.localScale.x + 0.3f, map1.transform.localScale.y + 0.3f);
				map2.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
				map3.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
				break;
			case 2:
				map2.transform.localScale = new Vector2 (map2.transform.localScale.x + 0.3f, map2.transform.localScale.y + 0.3f);
				map1.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
				map3.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
				break;
			case 3:
				map3.transform.localScale = new Vector2 (map3.transform.localScale.x + 0.3f, map3.transform.localScale.y + 0.3f);
				map2.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
				map1.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
				break;
			}
		}
		// Set default map
		else {
			map1.transform.localScale = new Vector2 (map1.transform.localScale.x + 0.3f, map1.transform.localScale.y + 0.3f);
			map2.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
			map3.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
		}
	}
	/// <summary>
	/// Toggles clicksounds
	/// </summary>
	public void onClickSound() {
		Sound = !Sound;
		Button b = SoundButton.GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		if (!Sound) { 
			SoundButton.GetComponent<Image>().color = Color.white;
			AudioListener.pause = true;
		} else {
			SoundButton.GetComponent<Image>().color = timeValue.color;
			AudioListener.pause = false;
		}
		b.colors = cb;
	}
	/// <summary>
	/// handles the Power-up Button
	/// </summary>
	public void onClickShrink() {
		Shrink = !Shrink;
		Button b = ShrinkButton.GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		if (!Shrink) { 
			ShrinkButton.GetComponent<Image>().color = Color.white;
		} else {
			ShrinkButton.GetComponent<Image>().color = timeValue.color;
		}
		b.colors = cb;
	}
	/// <summary>
	/// Handles the Zone button
	/// </summary>

	public void onClickZone() {
		Zone = !Zone;
		Button b = ZonesButton.GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		if (!Zone) { 
			ZonesButton.GetComponent<Image>().color = Color.white;
		} else {
			ZonesButton.GetComponent<Image>().color = timeValue.color;
		}
		b.colors = cb;
	}
	/// <summary>
	/// Handles the sound button
	/// ---hover---
	/// </summary>
	public void onHoverSound(){
		Button b = SoundButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.black;
	}
	/// <summary>
	/// handles the powerup button
	/// --- hover ---
	/// </summary>
	public void onHoverShrink(){
		Button b = ShrinkButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.black;
	}
	/// <summary>
	/// handles the zone button
	/// --- hover ---
	/// </summary>
		
	public void onHoverZone(){
		Button b = ZonesButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.black;
	}
	/// <summary>
	/// handles the sound button
	/// </summary>
	public void onLeaveSound(){
		Button b = SoundButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.clear;
	}

	/// <summary>
	/// handles the powerup button
	/// </summary>
	public void onLeaveShrink(){
		Button b = ShrinkButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.clear;
	}
	/// <summary>
	/// handles the zone button
	/// </summary>

	public void onLeaveZone(){
		Button b = ZonesButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.clear;
	}
	/// <summary>
	/// handles the map1 'button'
	/// </summary>
	public void map1click (){
		Map = 1;
		mapChoice (1);
	}
	/// <summary>
	/// handles the map2 'button'
	/// </summary>
	public void map2click (){
		Map = 2;
		mapChoice (2);
	}

	/// <summary>
	/// handles the map3 'button'
	/// </summary>
	public void map3click (){
		Map = 3;
		mapChoice (3);
	}

	/// <summary>
	/// handles the map1 'button'
	/// </summary>
	public void map1Hover (){
		mapChoiceSelect (1);
	}

	/// <summary>
	/// handles the map2 'button'
	/// </summary>
	public void map2Hover (){
		mapChoiceSelect (2);
	}

	/// <summary>
	/// handles the map3 'button'
	/// </summary>
	public void map3Hover (){
		mapChoiceSelect (3);
	}

	/// <summary>
	/// handles the map1 'button'
	/// </summary>
	public void map1Leave (){
		mapChoiceLeave (1);
	}

	/// <summary>
	/// handles the map2 'button'
	/// </summary>
	public void map2Leave (){
		mapChoiceLeave (2);
	}

	/// <summary>
	/// handles the map3 'button'
	/// </summary>
	public void map3Leave (){
		mapChoiceLeave (3);
	}
	/// <summary>
	/// uses the inputs from the handlers of map1,map2,map3 'buttons' to change how the 'buttons' are displayed
	/// </summary>
	/// <param name="n">N.</param>
	public void mapChoice (int n){
		switch (n) {
		case 1:
			map1.transform.localScale = new Vector2 (1.3f, 1.3f);
			map2.transform.localScale = new Vector2 (1f, 1f);
			map3.transform.localScale = new Vector2 (1f, 1f);
			map1.GetComponent<CanvasRenderer> ().SetAlpha (1f);
			map2.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
			map3.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
			break;
		case 2:
			map2.transform.localScale = new Vector2 (1.3f, 1.3f);
			map1.transform.localScale = new Vector2 (1f, 1f);
			map3.transform.localScale = new Vector2 (1f, 1f);
			map2.GetComponent<CanvasRenderer> ().SetAlpha (1f);
			map1.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
			map3.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
			break;
		case 3:
			map3.transform.localScale = new Vector2 (1.3f, 1.3f);
			map2.transform.localScale = new Vector2 (1f, 1f);
			map1.transform.localScale = new Vector2 (1f, 1f);
			map3.GetComponent<CanvasRenderer> ().SetAlpha (1f);
			map2.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
			map1.GetComponent<CanvasRenderer> ().SetAlpha (0.7f);
			break;
		}
	}
	/// <summary>
	/// uses the inputs from the handlers of map1,map2,map3 'buttons' to change how the 'buttons' are displayed
	/// </summary>
	/// <param name="n">N.</param>
	public void mapChoiceSelect (int n){
		switch (n) {
		case 1:
			map1.transform.localScale = new Vector2 (1.3f, 1.3f);
			break;
		case 2:
			map2.transform.localScale = new Vector2 (1.3f, 1.3f);
			break;
		case 3:
			map3.transform.localScale = new Vector2 (1.3f, 1.3f);
			break;
		}
	}
	/// <summary>
	/// uses the inputs from the handlers of map1,map2,map3 'buttons' to change how the 'buttons' are displayed
	/// </summary>
	/// <param name="n">N.</param>
	public void mapChoiceLeave (int n){
		switch (n) {
		case 1:
			if(map1.GetComponent<CanvasRenderer>().GetAlpha() == 0.7f)
				map1.transform.localScale = new Vector2 (1f, 1f);
			break;
		case 2:
			if(map2.GetComponent<CanvasRenderer>().GetAlpha() == 0.7f)
				map2.transform.localScale = new Vector2 (1f, 1f);
			break;
		case 3:
			if(map3.GetComponent<CanvasRenderer>().GetAlpha() == 0.7f)
				map3.transform.localScale = new Vector2 (1f, 1f);
			break;
		}
	}
}