using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
	private bool Sound;
	private bool Pow;
	private bool Zone;
	private int Time;
	private int PowUpOcc;
	private int Players;
	private int Map;
	//private int minPowUpTime;
	//private int maxPowUpTime;
	public Button startText;
	public Button exitText;
	public Button HowToPlay;
	public Button HowToPlayClose;
	public Button SoundButton;
	public Button PowerUpButton;
	public Button ZonesButton;
	public CanvasGroup HowToPlayCanvas;
	public Slider time;
	public Slider players;
	public Slider powerUpOcc;
	public Text timeValue;
	public Text playersValue;
	public Text powerUpOccValue;
	public GameObject map1;
	public GameObject map2;
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
		}
		else {
			Sound = true;
			Pow = true;
			Zone = true;
			Time = 210;
			Players = 2;
			Map = 1;
			PowUpOcc = 2;
			//minPowUpTime = 1000;
			//maxPowUpTime = 2000;
		}
		SetGUI ();
		HowToPlayCanvas.interactable = false;
		HowToPlayCanvas.alpha = 0;
		HowToPlayCanvas.gameObject.active = false;
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
		//GameWideScript.Instance.setminPowUpTime = minPowUpTime;
		//GameWideScript.Instance.setmaxPowUpTime = maxPowUpTime;
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

	public void timeChange(){
		Time = (int)time.value;
		timeValue.text =(10 * Time).ToString();
	}

	public void playersChange(){
		Players = (int)players.value;
		playersValue.text = Players.ToString();
	}

	public void PowUpOccChange(){
		PowUpOcc = (int)powerUpOcc.value;
		switch (PowUpOcc) {
		case 1:
			powerUpOccValue.text = "None";
			Pow = false;
			break;
		case 2: 
			powerUpOccValue.text = "Low";
			break;
		case 3: 
			powerUpOccValue.text = "Medium";
			break;
		case 4:
			powerUpOccValue.text = "High";
			break;	
		}

	}

	public void LoadHowToPlay () {
		HowToPlayCanvas.gameObject.active = true;
		HowToPlayCanvas.interactable = true;
		HowToPlayCanvas.alpha = 0.8f;
		HowToPlayClose.Select ();
	}

	public void closeHowToPlay () {
		HowToPlayCanvas.interactable = false;
		HowToPlayCanvas.alpha = 0;
		HowToPlayCanvas.gameObject.active = false;
	}

	private void SetGUI (){
		time.value = Time;
		players.value = Players;
		powerUpOcc.value = PowUpOcc;
		timeValue.text = (10 * Time).ToString();
		playersValue.text = Players.ToString();
		powerUpOccValue.text = PowUpOcc.ToString ();

		Button b = SoundButton.GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		cb.normalColor = timeValue.color;

		if (Sound) {
			SoundButton.GetComponent<Image> ().color = timeValue.color;
		}
		if (Pow) {
			PowerUpButton.GetComponent<Image> ().color = timeValue.color;
		}
		if (Zone) {
			ZonesButton.GetComponent<Image> ().color  = timeValue.color;
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

	public void onClickPower() {
		Pow = !Pow;
		Button b = PowerUpButton.GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		if (!Pow) { 
			PowerUpButton.GetComponent<Image>().color = Color.white;
		} else {
			PowerUpButton.GetComponent<Image>().color = timeValue.color;
		}
		b.colors = cb;
	}

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

	public void onHoverSound(){
		Button b = SoundButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.black;
	}

	public void onHoverPower(){
		Button b = PowerUpButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.black;
	}

	public void onHoverZone(){
		Button b = ZonesButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.black;
	}

	public void onLeaveSound(){
		Button b = SoundButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.clear;
	}

	public void onLeavePower(){
		Button b = PowerUpButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.clear;
	}

	public void onLeaveZone(){
		Button b = ZonesButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = Color.clear;
	}
		
	public void map1click (){
		Map = 1;
		mapChoice (1);
	}

	public void map2click (){
		Map = 2;
		mapChoice (2);
	}

	public void map3click (){
		Map = 3;
		mapChoice (3);
	}

	public void map1Hover (){
		mapChoiceSelect (1);
	}

	public void map2Hover (){
		mapChoiceSelect (2);
	}

	public void map3Hover (){
		mapChoiceSelect (3);
	}

	public void map1Leave (){
		mapChoiceLeave (1);
	}

	public void map2Leave (){
		mapChoiceLeave (2);
	}

	public void map3Leave (){
		mapChoiceLeave (3);
	}

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