using UnityEngine;
using System.Collections;

public class PowerUp_Inv_scr : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Boll 2")
        {
            StartCoroutine(wfs1());
        }

        if (coll.gameObject.name == "Boll")
        {
            StartCoroutine(wfs2());
        }
    }
    IEnumerator wfs1()
    {
        SnoBoll.PowerUp_Inv = true;
        yield return new WaitForSeconds(5);
        SnoBoll.PowerUp_Inv = false;
    }
    IEnumerator wfs2()
    {
        SnoBoll2.PowerUp_Inv = true;
        yield return new WaitForSeconds(5);
        SnoBoll2.PowerUp_Inv = false;
    }
}
