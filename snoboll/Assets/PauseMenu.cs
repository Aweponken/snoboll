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
    void Start()
    {
        isPaused = false;
        clicked = false;
        resumeGame = resumeGame.GetComponent<Button>();
        startMenu = startMenu.GetComponent<Button>();
       
    }

    // Update is called once per frame
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
    public void resume()
    {
        isPaused = false;
        timer.past = false; //nå variabel-past från timer.cs
        //DÖlj canvas (1)
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        //canvasGroup.blocksRaycasts = false;

        Time.timeScale = 1;
    }

    // Körs när vi pausar
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
    public void goToStartMenu()
    {
        Time.timeScale = 1;
        timer.past = false;
        SceneManager.LoadScene("start");
    }
}