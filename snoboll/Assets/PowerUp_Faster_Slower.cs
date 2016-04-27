
using UnityEngine;
using System.Collections;

public class PowerUp_Faster_Slower : MonoBehaviour {

	// Use this for initialization
	void Start () {

        float left = Camera.main.gameObject.transform.position.x
                 - ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) + 100;
        float right = Camera.main.gameObject.transform.position.x
            + ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) - 100;
        float top = Camera.main.gameObject.transform.position.y
            + ((2f * Camera.main.orthographicSize) / 2) - 15;
        float bott = Camera.main.gameObject.transform.position.y
            - ((2f * Camera.main.orthographicSize) / 2) + 15;
        gameObject.transform.position = new Vector2(Random.Range(left, right), Random.Range(bott, top));

    }
	
	// Update is called once per frame
	void FixedUpdate () {

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Boll")
        {
            GameObject snoBoll = GameObject.Find("Boll");
            SnoBoll script = (SnoBoll)snoBoll.GetComponent(typeof(SnoBoll));
            script.SlowerFasterF();
            gameObject.active = false;
        }

        if (coll.gameObject.name == "Boll 2")
        {
            GameObject snoBoll = GameObject.Find("Boll 2");
            SnoBoll2 script = (SnoBoll2)snoBoll.GetComponent(typeof(SnoBoll2));
            script.SlowerFasterF();
            gameObject.active = false;
        }
        if (coll.gameObject.name == "Boll 3")
        {
            print("BOLL 3");
            GameObject snoBoll = GameObject.Find("Boll 3");
            SnoBoll3 script = (SnoBoll3)snoBoll.GetComponent(typeof(SnoBoll3));
            script.SlowerFasterF();
            gameObject.active = false;
        }

        if (coll.gameObject.name == "Boll 4")
        {
            print("Boll 4");
            GameObject snoBoll = GameObject.Find("Boll 4");
            SnoBoll4 script = (SnoBoll4)snoBoll.GetComponent(typeof(SnoBoll4));
            script.SlowerFasterF();
            gameObject.active = false;
        }
    }
}
