﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{

    public Text text;
    public static int Mins = 1;
    public static int Secs = 0;
    public TimeSpan span = new TimeSpan(Mins, Secs, 0);
    public TimeSpan span2 = new TimeSpan(0, 0, 1);
    public static bool past = false;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       
       if(!past) {
        if (span == TimeSpan.Zero) 
        {
                //text.fontSize = 20;
                text.text = "done";
            SceneManager.LoadScene("slut");
        }
        else
            {
                span = span.Subtract(span2); //räknar ner i hundradelar
                text.text = span.ToString();

            }
      }
    }
}