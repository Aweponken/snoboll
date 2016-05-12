using UnityEngine;
using System.Collections;

public class Cloud_animering : MonoBehaviour
{


    [SerializeField]
    GameObject B1, B2, B3, B4, B5, B6, lavasky;

    [SerializeField]
    Material golv;

    int count = 0;
    long skycount = 0;
    int Bcount;
    // Use this for initialization
    void Start()
    {
        B1.SetActive(false);
        B2.SetActive(false);
        B3.SetActive(false);
        B4.SetActive(false);
        B5.SetActive(false);
        B6.SetActive(false);

        Bcount = Random.Range(100, 800);


    }

    // Update is called once per frame
    void Update()
    {
        if (skycount % 64 < 32)
        golv.EnableKeyword("_EMISSION");
        else if (skycount % 64 < 40)
        golv.DisableKeyword("_EMISSION");
        count++;
        skycount++;
        if (skycount % 800 < 400)
            lavasky.transform.localScale = new Vector3(lavasky.transform.localScale.x - 0.01f, lavasky.transform.localScale.y + 0.01f, lavasky.transform.localScale.z + 0.01f);
        else
            lavasky.transform.localScale = new Vector3(lavasky.transform.localScale.x + 0.01f, lavasky.transform.localScale.y - 0.01f, lavasky.transform.localScale.z - 0.01f);
        transform.Rotate(0, 0, 0.02f);
        if (Bcount < count)
        {
            switch (Random.Range(0, 6))
            {
                case 0:
                    StartCoroutine(delay(B1));
                    break;
                case 1:
                    StartCoroutine(delay(B2));
                    break;
                case 2:
                    StartCoroutine(delay(B3));
                    break;
                case 3:
                    StartCoroutine(delay(B4));
                    break;
                case 4:
                    StartCoroutine(delay(B5));
                    break;
                case 5:
                    StartCoroutine(delay(B6));
                    break;

            }
            Bcount = Random.Range(100, 800);
            count = 0;
        }
    }

    public IEnumerator delay(GameObject B)
    {
        B.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        B.SetActive(false);
    }
}
