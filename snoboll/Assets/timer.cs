using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
	private int counter = 60; //Fixed update counter for 1 sec.
	private int year = 2016; //Starting year
	private int stopYear = GameWideScript.Instance.setTime; //Stopping year
	private Text text;
    public static bool past = false;
    
	// Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }		
	void FixedUpdate(){
		if (!past) {
			text.text = "Year: " + year.ToString();
			if (counter != 0) {
				counter--;	
			} 
			else {
				if (year == stopYear) {
					text.text = "done";
					SceneManager.LoadScene ("slut");
				} else {
					year++;
					counter = 60;
				}
			}
		}

	}


}