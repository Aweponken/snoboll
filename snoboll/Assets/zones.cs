using UnityEngine;
using System.Collections;

public class zones : MonoBehaviour {

	private GameObject[] zonesArray;
	public static int isTwoActive = 0;
	[SerializeField]
	private int maxTime;
	[SerializeField]
	private int minTime;
	private float counter;
	private float counterSecondZone;
   

	// Use this for initialization
	void Start () {
       
		counter = Random.Range(minTime,maxTime);
		counterSecondZone = Random.Range(minTime,maxTime);
		zonesArray = GameObject.FindGameObjectsWithTag("Zone");
		foreach(GameObject i in zonesArray) {
			i.active = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(counter == 0){
			counter = Random.Range(minTime,maxTime);
			
			activateZone();	
		}
		else {
			counter--;
		}
		if(counterSecondZone == 0){
			counterSecondZone = Random.Range(minTime,maxTime);
			
			activateZone();	
		}
		else {
			counterSecondZone--;
		}
        
	}

	void activateZone(){
		int rand = Random.Range(0, zonesArray.Length - 1);

		if(zonesArray[rand].active == false){
			zonesArray[rand].active = true;
			isTwoActive++;
		}
	}
}
