using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class slutMenuScript : MonoBehaviour
{
    public Button restartText;
    public Button mainmenuText;

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

        chatEvents.Add(GameWideScript.Player1);
        chatEvents.Add(GameWideScript.Player2);
        chatEvents.Add(GameWideScript.Player3);
        chatEvents.Add(GameWideScript.Player4);


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
            if (msg.size > 0)
            {
                display = display.ToString() + msg.name.ToString() + " " + msg.size.ToString() + "\n";
            }
        }
        winner.text = display;
    }



    /// <summary>
    /// If triggered, goto scene "Map".
    /// </summary>
    public void StartLevel() //this function will be used on our Play button

    {
        SceneManager.LoadScene("Map"); //this will load our first level from our build settings. "1" is the second scene in our game

    }
    /// <summary>
    /// If triggered, shutdown the game.
    /// </summary>
    public void ExitGame() //This function will be used on our "Yes" button in our Quit menu

    {
        SceneManager.LoadScene("start"); //this will quit our game. Note this will only work after building the game

    }
}