using UnityEngine;
using System.Collections;
/// <summary>
/// script to handle zones
/// </summary>
public class zone : MonoBehaviour {

	private int counter;
    private int type;
    private SpriteRenderer myZone;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    [SerializeField]
	/// <summary>
	/// The change size winter.
	/// </summary>
    private float changeSizeWinter;
    [SerializeField]
    /// <summary>
    /// The change size summer.
    /// </summary>
	private float changeSizeSummer;


    // Use this for initialization
    void Start () {

        myZone = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
        type = Random.Range(0, 2);
        decideType();
        counter = 400;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(counter == 0){
            //Disable
            type = Random.Range(0, 2);
            decideType();
			counter = 400;
			gameObject.SetActive(false);
			zones.isTwoActive--;
		} 
		else {
			counter--;
		}
	}

    /// <summary>
    /// decide whether the zone is warm or cold depending on random number(0,1)
    /// </summary>
    void decideType()
    {
        if (type == 0) //kallt område
        {
            myZone.material.shader = shaderGUItext;
            myZone.color = Color.blue;

        }
        else   // varmt område
        {
            myZone.material.shader = shaderGUItext;
            myZone.color = Color.red;

        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        
      
        
        GameObject boll = other.gameObject;
        if (boll.name.Contains("Boll"))
        {

            if (type == 0)
            {
                if (boll.transform.localScale.x < 170)
                {
                    boll.transform.localScale = new Vector3(boll.transform.localScale.x + changeSizeWinter, boll.transform.localScale.x + changeSizeWinter, 0);
                }

            }
            else
            {
                if (boll.transform.localScale.x > 30)
                {
					boll.transform.localScale = new Vector3(boll.transform.localScale.x - changeSizeSummer, boll.transform.localScale.x - changeSizeSummer, 0);
					if (boll.gameObject.name == "Boll")
					{
						GameObject snoBoll = GameObject.Find("Boll");
						SnoBoll script = (SnoBoll)snoBoll.GetComponent(typeof(SnoBoll));
						script.updateRad();
					}
					else if (boll.gameObject.name == "Boll 2")
					{
						GameObject snoBoll = GameObject.Find("Boll 2");
						SnoBoll2 script = (SnoBoll2)snoBoll.GetComponent(typeof(SnoBoll2));
						script.updateRad();
					}
					else if (boll.gameObject.name == "Boll 3")
					{
						print ("BOLL 3");
						GameObject snoBoll = GameObject.Find("Boll 3");
						SnoBoll3 script = (SnoBoll3)snoBoll.GetComponent(typeof(SnoBoll3));
						script.updateRad();
					}
					else if (boll.gameObject.name == "Boll 4")
					{
						print ("Boll 4");
						GameObject snoBoll = GameObject.Find("Boll 4");
						SnoBoll4 script = (SnoBoll4)snoBoll.GetComponent(typeof(SnoBoll4));
						script.updateRad ();
					}
				}

            }
        }
    }
}
