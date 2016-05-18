
using UnityEngine;
using System.Collections;
/// <summary>
/// Script that handles Power up faster slower.
/// </summary>
public class PowerUp_Faster_Slower : MonoBehaviour {

	AudioSource[] FX;
    public static int slowerFasterSound;

	void Start () {
		FX = GetComponents<AudioSource>();
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
            coll.gameObject.SendMessage("SlowerFasterF");
            if (slowerFasterSound == 0)
            {
                AudioSource.PlayClipAtPoint(FX[0].clip, new Vector2(0, 0));
            }
            else
            {
                AudioSource.PlayClipAtPoint(FX[1].clip, new Vector2(0, 0));
            }
            
			
			gameObject.SetActive(false);
		}
    }
}
