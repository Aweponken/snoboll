using UnityEngine;
using System.Collections;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private bool isPaused;
    private bool clicked;
    public Button resumeGame;
    public Button startMenu;
    public CanvasGroup canvasGroup;


    // Use this for initialization
    /// <summary>
    /// Called once on start. Initialize variables
    /// </summary>
    void Start()
    {
        isPaused = false;
        clicked = false;
        resumeGame = resumeGame.GetComponent<Button>();
        startMenu = startMenu.GetComponent<Button>();   
    }

    // Update is called once per frame
    /// <summary>
    /// Runs every frame update. Here we check for click on cancel_menu input axis. If clicked we call paus/resume and also check for multible not intended pushes on Cancel_menu
    /// </summary>
    void Update()
    {
        //kolla efter tryck på esc
        if (Input.GetAxisRaw("Cancel_menu") != 0)
        {
            if (!clicked)
            {
                clicked = true;
                if (!isPaused)
                    paus();
                else
                    resume();
            }
        }
        if (Input.GetAxisRaw("Cancel_menu") == 0)
            clicked = false;
    }

    // Körs när vi klickar på resume
    /// <summary>
    /// Here we resume the game. That means resuming the timer, hiding the canvas group that holds the paus-menu, making that canvas group not interactable and also resetting the timescale
    /// </summary>
    public void resume()
    {
        isPaused = false;
        timer.past = false; //nå variabel-past från timer.cs
        //DÖlj canvas (1)
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;

        Time.timeScale = 1;
    }

    // Körs när vi pausar
    /// <summary>
    /// Here we paus the game. That means pausing the timer, showing the canvas group that holds the paus-menu, making that canvas group interactable and also stopping the timescale
    /// </summary>
    private void paus()
    {
        timer.past = true; //nå variabel past från timer.cs 
        isPaused = true;
        //Visa canvas(1)
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        //canvasGroup.blocksRaycasts = true;
        resumeGame.Select();

        Time.timeScale = 0;
    }

    // Körs när vi klickar på start menu
    /// <summary>
    /// This function is called "on click" from the StartMenuButton. Here we reset the timescale and timer and then change scene to "start". 
    /// </summary>
    public void goToStartMenu()
    {
        Time.timeScale = 1;
        timer.past = false;
        SceneManager.LoadScene("Options");
    }
}