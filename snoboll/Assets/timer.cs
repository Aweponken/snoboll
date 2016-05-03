using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
/// <summary>
/// Script to handle the in-game timer
/// </summary>
public class timer : MonoBehaviour
{
	private int counter = 60; //Fixed update counter for 1 sec.
	private int year = 2016; //Starting year
	private int stopYear = GameWideScript.Instance.setTime; //Stopping year
	private Text text;
	/// <summary>
	/// boolean activated when the time is up
	/// </summary>
    public static bool past = false;
	public bool scoreBoard = false;
	public static int onlyOne = 1;
	// Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }		
	void FixedUpdate(){
		if (onlyOne>=GameWideScript.Instance.setPlayers) {
			//GameObject.Find ("mainScene").GetComponent<Camera> ().enabled = true;
			Time.timeScale = 0;
			Application.LoadLevelAdditive ("slut");
			scoreBoard = true;
		}
		if (!past) {
			text.text = "Year: " + year.ToString();
			if (counter != 0) {
				counter--;	
			} 
			else {
				if (year == stopYear && !GameWideScript.Instance.setKrymp) {
					text.text = "done";
					if (!scoreBoard) {
						//GameObject.Find ("mainScene").GetComponent<Camera> ().enabled = true;
						Time.timeScale = 0;
						Application.LoadLevelAdditive ("slut");
						scoreBoard = true;
					}
				} else {
					year++;
					counter = 60;
				}
			}
		}

	}
}