using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
/// <summary>
/// Script to handle the settings-menu
/// </summary>
public class menuScriptV2 : MonoBehaviour
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

	/// <summary>
	/// The time handle.
	/// </summary>
	public GameObject timeHandle;

	/// <summary>
	/// The power up handle.
	/// </summary>
	public GameObject powerUpHandle;

	/// <summary>
	/// The player handle.
	/// </summary>
	public GameObject playerHandle;

	public Text timeValue;
	/// <summary>
	/// Text that displays 'players' value
	/// </summary>
	public Text playersValue;
	/// <summary>
	/// 'button' to toggle map1
	/// </summary>
	public Text powerUpOccValue;

	//Rotate maps stuff :P
	private GameObject[] themMaps;
	private float leftPosBall;
	private float rightPosBall;
	private bool inMapWrapper;
	private bool player1Right;
	private bool player1Left;
	private int player1I;
	public Text mapName;
	/// <summary>
	/// The remove options.
	/// </summary>
	private bool removeOptions = false;
	/// <summary>
	/// The remove counter.
	/// </summary>
	private int removeCounter = 12;

	/// <summary>
	/// Get Components and select the Start-text.
	/// </summary>
	void Start()
	{
		inMapWrapper = false;
		player1Left = false;
		player1Right = false;
		player1I = 1;
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

		themMaps = GameObject.FindGameObjectsWithTag("Maps").OrderBy( go => go.name ).ToArray();;
		rightPosBall = themMaps [themMaps.Length - 1].transform.position.x;
		leftPosBall = GameObject.Find("ghostmap").transform.position.x;

		SetGUI ();
		HowToPlayCanvas.interactable = false;
		HowToPlayCanvas.alpha = 0;
		HowToPlayCanvas.gameObject.SetActive(false);
		SoundButton.Select ();
	}

	private void FixedUpdate(){
		if (removeOptions) {
			if (removeCounter != 0) {
				float current = gameObject.GetComponent<CanvasGroup> ().alpha;
				gameObject.GetComponent<CanvasGroup> ().alpha = (current - 0.084f);
				startText.GetComponent<CanvasGroup> ().alpha -= 0.084f;
				exitText.GetComponent<CanvasGroup> ().alpha -= 0.084f;
				HowToPlay.GetComponent<CanvasGroup> ().alpha -= 0.084f;
				removeCounter = removeCounter - 1;
			} else if (removeCounter == 0){
				gameObject.GetComponent<CanvasGroup> ().interactable = false;
				startText.GetComponent<CanvasGroup> ().interactable = false;
				exitText.GetComponent<CanvasGroup> ().interactable = false;
				HowToPlay.GetComponent<CanvasGroup> ().interactable = false;
				BollMenyScript.fadeInBalls = true;
			}
		}
		if (inMapWrapper) {
			if ((Input.GetAxis ("Horizontal1") == 1 || Input.GetAxis ("Horizontal2") == 1 || Input.GetAxis ("Horizontal3") == 1 || Input.GetAxis ("Horizontal4") == 1) && !player1Left) {
				if (!player1Right) {
					player1Right = !player1Right;
					StartCoroutine (rotateRight (player1I, themMaps));
					player1I -= 1;
				}
			} else if ((Input.GetAxis ("Horizontal1") < 0.8 && Input.GetAxis ("Horizontal2") < 0.8 && Input.GetAxis ("Horizontal3") < 0.8 && Input.GetAxis ("Horizontal4") < 0.8) && player1Right) {
				player1Right = !player1Right;
			}

			if ((Input.GetAxis ("Horizontal1") == -1 || Input.GetAxis ("Horizontal2") == -1 || Input.GetAxis ("Horizontal3") == -1 || Input.GetAxis ("Horizontal4") == -1) && !player1Right) {
				if (!player1Left) {
					player1Left = !player1Left;
					StartCoroutine (rotateLeft (player1I, themMaps));
					player1I += 1;
				}
			} else if ((Input.GetAxis ("Horizontal1") > -0.8 && Input.GetAxis ("Horizontal2") > -0.8 && Input.GetAxis ("Horizontal3") > -0.8 && Input.GetAxis ("Horizontal4") > -0.8) && player1Left) {
				player1Left = !player1Left;
			}
		}
	}

	/// <summary>
	/// If triggered, goto scene "Map".
	/// </summary>
	public void StartLevel() //this function will be used on our Play button
	{
		GameWideScript.Instance.setMap = Mod(player1I, themMaps.Length);
		GameWideScript.Instance.setSound = Sound;
		GameWideScript.Instance.setPow = Pow;
		GameWideScript.Instance.setZone = Zone;
		GameWideScript.Instance.setTime = 10 * Time;
		GameWideScript.Instance.setPlayers = Players;
		GameWideScript.Instance.setPowUpOcc = PowUpOcc;
		GameWideScript.Instance.setPowUpDelay = PowUpDelay;
		GameWideScript.Instance.setKrymp = Shrink;

		GameWideScript.Instance.setCostum = true;

        removeOptions = true;
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
			powerUpOccValue.text = "None";
			Pow = false;
			break;
		case 2: 
			powerUpOccValue.text = "Low";
			PowUpDelay = 10;
			break;
		case 3: 
			powerUpOccValue.text = "Medium";
			PowUpDelay = 5;
			break;
		case 4:
			powerUpOccValue.text = "High";
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
			powerUpOccValue.text = "None";
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

		if (Sound) {
			SoundButton.GetComponent<Image> ().color = new Color32(102,102,102,255); 
			SoundButton.transform.GetChild(0).GetComponent<Text> ().color = Color.white;
		}
			
		if (Zone) {
			ZonesButton.GetComponent<Image> ().color  = new Color32(102,102,102,255);
			ZonesButton.transform.GetChild(0).GetComponent<Text> ().color = Color.white;
		}

		if (Shrink) {
			ShrinkButton.GetComponent<Image> ().color = new Color32(102,102,102,255);
			ShrinkButton.transform.GetChild(0).GetComponent<Text> ().color = Color.white;
		}

		// Custom map could be set
		if (GameWideScript.Instance.setCostum) {
			if (Map < 1) {
				int middle = 1;
				for (int rotate = (1 - Map); rotate > 0; rotate--) {
					StartCoroutine (rotateRightNoDelay (middle, themMaps));
					middle -= 1;
				}
			} else if (Map > 1) {
				int middle = 1;
				for (int rotate = (Map - 1); rotate > 0; rotate--){
					StartCoroutine (rotateLeftNoDelay (middle, themMaps));
					middle += 1;
				}
			}
		}
	}
	/// <summary>
	/// Toggles clicksounds
	/// </summary>
	public void onClickSound() {
		Sound = !Sound; 
		if (!Sound) { 
			SoundButton.GetComponent<Image>().color = Color.white;
			SoundButton.transform.GetChild(0).GetComponent<Text> ().color = new Color32(102,102,102,255);
			AudioListener.pause = true;
		} else {
			SoundButton.GetComponent<Image>().color = new Color32(102,102,102,255);
			SoundButton.transform.GetChild (0).GetComponent<Text> ().color = Color.white;
			AudioListener.pause = false;
		}
	}
	/// <summary>
	/// handles the Power-up Button
	/// </summary>
	public void onClickShrink() {
		Shrink = !Shrink;
		if (!Shrink) { 
			ShrinkButton.GetComponent<Image>().color = Color.white;
			ShrinkButton.transform.GetChild(0).GetComponent<Text> ().color = new Color32(102,102,102,255);
		} else {
			ShrinkButton.GetComponent<Image>().color = new Color32(102,102,102,255);
			ShrinkButton.transform.GetChild (0).GetComponent<Text> ().color = Color.white;
		}
	}
	/// <summary>
	/// Handles the Zone button
	/// </summary>

	public void onClickZone() {
		Zone = !Zone;
		if (!Zone) { 
			ZonesButton.GetComponent<Image>().color = Color.white;
			ZonesButton.transform.GetChild(0).GetComponent<Text> ().color = new Color32(102,102,102,255);
		} else {
			ZonesButton.GetComponent<Image>().color = new Color32(102,102,102,255);
			ZonesButton.transform.GetChild (0).GetComponent<Text> ().color = Color.white;
		}
	}
	/// <summary>
	/// Handles the sound button
	/// ---hover---
	/// </summary>
	public void onHoverSound(){
		Button b = SoundButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = new Color32(0,147,209,255);
	}
	/// <summary>
	/// handles the powerup button
	/// --- hover ---
	/// </summary>
	public void onHoverShrink(){
		Button b = ShrinkButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = new Color32(0,147,209,255);
	}
	/// <summary>
	/// handles the zone button
	/// --- hover ---
	/// </summary>
		
	public void onHoverZone(){
		Button b = ZonesButton.GetComponent<Button>(); 
		b.GetComponent<Outline> ().effectColor = new Color32(0,147,209,255);
	}
	/// <summary>
	/// OnHover time.
	/// </summary>
	public void onHoverTime(){
		timeHandle.GetComponent<Outline>().effectColor = new Color32(0,147,209,255);
	}

	/// <summary>
	/// OnHover powup.
	/// </summary>
	public void onHoverPowUp(){
		powerUpHandle.GetComponent<Outline>().effectColor = new Color32(0,147,209,255);
	}

	/// <summary>
	/// OnHover players.
	/// </summary>
	public void onHoverPlayers(){
		playerHandle.GetComponent<Outline>().effectColor = new Color32(0,147,209,255);
	}

	/// <summary>
	/// OnLeave time.
	/// </summary>
	public void onLeaveTime(){
		timeHandle.GetComponent<Outline>().effectColor = Color.clear;
	}

	/// <summary>
	/// OnLeave powup.
	/// </summary>
	public void onLeavePowUp(){
		powerUpHandle.GetComponent<Outline>().effectColor = Color.clear;
	}

	/// <summary>
	/// OnLeave players.
	/// </summary>
	public void onLeavePlayers(){
		playerHandle.GetComponent<Outline>().effectColor = Color.clear;
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

	private void fadeOut(int i) {
		while(i != 0){
			float current = gameObject.GetComponent<CanvasGroup> ().alpha;
			gameObject.GetComponent<CanvasGroup> ().alpha = (current - 0.01f);
			i = i-1;
		}
		gameObject.GetComponent<CanvasGroup> ().interactable = false;

	}

	private IEnumerator rotateRight(int i, GameObject[] balls) {
		if (balls [Mod((i + 2), balls.Length)].transform.position.x != leftPosBall)
			balls [Mod((i + 2), balls.Length)].transform.position = new Vector2 (leftPosBall , balls [Mod((i + 2), balls.Length)].transform.position.y);

		for (int x = 0; x < 10; x++) {
			if (x < 5)
				fadeOutName ();
			else
				fadeInName ();
			stepRight (balls [Mod((i + 2), balls.Length)]);
			stepRightAndGrow (balls [Mod((i - 1), balls.Length)]);
			stepRightAndShrink (balls [Mod(i, balls.Length)]);
			stepRight (balls [Mod((i + 1), balls.Length)]);
			yield return new WaitForSeconds (0.001f);
		}

	}
	private IEnumerator rotateRightNoDelay(int i, GameObject[] balls) {
		if (balls [Mod((i + 2), balls.Length)].transform.position.x != leftPosBall)
			balls [Mod((i + 2), balls.Length)].transform.position = new Vector2 (leftPosBall, balls [Mod((i + 2), balls.Length)].transform.position.y);

		fadeOutNameNoName ();
		fadeInNameNoDelay ();
		stepRightNoDelay (balls [Mod((i + 2), balls.Length)]);
		stepRightAndGrowNoDelay (balls [Mod((i - 1), balls.Length)]);
		stepRightAndShrinkNoDelay (balls [Mod(i, balls.Length)]);
		stepRightNoDelay (balls [Mod((i + 1), balls.Length)]);
		yield return null;
	}
	private void stepRight (GameObject ball) {
		ball.transform.position = new Vector2 (ball.transform.position.x + 14.5f, ball.transform.position.y);
	}

	private void stepRightAndGrow (GameObject ball) {
		RectTransform temp = ball.GetComponent<RectTransform> ();
		RawImage temp2 = ball.GetComponent<RawImage> ();
		ball.transform.position = new Vector2 (ball.transform.position.x + 14.5f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x + 0.21f, temp.sizeDelta.y + 0.099999999999f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a + 0.06f );
	}

	private void stepRightAndShrink (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		ball.transform.position = new Vector2 (ball.transform.position.x + 14.5f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x - 0.21f, temp.sizeDelta.y - 0.099999999999f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a - 0.06f);
	}
	private void stepRightNoDelay (GameObject ball) {
		ball.transform.position = new Vector2 (ball.transform.position.x + 145f, ball.transform.position.y);
	}

	private void stepRightAndGrowNoDelay (GameObject ball) {
		RectTransform temp = ball.GetComponent<RectTransform> ();
		RawImage temp2 = ball.GetComponent<RawImage> ();
		ball.transform.position = new Vector2 (ball.transform.position.x + 145f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x + 2.1f, temp.sizeDelta.y + 0.99999999999f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a + 0.6f );
	}

	private void stepRightAndShrinkNoDelay (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		ball.transform.position = new Vector2 (ball.transform.position.x + 145f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x - 2.1f, temp.sizeDelta.y - 0.99999999999f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a - 0.6f);
	}

	private IEnumerator rotateLeft(int i, GameObject[] balls) {
		if (balls [Mod((i + 2), balls.Length)].transform.position.x != rightPosBall)
			balls [Mod((i + 2), balls.Length)].transform.position = new Vector2 (rightPosBall, balls [Mod((i + 2), balls.Length)].transform.position.y);

		for (int x = 0; x < 10; x++) {
			if (x < 5)
				fadeOutName ();
			else
				fadeInName ();
			stepLeft (balls [Mod((i + 2), balls.Length)]);
			stepLeftAndGrow (balls [Mod((i + 1), balls.Length)]);
			stepLeftAndShrink (balls [Mod(i, balls.Length)]);
			stepLeft (balls [Mod((i - 1), balls.Length)]);
			yield return new WaitForSeconds (0.001f);
		}

	}
	private IEnumerator rotateLeftNoDelay(int i, GameObject[] balls) {
		if (balls [Mod((i + 2), balls.Length)].transform.position.x != rightPosBall)
			balls [Mod((i + 2), balls.Length)].transform.position = new Vector2 (rightPosBall, balls [Mod((i + 2), balls.Length)].transform.position.y);

		fadeOutNameNoName ();
		fadeInNameNoDelay ();
		stepLeftNoDelay (balls [Mod((i + 2), balls.Length)]);
		stepLeftAndGrowNoDelay (balls [Mod((i + 1), balls.Length)]);
		stepLeftAndShrinkNoDelay (balls [Mod(i, balls.Length)]);
		stepLeftNoDelay (balls [Mod((i - 1), balls.Length)]);
		yield return null;
	}
	private void stepLeft (GameObject ball) {
		ball.transform.position = new Vector2 (ball.transform.position.x - 14.5f, ball.transform.position.y);
	}

	private void stepLeftAndGrow (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		ball.transform.position = new Vector2 (ball.transform.position.x - 14.5f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x + 0.21f, temp.sizeDelta.y + 0.099999999999f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a + 0.06f);
	}

	private void stepLeftAndShrink (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		ball.transform.position = new Vector2 (ball.transform.position.x - 14.5f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x - 0.21f, temp.sizeDelta.y - 0.099999999999f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a - 0.06f );
	}

	private void stepLeftNoDelay (GameObject ball) {
		ball.transform.position = new Vector2 (ball.transform.position.x - 145f, ball.transform.position.y);
	}

	private void stepLeftAndGrowNoDelay (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		ball.transform.position = new Vector2 (ball.transform.position.x - 145f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x + 2.1f, temp.sizeDelta.y + 0.99999999999f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a + 0.6f);
	}

	private void stepLeftAndShrinkNoDelay (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		ball.transform.position = new Vector2 (ball.transform.position.x - 145f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x - 2.1f, temp.sizeDelta.y - 0.99999999999f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a - 0.6f );
	}

	public void inMaps(){
		inMapWrapper = !inMapWrapper;
	}

	private void fadeInName(){
		switch (Mod(player1I, themMaps.Length)+1) {
		case 1:
			mapName.text = "Iceberg, Right ahead!";
			break;
		case 2:
			mapName.text = "Murder on the dancefloor";
			break;
		case 3:
			mapName.text = "Eiffel mountain";
			break;
		case 4:
			mapName.text = "Rock bottom";
			break;
		}
		mapName.color = new Color (mapName.color.r, mapName.color.g, mapName.color.b, mapName.color.a + 0.2f);
	}

	private void fadeOutName(){
		mapName.color = new Color (mapName.color.r, mapName.color.g, mapName.color.b, mapName.color.a - 0.2f);
	}
	private void fadeInNameNoDelay(){
		switch (Mod(player1I, themMaps.Length)+1) {
		case 1:
			mapName.text = "Iceberg, Right ahead!";
			break;
		case 2:
			mapName.text = "Murder on the dancefloor";
			break;
		case 3:
			mapName.text = "Eiffel mountain";
			break;
		case 4:
			mapName.text = "Rock bottom";
			break;
		}
		mapName.color = new Color (mapName.color.r, mapName.color.g, mapName.color.b, mapName.color.a + 1f);
	}

	private void fadeOutNameNoName(){
		mapName.color = new Color (mapName.color.r, mapName.color.g, mapName.color.b, mapName.color.a - 1f);
	}

	private int Mod(int a, int b){
		return (a % b + b) % b;
	}
}
