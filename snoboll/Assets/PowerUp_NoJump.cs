using UnityEngine;
using System.Collections;
/// <summary>
/// Script that handles no jump power up
/// </summary>
public class PowerUp_NoJump : MonoBehaviour
{
<<<<<<< HEAD

	AudioClip FX;

    void Start()
    {
		FX = GetComponent<AudioSource> ().clip;
=======
	GameObject noJump3d;
    // Use this for initialization
    void Start()
    {
		noJump3d = GameObject.Find("powNoJump");
>>>>>>> refs/remotes/origin/disco
        float left = Camera.main.gameObject.transform.position.x
         - ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) + 100;
        float right = Camera.main.gameObject.transform.position.x
            + ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) - 100;
        float top = Camera.main.gameObject.transform.position.y
            + ((2f * Camera.main.orthographicSize) / 2) - 15;
        float bott = Camera.main.gameObject.transform.position.y
            - ((2f * Camera.main.orthographicSize) / 2) + 15;
        gameObject.transform.position = new Vector2(Random.Range(left, right), Random.Range(bott, top));
<<<<<<< HEAD
=======

    }

    // Update is called once per frame
    void Update()
    {
		noJump3d.transform.position = new Vector3 (transform.position.x+12.5f,transform.position.y+12.5f,30);
>>>>>>> refs/remotes/origin/disco
    }
		
    void OnCollisionEnter2D(Collision2D coll)
    {
<<<<<<< HEAD
		if (coll.gameObject.tag == "Boll")
		{
			AudioSource.PlayClipAtPoint (FX, new Vector2(0,0));
			GameObject snoBoll = GameObject.Find("Boll");
			snoBoll.SendMessage ("noJump");
=======
        if (coll.gameObject.name == "Boll" || coll.gameObject.name == "Boll 2" || coll.gameObject.name == "Boll 3" || coll.gameObject.name == "Boll 4")
        {
            GameObject snoBoll = GameObject.Find("Boll");
            SnoBoll script = (SnoBoll)snoBoll.GetComponent(typeof(SnoBoll));
            script.noJump();
			noJump3d.transform.position = new Vector3 (-30,-30,-30); 
>>>>>>> refs/remotes/origin/disco
			gameObject.SetActive(false);
		}
    }
}