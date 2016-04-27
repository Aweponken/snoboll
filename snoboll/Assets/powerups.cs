using UnityEngine;
using System.Collections;

public class powerups : MonoBehaviour {

    public int rndm_time;
    public GameObject Pow;
 

	// Use this for initialization
	void Start () {
        rndm_time = Random.Range(500, 1000);
        int a = Random.Range(1, 3);
        if (a == 1)
        {
            Pow = GameObject.Find("PowerUp_Inv");
        }
        else
        {
            Pow = GameObject.Find("PowerUp_Slower_Faster");
        }
        Pow.active = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rndm_time <= 0 && !(Pow.active))
        {
            float left = Camera.main.gameObject.transform.position.x
                 - ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) + 100;
            float right = Camera.main.gameObject.transform.position.x
                + ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) - 100;
            float top = Camera.main.gameObject.transform.position.y
                + ((2f * Camera.main.orthographicSize) / 2) - 15;
            float bott = Camera.main.gameObject.transform.position.y
                - ((2f * Camera.main.orthographicSize) / 2) + 15;
            Pow.transform.position = new Vector2(Random.Range(left, right), Random.Range(bott, top));
            Pow.active = true;
            rndm_time = Random.Range(500, 1000);
        }
        if(!Pow.active)
            rndm_time--;
	}
}
