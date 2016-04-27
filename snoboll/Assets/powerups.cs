using UnityEngine;
using System.Collections;

public class powerups : MonoBehaviour {

    public int rndm_time;
    public GameObject Pow, Pow1,Pow2, Pow3, Pow4;
    public static int a = Random.Range(1, 5);

    private BoxCollider2D InvCollider;


	// Use this for initialization
	void Start () {
        rndm_time = Random.Range(100, 200);
        Pow1 = GameObject.Find("PowerUp_Inv");
        Pow2 = GameObject.Find("PowerUp_Shield");
        Pow3 = GameObject.Find("PowerUp_SpeedSlow");
        Pow4 = GameObject.Find("PowerUp_NoJump");

       

        Pow1.active = false;
        Pow2.active = false;
        Pow3.active = false;
        Pow4.active = false;


    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if (rndm_time <= 0 && !(Pow.active))
        {
            if (a == 1)
            {
                Pow1.active = true;
                Pow = Pow1;

            }
            else if (a == 2)
            {
                Pow2.active = true;
                Pow = Pow2;
            }

            else if (a == 3)
            {
                Pow3.active = true;
                Pow = Pow3;
            }
            else
            {
                Pow4.active = true;
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
            Pow.transform.position = new Vector2(Random.Range(left, right), Random.Range(bott, top));
            Pow.active = true;

            rndm_time = Random.Range(100, 200);

        }

        a = Random.Range(1, 5);
        if(!Pow.active)
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
