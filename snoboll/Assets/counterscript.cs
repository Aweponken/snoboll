using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class counterscript : MonoBehaviour {

    public Text textOutput;
    private int counter=0,number=3;
    public CanvasGroup canvasGroup;



    // Use this for initialization
    void Start () {

        Time.timeScale = 0;
        timer.past = false;

    }
	
	// Update is called once per frame
	void Update () {
        counter++;
        textOutput.fontSize  +=10;
        if (counter > 50)
        {
            if(number == 0 && counter > 60)
            {
                Time.timeScale = 1;
                timer.past = true;
                canvasGroup.alpha = 0;
            }
            else if (number==1)
            {
                textOutput.text = "GO!";
                number--;
            }
            else if ( number > 1)
            {
                number--;
                textOutput.fontSize = 0;
                textOutput.text = number.ToString();
                counter = 0;
            }
        }

	}
    
}
