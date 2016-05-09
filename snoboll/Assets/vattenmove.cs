using UnityEngine;
using System.Collections;

public class vattenmove : MonoBehaviour {
	// Use this for initialization
    int count = 0;
    int pount = 0;
    bool up = true;
    bool pup = true;
    [SerializeField]
    GameObject ice;
    [SerializeField]
    GameObject p1;
    [SerializeField]
    GameObject p2;
    [SerializeField]
    GameObject p3;
    [SerializeField]
    GameObject berg;
    [SerializeField]
    GameObject baut;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (count < 200 && up)
        {
            baut.transform.Rotate(0, -0.01f, 0);
            berg.transform.Rotate(0,0.01f,0);
            transform.localScale = new Vector3(transform.localScale.x + 0.015f, transform.localScale.y + 0.015f, transform.localScale.z);
            ice.transform.position = new Vector3(ice.transform.position.x, ice.transform.position.y + 0.01f, ice.transform.position.z);
            p1.transform.position = new Vector3(p1.transform.position.x, p1.transform.position.y + 0.01f, p1.transform.position.z);
            p2.transform.position = new Vector3(p2.transform.position.x, p2.transform.position.y + 0.01f, p2.transform.position.z);
            p3.transform.position = new Vector3(p3.transform.position.x, p3.transform.position.y + 0.01f, p3.transform.position.z);
            count++;
        }
        else if (count < 200)
        {
            baut.transform.Rotate(0, 0.01f, 0);
            berg.transform.Rotate(0, -0.01f, 0);
            transform.localScale = new Vector3(transform.localScale.x - 0.015f, transform.localScale.y - 0.015f, transform.localScale.z);
            ice.transform.position = new Vector3(ice.transform.position.x, ice.transform.position.y - 0.01f, ice.transform.position.z);
            p1.transform.position = new Vector3(p1.transform.position.x, p1.transform.position.y - 0.01f, p1.transform.position.z);
            p2.transform.position = new Vector3(p2.transform.position.x, p2.transform.position.y - 0.01f, p2.transform.position.z);
            p3.transform.position = new Vector3(p3.transform.position.x, p3.transform.position.y - 0.01f, p3.transform.position.z);
            count++;
        }
        else
        {
            count = 0;
            up = !up;
        }
        if (pount < 15 && pup)
        {
            p1.transform.Rotate(0, 0, 0.2f);
            p2.transform.Rotate(0, 0, -0.3f);
            p3.transform.Rotate(0, 0, 0.2f);
            pount++;
        }
        else if (pount < 15)
        {
            p1.transform.Rotate(0, 0, -0.2f);
            p2.transform.Rotate(0, 0, 0.3f);
            p3.transform.Rotate(0, 0, -0.2f);
            pount++;
        }
        else
        {
            pount = 0;
            pup = !pup;
        }
	}
}
