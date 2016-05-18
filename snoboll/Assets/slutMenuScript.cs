using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
/// <summary>
/// script that handles the scoreboard
/// </summary>
public class slutMenuScript : MonoBehaviour
{
	/// <summary>
	/// button to restart game
	/// </summary>
    public Button restartText;
	/// <summary>
	/// button that links to main menu
	/// </summary>
    public Button mainmenuText;
	/// <summary>
	/// the actual scoreboard
	/// </summary>
    public Text winner;

    private string display = "";

    List<GameWideScript.Player> chatEvents;

    private bool callMe;

    /// <summary>
    /// Create a list with all player objects (to be displayed on the scoreboard) and initiates the Restart and Main menu button.
    /// </summary>
    void Start()
    {
        chatEvents = new List<GameWideScript.Player>();
		chatEvents.Add (GameWideScript.Player1);
		chatEvents.Add (GameWideScript.Player2);
		if (GameWideScript.Instance.setPlayers > 2) {
			chatEvents.Add (GameWideScript.Player3);
		}
		if (GameWideScript.Instance.setPlayers > 3) {
			chatEvents.Add (GameWideScript.Player4);
		}

        callMe = true;

        restartText = restartText.GetComponent<Button>();
        mainmenuText = mainmenuText.GetComponent<Button>();
        restartText.Select();
    }

    void Update()
    {
        if (callMe)
        {
            Sort();
            AddText();
            callMe = false;
			GameWideScript.Instance.EndOfGame = true;
        }
    }

    /// <summary>
    /// Sorts the list of players depending on their sizes
    /// </summary>

    void Sort()
    {
        GameWideScript.Player temp = null;
        List<GameWideScript.Player> res = new List<GameWideScript.Player>();
        while (chatEvents.Count != 0)
        {
            foreach (GameWideScript.Player msg in chatEvents)
            {
                if (temp == null)
                {
                    temp = msg;
                }
                else if (temp.size < msg.size)
                {
                    temp = msg;
                }
            }
            res.Add(temp);
            chatEvents.Remove(temp);
            temp = null;
        }
        chatEvents = res;
    }

    /// <summary>
    /// Adds the players and their sizes to the scoreboard canvas in descending order
    /// </summary>

    void AddText()
    {
        foreach (GameWideScript.Player msg in chatEvents)
		{
            int score = (int)Mathf.Round(msg.size); 
			display = display.ToString() + msg.Color.ToString() + " " + score.ToString() + "\n";
        }
        winner.text = display;
    }



    /// <summary>
    /// If triggered, goto scene "Map".
    /// </summary>
    public void StartLevel() //this function will be used on our Play button

    {	
		GameWideScript.Instance.EndOfGame = false;
		Time.timeScale = 1;
		timer.onlyOne = 1;

        if (GameWideScript.Instance.setMap == 0)
            SceneManager.LoadScene("Map"); //this will load our first level from our build settings. "1" is the second scene in our game
        else if (GameWideScript.Instance.setMap == 1)
            SceneManager.LoadScene("Map2"); //this will load our first level from our build settings. "1" is the second scene in our game
		else if(GameWideScript.Instance.setMap == 2)
            SceneManager.LoadScene("Map3"); //this will load our first level from our build settings. "1" is the second scene in our game
		else
			SceneManager.LoadScene("Map4");
    }
    /// <summary>
    /// If triggered, open options.
    /// </summary>
    public void ExitGame() //This function will be used on our "Yes" button in our Quit menu

    {
        GameWideScript.Instance.EndOfGame = false;
        timer.onlyOne = 1;
        Time.timeScale = 1;
        timer.past = false;
        SceneManager.LoadScene("Options");
    }
}