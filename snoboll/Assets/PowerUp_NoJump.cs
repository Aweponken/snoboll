using UnityEngine;
using System.Collections;

public class PowerUp_NoJump : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Boll" || coll.gameObject.name == "Boll 2" || coll.gameObject.name == "Boll 3" || coll.gameObject.name == "Boll 4")
        {
            GameObject snoBoll = GameObject.Find("Boll");
            SnoBoll script = (SnoBoll)snoBoll.GetComponent(typeof(SnoBoll));
            script.noJump();
            gameObject.active = false;
        }
    }

}