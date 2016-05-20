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
    /// <summary>
    /// The change size summer.
    /// </summary>
    [SerializeField]
    private float changeSizeSummer;
    private float defSize;
    private float sizeWarm;
    private float defPos;
    private float posWarm;
    [SerializeField]
    private ParticleSystem eld;
    [SerializeField]
    private ParticleSystem snow;
    [SerializeField]
    private GameObject moln;

    // Use this for initialization
    void Start () {
        /*defSize = gameObject.transform.localScale.x;
        sizeWarm = defSize/2;
        defPos = gameObject.transform.position.x;
        posWarm = defPos + sizeWarm;
        */
        //decideType();

        counter = 400;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(counter == 0){
            //Disable
            type = Random.Range(0, 2);
           // decideType();
			counter = 400;
            snowF();
            molnF();
            fireF();
            gameObject.SetActive(false);
			zones.isTwoActive--;
		} 
		else {
            if (type == 0){
                if (counter >= 300)
                {
                    moln.transform.position = new Vector3 (moln.transform.position.x + 0.03f, moln.transform.position.y , moln.transform.position.z);
                }
               else if (counter >= 200)
                {
                    moln.transform.position = new Vector3(moln.transform.position.x - 0.03f, moln.transform.position.y, moln.transform.position.z);
                }
                else if (counter >= 100)
                {
                    moln.transform.position = new Vector3 (moln.transform.position.x - 0.03f, moln.transform.position.y , moln.transform.position.z);
                }
                else
                    moln.transform.position = new Vector3(moln.transform.position.x + 0.03f, moln.transform.position.y, moln.transform.position.z);
            }
			counter--;
		}
	}

    /// <summary>
    /// decide whether the zone is warm or cold depending on random number(0,1)
    /// </summary>
    public void decideType()
    {

        //myZone = gameObject.GetComponent<SpriteRenderer>();
        //shaderGUItext = Shader.Find("GUI/Text Shader");
        //shaderSpritesDefault = Shader.Find("Sprites/Default");
        type = Random.Range(0, 2);
        if (type == 0) //kallt område
        {
          //  myZone.material.shader = shaderGUItext;
            //myZone.color = Color.blue;
            //snow.gameObject.SetActive(true);
            moln.SetActive(true);
            snow.Play();        }
        else   // varmt område
        {
            
           // gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x/2, gameObject.transform.localScale.y);
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x + gameObject.transform.localScale.x, gameObject.transform.position.y, gameObject.transform.position.z);
           // myZone.material.shader = shaderGUItext;
          //  myZone.color = Color.red;
            eld.Play();
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
                        
					boll.transform.localScale = new Vector3(boll.transform.localScale.x + changeSizeWinter, boll.transform.localScale.x + changeSizeWinter, boll.transform.localScale.z);
					if (boll.gameObject.name == "Boll")
					{
						GameObject snoBoll = GameObject.Find("Boll");
						SnoBoll script = (SnoBoll)snoBoll.GetComponent(typeof(SnoBoll));
						script.updateRad(0);
					}
					else if (boll.gameObject.name == "Boll 2")
					{
						GameObject snoBoll = GameObject.Find("Boll 2");
						SnoBoll2 script = (SnoBoll2)snoBoll.GetComponent(typeof(SnoBoll2));
						script.updateRad(0);
					}
					else if (boll.gameObject.name == "Boll 3")
					{
						GameObject snoBoll = GameObject.Find("Boll 3");
						SnoBoll3 script = (SnoBoll3)snoBoll.GetComponent(typeof(SnoBoll3));
						script.updateRad(0);
					}
					else if (boll.gameObject.name == "Boll 4")
					{
						GameObject snoBoll = GameObject.Find("Boll 4");
						SnoBoll4 script = (SnoBoll4)snoBoll.GetComponent(typeof(SnoBoll4));
						script.updateRad (0);
					}
                }

            }
            else
            {
                if (boll.transform.localScale.x > 30)
                {
					boll.transform.localScale = new Vector3(boll.transform.localScale.x - changeSizeSummer, boll.transform.localScale.x - changeSizeSummer, boll.transform.localScale.z);
                    if (boll.gameObject.name == "Boll")
                    {
                        GameObject snoBoll = GameObject.Find("Boll");
                        SnoBoll script = (SnoBoll)snoBoll.GetComponent(typeof(SnoBoll));
                        script.updateRad(1);
                    }
                    else if (boll.gameObject.name == "Boll 2")
                    {
                        GameObject snoBoll = GameObject.Find("Boll 2");
                        SnoBoll2 script = (SnoBoll2)snoBoll.GetComponent(typeof(SnoBoll2));
                        script.updateRad(1);
                    }
                    else if (boll.gameObject.name == "Boll 3")
                    {
                        GameObject snoBoll = GameObject.Find("Boll 3");
                        SnoBoll3 script = (SnoBoll3)snoBoll.GetComponent(typeof(SnoBoll3));
                        script.updateRad(1);
                    }
                    else if (boll.gameObject.name == "Boll 4")
                    {
                        GameObject snoBoll = GameObject.Find("Boll 4");
                        SnoBoll4 script = (SnoBoll4)snoBoll.GetComponent(typeof(SnoBoll4));
                        script.updateRad(1);
                    }
                }

            }
        }
    }
    
    public void fireF()
    {
        eld.Stop();
    }
    public void snowF()
    {
        snow.Stop();
    }
    public void molnF()
    {
        moln.SetActive(false);
    }
   
    /*public void molnFunktionWithDelay()
    {
        StartCoroutine(molnPause());
    }

    IEnumerator molnPause()
    {  
        yield return new WaitForSeconds(2);
        moln.SetActive(false);
    }
    */
}
