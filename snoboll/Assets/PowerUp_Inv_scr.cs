using UnityEngine;
using System.Collections;
/// <summary>
/// script for invert-power up
/// </summary>
 
public class PowerUp_Inv_scr : MonoBehaviour {

	AudioClip FX;

	void Start () 
    {
		FX = GetComponent<AudioSource> ().clip;
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

    void OnCollisionEnter2D(Collision2D coll)
    {		
        if (coll.gameObject.tag == "Boll")
        {
			AudioSource.PlayClipAtPoint (FX, new Vector2(0,0));
			coll.gameObject.SendMessage ("inv");
			gameObject.SetActive(false);
        }
    }

    IEnumerator wfs1()
    {
        SnoBoll.PowerUp_Inv = true;
        yield return new WaitForSeconds(5);
        SnoBoll.PowerUp_Inv = false;
    }
}