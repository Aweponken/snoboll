using UnityEngine;
using System.Collections;
/// <summary>
/// script to handle the spawning of zones
/// </summary>
public class zones : MonoBehaviour {

	private GameObject[] zonesArray;
	/// <summary>
	/// int to check if there are already two zones active
	/// --- if 2; no more zones will spawn until this variable is decreased  
	/// </summary>
	public static int isTwoActive = 0;
	/// <summary>
	/// sets the max time between zones
	/// </summary>
	[SerializeField]
	private int maxTime;
	/// <summary>
	/// sets the min time between zones
	/// </summary>
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
			i.SetActive(false);
            zone script = (zone)i.GetComponent(typeof(zone));
            i.SetActive(false);
            script.snowF();
            script.molnF();
            script.fireF();
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
        


        if (zonesArray[rand].active == false){
            zone script = (zone)zonesArray[rand].GetComponent(typeof(zone));
            zonesArray[rand].SetActive(true);
            zonesArray[rand].transform.position = new Vector3(zonesArray[rand].transform.position.x + 0.1f, zonesArray[rand].transform.position.y, 0);
            zonesArray[rand].transform.position = new Vector3(zonesArray[rand].transform.position.x - 0.1f, zonesArray[rand].transform.position.y, 0);
            isTwoActive++;
            script.decideType();

        }
	}
	
	void activateZoneByIndex(int i){

        int rand = Random.Range(0, zonesArray.Length);

        if (zonesArray[i].active == false){
            zonesArray[i].SetActive(true);
			zonesArray[i].transform.position = new Vector3(zonesArray[i].transform.position.x + 0.1f, zonesArray[i].transform.position.y, 0);
			zonesArray[i].transform.position = new Vector3(zonesArray[i].transform.position.x - 0.1f, zonesArray[i].transform.position.y, 0);
			isTwoActive++;
		}
	}
}
