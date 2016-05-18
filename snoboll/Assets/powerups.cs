using UnityEngine;
using System.Collections;
/// <summary>
/// script that handles powerup spawning
/// </summary>
public class powerups : MonoBehaviour {
	/// <summary>
	/// sets time between spawns
	/// </summary>
    public int rndm_time;
	/// <summary>
	/// variable that handles different kids of powerups
	/// </summary>
    public GameObject Pow, Pow1,Pow2, Pow3, Pow4;
	/// <summary>
	/// decides wich will be the next powerup to spawn
	/// </summary>
    public static int a;
	private int count;


	// Use this for initialization
	void Start () {

        a = Random.Range(1, 5);
 		rndm_time = Random.Range (200, 800);
		//rndm_time = Random.Range(GameWideScript.Instance.setminPowUpTime, GameWideScript.Instance.setmaxPowUpTime);
        Pow1 = GameObject.Find("PowerUp_Inv");
        Pow2 = GameObject.Find("PowerUp_Shield");
        Pow3 = GameObject.Find("PowerUp_SpeedSlow");
        Pow4 = GameObject.Find("PowerUp_NoJump");

       

		Pow1.SetActive(false);
		Pow2.SetActive(false);
		Pow3.SetActive(false);
		Pow4.SetActive(false);


    }

	// Update is called once per frame
	void FixedUpdate () {
        
		if (rndm_time <= 0 && !(Pow.activeSelf)) {
			if (GameWideScript.Instance.setPow) {
				if (count >= GameWideScript.Instance.setPowUpDelay) {
					if (a == 1) {
						Pow1.SetActive (true);
						Pow = Pow1;

					} else if (a == 2) {
						Pow2.SetActive (true);
						Pow = Pow2;
					} else if (a == 3) {
						Pow3.SetActive (true);
						Pow = Pow3;
					} else {
						Pow4.SetActive (true);
						Pow = Pow4;
					}
					float left = Camera.main.gameObject.transform.position.x
					             - ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) + 100;
					float right = Camera.main.gameObject.transform.position.x
					              + ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) - 100;
					float top = Camera.main.gameObject.transform.position.y
					            + ((2f * Camera.main.orthographicSize) / 2) - 15;
					float bott = Camera.main.gameObject.transform.position.y
					             - ((2f * Camera.main.orthographicSize) / 2) + 15;
					Pow.transform.position = new Vector2 (Random.Range (left, right), Random.Range (bott, top));
					Pow.SetActive (true);
					count = 0;
				} 
				else 
					count += 5;
				//rndm_time = Random.Range (GameWideScript.Instance.setminPowUpTime, GameWideScript.Instance.setmaxPowUpTime);
				rndm_time = Random.Range (200, 800);

			}
		}
		
        a = Random.Range(1, 5);
        
        if(!Pow.activeSelf)
            rndm_time--;
		tel();
	}
	private void tel()
	{ 
		
		float top = Camera.main.gameObject.transform.position.y
			+ ((2f * Camera.main.orthographicSize) / 2) + Pow.transform.localScale.y/2;
		float bott = Camera.main.gameObject.transform.position.y
			- ((2f * Camera.main.orthographicSize) / 2) - Pow.transform.localScale.y/2;

	
		if (Pow.transform.position.y > top)
		{
			Pow.transform.position = new Vector2(Pow.transform.position.x, bott);

		}
		if (Pow.transform.position.y < bott)
		{
			Pow.transform.position = new Vector2(Pow.transform.position.x, top);

		}
	}

}
