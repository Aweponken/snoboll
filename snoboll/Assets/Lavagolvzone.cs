using UnityEngine;
using System.Collections;
/// <summary>
/// script to handle zones
/// </summary>
public class Lavagolvzone : MonoBehaviour {

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
       

      
	}

  
    void OnTriggerStay2D(Collider2D other)
    {    
        GameObject boll = other.gameObject;
        if (boll.name.Contains("Boll"))
        {

           
			if (boll.transform.localScale.x > 30)
			{
				boll.transform.localScale = new Vector3(boll.transform.localScale.x - changeSizeSummer, boll.transform.localScale.x - changeSizeSummer, boll.transform.localScale.z);
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
