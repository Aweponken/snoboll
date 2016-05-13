using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class BollMenyScript : MonoBehaviour {
	// General stuff:P
	public static bool fadeInBalls = false;
	private int removeCounter = 13;
	public Button start;
	public Button deselect;

	//Used for input
	private bool player1Right;
	private bool player1Left;
	private bool player1Clicked;
	private bool player2Right;
	private bool player2Left;
	private bool player2Clicked;
	private bool player3Right;
	private bool player3Left;
	private bool player3Clicked;
	private bool player4Right;
	private bool player4Left;
	private bool player4Clicked;
	private float leftPosBall;
	private float rightPosBall;

	//Arrays for the player's ballz :P
	private GameObject[] player1Balls;
	private GameObject[] player2Balls;
	private GameObject[] player3Balls;
	private GameObject[] player4Balls;

	//Indexes for the "middle" ball
	private int player1I;
	private int player2I;
	private int player3I;
	private int player4I;

	//Bool array to check if all players have selected a color
	private bool[] isSelected;
	private int[] playersSelectedColor;
	private bool isAllSelected = true;

	//Bool array to see what balls are selected
	private bool[] selectedColors;

	// Use this for initialization
	void Start () {
		player1Right = false;
		player1Left = false;
		player1Clicked = false;
		player2Right = false;
		player2Left = false;
		player2Clicked = false;
		player3Right = false;
		player3Left = false;
		player3Clicked = false;
		player4Right = false;
		player4Left = false;
		player4Clicked = false;
		fadeInBalls = false;
		removeCounter = 13;
		isAllSelected = false;
		isSelected = new bool[GameWideScript.Instance.setPlayers];
		playersSelectedColor = new int[GameWideScript.Instance.setPlayers];
		// Get all the player1 ball gameObjects
		player1Balls = GameObject.FindGameObjectsWithTag("player1Bollar").OrderBy( go => go.name ).ToArray();;
		player1I = 3;

		// Get all the player2 ball gameObjects
		player2Balls = GameObject.FindGameObjectsWithTag("player2Bollar").OrderBy( go => go.name ).ToArray();;
		player2I = 3;

		// Get all the player3 ball gameObjects
		player3Balls = GameObject.FindGameObjectsWithTag("player3Bollar").OrderBy( go => go.name ).ToArray();;
		player3I = 3;

		// Get all the player4 ball gameObjects
		player4Balls = GameObject.FindGameObjectsWithTag("player4Bollar").OrderBy( go => go.name ).ToArray();;
		player4I = 3;

		//Set correct length of array
		selectedColors = new bool[player1Balls.Length];

		leftPosBall = player1Balls [0].transform.position.x;
		rightPosBall = player1Balls [6].transform.position.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (fadeInBalls) {
			if (removeCounter == 13) {
				gameObject.GetComponent<CanvasGroup> ().interactable = true;
				Debug.Log (GameWideScript.colorSelection.isPlayedOnce);
				if (GameWideScript.colorSelection.isPlayedOnce) {
					player1I = GameWideScript.colorSelection.playersIndex[0];
					player2I = GameWideScript.colorSelection.playersIndex[1];
					player3I = GameWideScript.colorSelection.playersIndex[2];
					player4I = GameWideScript.colorSelection.playersIndex[3];
					selectedColors = GameWideScript.colorSelection.selectedColors;
					isSelected = GameWideScript.colorSelection.isSelected;
					playersSelectedColor = GameWideScript.colorSelection.playersSelectedColor;

					selectColor (1, Mod(player1I,selectedColors.Length));
					selectColor (2, Mod(player2I,selectedColors.Length));
					if (isSelected.Length > 2) {
						selectColor (3, Mod (player3I, selectedColors.Length));
						if(isSelected.Length > 3)
							selectColor (4, Mod (player4I, selectedColors.Length));
					}

					int witchPlayer = 0;
					foreach (int x in playersSelectedColor) {
						GameObject[] temp = null;
						switch (witchPlayer) {
							case 0: 
								temp = player1Balls;
								break;
							case 1: 
								temp = player2Balls;
								break;
							case 2: 
								temp = player3Balls;
								break;
							case 3: 
								temp = player4Balls;
								break;
						}
						if (x < 3) {
							int middle = 3;
							for (int rotate = (3 - x); rotate > 0; rotate--) {
								StartCoroutine (rotateRightNoDelay (middle, temp));
								middle -= 1;
							}
						} else if (x > 3) {
							int middle = 3;
							for (int rotate = (x - 3); rotate > 0; rotate--){
								StartCoroutine (rotateLeftNoDelay (middle, temp));
								middle += 1;
							}
						}
						witchPlayer += 1;
					}
				} else {
					isSelected = new bool[GameWideScript.Instance.setPlayers];
					playersSelectedColor = new int[GameWideScript.Instance.setPlayers];
				}
				switch (GameWideScript.Instance.setPlayers) {
				case 2:
					GameObject.Find ("'dem ballz P3").SetActive (false);
					GameObject.Find ("'dem ballz P4").SetActive (false);
					GameObject.Find ("Player 3").SetActive (false);
					GameObject.Find ("Player 4").SetActive (false);
					break;
				case 3:
					GameObject.Find ("'dem ballz P4").SetActive (false);
					GameObject.Find ("Player 4").SetActive (false);
					break;
				}
				removeCounter -= 1;
			}
			else if (removeCounter > 0) {
				float current = gameObject.GetComponent<CanvasGroup> ().alpha;
				gameObject.GetComponent<CanvasGroup> ().alpha = (current + 0.084f);
				removeCounter = removeCounter - 1;
			} else if (removeCounter == 0) {
				removeCounter -= 1;
			}
		
			// Check if all players have selected their color
			foreach (bool player in isSelected) {
				if (!player) {
					isAllSelected = false;
					break;
				}
			}

			//Input from player 1
			if (Input.GetAxis ("Horizontal1") == 1 && !player1Left) {
				if (!player1Right) {
					player1Right = !player1Right;
					StartCoroutine (rotateRight (player1I, player1Balls));
					player1I -= 1;
					if(isSelected [0])
						isSelected [0] = false;
				}
			} else if (Input.GetAxis ("Horizontal1") < 0.8 && player1Right) {
				player1Right = !player1Right;
			}

			if (Input.GetAxis ("Horizontal1") == -1 && !player1Right) {
				if (!player1Left) {
					player1Left = !player1Left;
					StartCoroutine (rotateLeft (player1I, player1Balls));
					player1I += 1;
					if(isSelected [0])
						isSelected [0] = false;
				}
			} else if (Input.GetAxis ("Horizontal1") > -0.8 && player1Left) {
				player1Left = !player1Left;
			}

			if (Input.GetAxis ("Jump1") == 1) {
				if (!player1Clicked) {
					player1Clicked = !player1Clicked;
					if(!selectedColors[Mod(player1I,selectedColors.Length)])
						selectColor (1, Mod(player1I,selectedColors.Length));
				}
			} else if (Input.GetAxis ("Jump1") == 0 && player1Clicked) {
				player1Clicked = !player1Clicked;
			}

			//Input from player 2
			if (Input.GetAxis ("Horizontal2") == 1 && !player2Left) {
				if (!player2Right) {
					player2Right = !player2Right;
					StartCoroutine (rotateRight (player2I, player2Balls));
					player2I -= 1;
					if(isSelected [1])
						isSelected [1] = false;
				}
			} else if (Input.GetAxis ("Horizontal2") < 0.8 && player2Right) {
				player2Right = !player2Right;
			}

			if (Input.GetAxis ("Horizontal2") == -1 && !player2Right) {
				if (!player2Left) {
					player2Left = !player2Left;
					StartCoroutine (rotateLeft (player2I, player2Balls));
					player2I += 1;
					if(isSelected [1])
						isSelected [1] = false;
				}
			} else if (Input.GetAxis ("Horizontal2") > -0.8 && player2Left) {
				player2Left = !player2Left;
			}
			if (Input.GetAxis ("Jump2") == 1) {
				if (!player2Clicked) {
					player2Clicked = !player2Clicked;
					if(!selectedColors[Mod(player2I,selectedColors.Length)])
						selectColor (2, Mod(player2I,selectedColors.Length));
				}
			} else if (Input.GetAxis ("Jump2") == 0 && player2Clicked) {
				player2Clicked = !player2Clicked;
			}

			if (GameWideScript.Instance.setPlayers > 2) {
				//Input from player 3
				if (Input.GetAxis ("Horizontal3") == 1 && !player3Left) {
					if (!player3Right) {
						player3Right = !player3Right;
						StartCoroutine (rotateRight (player3I, player3Balls));
						player3I -= 1;
						if (isSelected [2])
							isSelected [2] = false;
					}
				} else if (Input.GetAxis ("Horizontal3") < 0.8 && player3Right) {
					player3Right = !player3Right;
				}

				if (Input.GetAxis ("Horizontal3") == -1 && !player3Right) {
					if (!player3Left) {
						player3Left = !player3Left;
						StartCoroutine (rotateLeft (player3I, player3Balls));
						player3I += 1;
						if (isSelected [2])
							isSelected [2] = false;
					}
				} else if (Input.GetAxis ("Horizontal3") > -0.8 && player3Left) {
					player3Left = !player3Left;
				}
				if (Input.GetAxis ("Jump3") == 1) {
					if (!player3Clicked) {
						player3Clicked = !player3Clicked;
						if (!selectedColors [Mod (player3I, selectedColors.Length)])
							selectColor (3, Mod (player3I, selectedColors.Length));
					}
				} else if (Input.GetAxis ("Jump3") == 0 && player3Clicked) {
					player3Clicked = !player3Clicked;
				}

				if (GameWideScript.Instance.setPlayers > 3) {
					//Input from player 4
					if (Input.GetAxis ("Horizontal4") == 1 && !player4Left) {
						if (!player4Right) {
							player4Right = !player4Right;
							StartCoroutine (rotateRight (player4I, player4Balls));
							player4I -= 1;
							if (isSelected [3])
								isSelected [3] = false;
						}
					} else if (Input.GetAxis ("Horizontal4") < 0.8 && player4Right) {
						player4Right = !player4Right;
					}

					if (Input.GetAxis ("Horizontal4") == -1 && !player4Right) {
						if (!player4Left) {
							player4Left = !player4Left;
							StartCoroutine (rotateLeft (player4I, player4Balls));
							player4I += 1;
							if (isSelected [3])
								isSelected [3] = false;
						}
					} else if (Input.GetAxis ("Horizontal4") > -0.8 && player4Left) {
						player4Left = !player4Left;
					}
					if (Input.GetAxis ("Jump4") == 1) {
						if (!player4Clicked) {
							player4Clicked = !player4Clicked;
							if (!selectedColors [Mod (player4I, selectedColors.Length)])
								selectColor (4, Mod (player4I, selectedColors.Length));
						}
					} else if (Input.GetAxis ("Jump4") == 0 && player4Clicked) {
						player4Clicked = !player4Clicked;
					}
				}
			}
			if (isAllSelected)
				start.Select ();
			else{
				deselect.Select ();
				isAllSelected = true;
			}
		}
	}

	public void StartLevel() {
		GameWideScript.colorSelection.isPlayedOnce = true;
		int[] tempIndex = new int[4];
		tempIndex [0] = player1I;
		tempIndex [1] = player2I;
		tempIndex [2] = player3I;
		tempIndex [3] = player4I;
		GameWideScript.colorSelection.playersIndex = tempIndex;
		GameWideScript.colorSelection.isSelected = isSelected;
		GameWideScript.colorSelection.playersSelectedColor = playersSelectedColor;
		GameWideScript.colorSelection.selectedColors = selectedColors;

		if (GameWideScript.Instance.setMap == 0)
			SceneManager.LoadScene("Map"); //this will load our first level from our build settings. "1" is the second scene in our game
		else if (GameWideScript.Instance.setMap == 1)
            SceneManager.LoadScene("Map2"); //this will load our first level from our build settings. "1" is the second scene in our game
		else if(GameWideScript.Instance.setMap == 2)
            SceneManager.LoadScene("Map3"); //this will load our first level from our build settings. "1" is the second scene in our game
		else 
			SceneManager.LoadScene("Map4");
	}

	private IEnumerator rotateRight(int i, GameObject[] balls) {
		if (balls [Mod((i - 2), balls.Length)].transform.position.x != leftPosBall)
			balls [Mod((i - 2), balls.Length)].transform.position = new Vector2 (leftPosBall, balls [Mod((i - 2), balls.Length)].transform.position.y);

		for (int x = 0; x < 10; x++) {
			stepRight (balls [Mod((i - 2), balls.Length)]);
			stepRightAndGrow (balls [Mod((i - 1), balls.Length)]);
			stepRightAndShrink (balls [Mod(i, balls.Length)]);
			stepRight (balls [Mod((i + 1), balls.Length)]);
			yield return new WaitForSeconds (0.001f);
		}

	}
	private IEnumerator rotateRightNoDelay(int i, GameObject[] balls) {
		if (balls [Mod((i - 2), balls.Length)].transform.position.x != leftPosBall)
			balls [Mod((i - 2), balls.Length)].transform.position = new Vector2 (leftPosBall, balls [Mod((i - 2), balls.Length)].transform.position.y);


			stepRightNoDelay (balls [Mod((i - 2), balls.Length)]);
			stepRightAndGrowNoDelay (balls [Mod((i - 1), balls.Length)]);
			stepRightAndShrinkNoDelay (balls [Mod(i, balls.Length)]);
			stepRightNoDelay (balls [Mod((i + 1), balls.Length)]);
			yield return null;


	}
	private void stepRight (GameObject ball) {
		ball.transform.position = new Vector2 (ball.transform.position.x + 7.225f, ball.transform.position.y);
	}

	private void stepRightAndGrow (GameObject ball) {
		RectTransform temp = ball.GetComponent<RectTransform> ();
		RawImage temp2 = ball.GetComponent<RawImage> ();
		Text selected = ball.transform.GetChild (0).GetComponent<Text> ();
		ball.transform.position = new Vector2 (ball.transform.position.x + 7.225f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x + 0.05f, temp.sizeDelta.y + 0.05f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a + 0.06f );
		selected.color = new Color(selected.color.r, selected.color.g, selected.color.b, selected.color.a + 0.1f);
	}

	private void stepRightAndShrink (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		Text selected = ball.transform.GetChild (0).GetComponent<Text> ();
		ball.transform.position = new Vector2 (ball.transform.position.x + 7.225f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x - 0.05f, temp.sizeDelta.y - 0.05f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a - 0.06f);
		selected.color = new Color(selected.color.r, selected.color.g, selected.color.b, selected.color.a - 0.1f);
	}
	private void stepRightNoDelay (GameObject ball) {
		ball.transform.position = new Vector2 (ball.transform.position.x + 72.25f, ball.transform.position.y);
	}

	private void stepRightAndGrowNoDelay (GameObject ball) {
		RectTransform temp = ball.GetComponent<RectTransform> ();
		RawImage temp2 = ball.GetComponent<RawImage> ();
		Text selected = ball.transform.GetChild (0).GetComponent<Text> ();
		ball.transform.position = new Vector2 (ball.transform.position.x + 72.25f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x + 0.5f, temp.sizeDelta.y + 0.5f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a + 0.6f );
		selected.color = new Color(selected.color.r, selected.color.g, selected.color.b, selected.color.a + 1f);
	}

	private void stepRightAndShrinkNoDelay (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		Text selected = ball.transform.GetChild (0).GetComponent<Text> ();
		ball.transform.position = new Vector2 (ball.transform.position.x + 72.25f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x - 0.5f, temp.sizeDelta.y - 0.5f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a - 0.6f);
		selected.color = new Color(selected.color.r, selected.color.g, selected.color.b, selected.color.a - 1f);
	}

	private IEnumerator rotateLeft(int i, GameObject[] balls) {
		if (balls [Mod((i + 2), balls.Length)].transform.position.x != rightPosBall)
			balls [Mod((i + 2), balls.Length)].transform.position = new Vector2 (rightPosBall, balls [Mod((i + 2), balls.Length)].transform.position.y);

		for (int x = 0; x < 10; x++) {
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


		stepLeftNoDelay (balls [Mod((i + 2), balls.Length)]);
		stepLeftAndGrowNoDelay (balls [Mod((i + 1), balls.Length)]);
		stepLeftAndShrinkNoDelay (balls [Mod(i, balls.Length)]);
		stepLeftNoDelay (balls [Mod((i - 1), balls.Length)]);
			yield return null;


	}
	private void stepLeft (GameObject ball) {
		ball.transform.position = new Vector2 (ball.transform.position.x - 7.225f, ball.transform.position.y);
	}

	private void stepLeftAndGrow (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		Text selected = ball.transform.GetChild (0).GetComponent<Text> ();
		ball.transform.position = new Vector2 (ball.transform.position.x - 7.225f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x + 0.05f, temp.sizeDelta.y + 0.05f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a + 0.06f);
		selected.color = new Color(selected.color.r, selected.color.g, selected.color.b, selected.color.a + 0.1f);
	}

	private void stepLeftAndShrink (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		Text selected = ball.transform.GetChild (0).GetComponent<Text> ();
		ball.transform.position = new Vector2 (ball.transform.position.x - 7.225f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x - 0.05f, temp.sizeDelta.y - 0.05f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a - 0.06f );
		selected.color = new Color(selected.color.r, selected.color.g, selected.color.b, selected.color.a - 0.1f);
	}

	private void stepLeftNoDelay (GameObject ball) {
		ball.transform.position = new Vector2 (ball.transform.position.x - 72.25f, ball.transform.position.y);
	}

	private void stepLeftAndGrowNoDelay (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		Text selected = ball.transform.GetChild (0).GetComponent<Text> ();
		ball.transform.position = new Vector2 (ball.transform.position.x - 72.25f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x + 0.5f, temp.sizeDelta.y + 0.5f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a + 0.6f);
		selected.color = new Color(selected.color.r, selected.color.g, selected.color.b, selected.color.a + 1f);
	}

	private void stepLeftAndShrinkNoDelay (GameObject ball) {
		RawImage temp2 = ball.GetComponent<RawImage> ();
		RectTransform temp = ball.GetComponent<RectTransform> ();
		Text selected = ball.transform.GetChild (0).GetComponent<Text> ();
		ball.transform.position = new Vector2 (ball.transform.position.x - 72.25f, ball.transform.position.y);
		temp.sizeDelta = new Vector2 (temp.sizeDelta.x - 0.5f, temp.sizeDelta.y - 0.5f);
		temp2.color = new Color(temp2.color.r, temp2.color.g, temp2.color.b, temp2.color.a - 0.6f );
		selected.color = new Color(selected.color.r, selected.color.g, selected.color.b, selected.color.a - 1f);
	}

	private void selectColor(int player, int playersIndex) {
		if (!isSelected [player - 1]) {
			int oldSelected = playersSelectedColor [player - 1];
			selectedColors [oldSelected] = false;

			player1Balls [oldSelected].transform.GetChild(0).GetComponent<Text>().text = "";
			player2Balls [oldSelected].transform.GetChild(0).GetComponent<Text>().text = "";
			player3Balls [oldSelected].transform.GetChild(0).GetComponent<Text>().text = "";
			player4Balls [oldSelected].transform.GetChild(0).GetComponent<Text>().text = "";

		}
		selectedColors [playersIndex] = true;
		isSelected [player - 1] = true;
		playersSelectedColor [player - 1] = playersIndex;

		string[] temp = new string[selectedColors.Length];
		for (int i = 0; i < selectedColors.Length; i++) {
			temp[i] = "PLAYER " + player.ToString();
		}
		player1Balls [playersIndex].transform.GetChild(0).GetComponent<Text>().text = temp[0];
		player2Balls [playersIndex].transform.GetChild(0).GetComponent<Text>().text = temp[1];
		player3Balls [playersIndex].transform.GetChild(0).GetComponent<Text>().text = temp[2];
		player4Balls [playersIndex].transform.GetChild(0).GetComponent<Text>().text = temp[3];
		switch (player) {
			case 1:
				player1Balls [playersIndex].transform.GetChild(0).GetComponent<Text>().text = "SELECTED";
				break;
			case 2:
				player2Balls [playersIndex].transform.GetChild(0).GetComponent<Text>().text = "SELECTED";
				break;
			case 3: 
				player3Balls [playersIndex].transform.GetChild(0).GetComponent<Text>().text = "SELECTED";
				break;
			case 4:
				player4Balls [playersIndex].transform.GetChild(0).GetComponent<Text>().text = "SELECTED";
				break;
		}
	}

	private int Mod(int a, int b){
		return (a % b + b) % b;
	}
}
