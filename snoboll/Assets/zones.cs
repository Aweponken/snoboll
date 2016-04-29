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
		if (GameWideScript.Instance.setZone) {
			if (counter == 0) {
				counter = Random.Range (minTime, maxTime);
			
				activateZone ();	
			} else {
				counter--;
			}
			if (counterSecondZone == 0) {
				counterSecondZone = Random.Range (minTime, maxTime);
			
				activateZone ();	
			} else {
				counterSecondZone--;
			}
		}  
	}

	void activateZone(){
		int rand = Random.Range(0, zonesArray.Length);

		if(zonesArray[rand].active == false){
			zonesArray[rand].active = true;
            zonesArray[rand].transform.position = new Vector3(zonesArray[rand].transform.position.x + 0.1f, zonesArray[rand].transform.position.y, 0);
            zonesArray[rand].transform.position = new Vector3(zonesArray[rand].transform.position.x - 0.1f, zonesArray[rand].transform.position.y, 0);
            isTwoActive++;
		}
	}
}
