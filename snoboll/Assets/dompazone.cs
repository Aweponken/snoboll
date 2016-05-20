using UnityEngine;
using System.Collections;
/// <summary>
/// script to handle zones
/// </summary>
public class dompazone : MonoBehaviour {

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
	private ParticleSystem eld2;
	[SerializeField]
	private ParticleSystem snow2;
	[SerializeField]
	private GameObject ishink;
	[SerializeField]
	private GameObject dompa;

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
			molnF2();
			fireF();
			gameObject.SetActive(false);
			DompaZones.isTwoActive--;
		} 
		else {

			counter--;
		}
	}

	/// <summary>
	/// decide whether the zone is warm or cold depending on random number(0,1)
	/// </summary>
	public void decideType()
	{


		type = Random.Range(0, 2);
		if (type == 0) //kallt område
		{	
			
			snow.gameObject.SetActive(true);
			snow2.gameObject.SetActive(true);
			ishink.SetActive(true);
			snow.Play();    
			snow2.Play();
		
		}
		else   // varmt område
		{

	
			eld.gameObject.SetActive(true);
			eld2.gameObject.SetActive(true);
			dompa.SetActive(true);
			eld.Play();
			eld2.Play();
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
						print ("BOLL 3");
						GameObject snoBoll = GameObject.Find("Boll 3");
						SnoBoll3 script = (SnoBoll3)snoBoll.GetComponent(typeof(SnoBoll3));
						script.updateRad(1);
					}
					else if (boll.gameObject.name == "Boll 4")
					{
						print ("Boll 4");
						GameObject snoBoll = GameObject.Find("Boll 4");
						SnoBoll4 script = (SnoBoll4)snoBoll.GetComponent(typeof(SnoBoll4));
						script.updateRad (1);
					}
				}

			}
		}
	}

	public void fireF()
	{
		eld.Stop();
		eld2.Stop ();
	}
	public void snowF()
	{
		snow.Stop();
		snow2.Stop ();
	}
	public void molnF()
	{
		ishink.SetActive(false);
	}
	public void molnF2()
	{
		dompa.SetActive(false);
	}


}
