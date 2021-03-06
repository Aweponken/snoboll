﻿using UnityEngine;
using System.Collections;
/// <summary>
/// script for player4
/// </summary>
public class SnoBoll4 : MonoBehaviour
{
	private bool isObj;
	private bool isVertical;
	private bool isHorizontal;
	private Rigidbody2D snoBoll; //pekare till snöboll1
	private CircleCollider2D snoBollCollider;
	private bool grounded;

	[SerializeField]
	public GameObject sköldRing;
	/// <summary>
	/// Boolean showing if the controls are inverted
	/// </summary>
	public static bool PowerUp_Inv = false;
	/// <summary>
	/// Boolean used to set the shield
	/// </summary>
	public static bool static_shield = false;
	/// <summary>
	/// Boolean showing if the shield is active
	/// </summary>
	public bool shield = false;
	/// <summary>
	/// Boolean showing if the player is able to jump
	/// </summary>
	public static bool ableToJump = true;
	/// <summary>
	/// the input for horizontal movement
	/// </summary>
	public static float horizontal;
	/// <summary>
	/// float used to set movement speed
	/// </summary>
    public static float slowerFaster = 1;
    Vector2 facing;


	/// <summary>
	/// Defines what layers are "ground"
	/// --jumpable--
	/// </summary>
	/// 
	[SerializeField]
	private GameObject tredBoll;
	[SerializeField]
	private LayerMask whatIsGround;

	[SerializeField] 
	private LayerMask whatIsAny;

	/// <summary>
	/// The ground.
	/// </summary>
	[SerializeField]
	private Transform ground;

	/// <summary>
	/// The ground radius.
	/// </summary>
	[SerializeField]
	private float groundRadius;
    [SerializeField]
    public Material BoostMesh;
    /// <summary>
    /// The movement speed.
    /// to be able to control speed from GUI:t
    /// </summary>
    [SerializeField]
	private float movementSpeed;

	/// <summary>
	/// The jump force.
	/// to be able to control jumpforce from GUI:t
	/// </summary>
	[SerializeField]
	private float jumpForce;

	[SerializeField]     //för att kunna styra Boostforce från GUI:t
	private float boostForce;
	[SerializeField]
	private float boostCooldown;
	private float boostStartTime = 0f;
	[SerializeField]
	private float boostDuration;
	/// <summary>
	/// Boolean showing if the boost is active
	/// </summary>
	public bool boosty = false;

	[SerializeField]
	private float maxSize = 220;
	[SerializeField]
	private float minSize = 30;
	[SerializeField]
	private float changeIfHit = 5;
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        sköldRing.SetActive(false);
        SnoBoll2.PowerUp_Inv = false;
        SnoBoll3.PowerUp_Inv = false;
        SnoBoll.PowerUp_Inv = false;
        shield = false;
        static_shield = false;
        tredBoll.GetComponent<TrailRenderer>().material = BoostMesh;
        shield = static_shield;
        boostStartTime = Time.time;
		GameWideScript.Player4.size = transform.localScale.x;
		snoBoll = GetComponent<Rigidbody2D>();
		snoBollCollider = GetComponent<CircleCollider2D>();

	}

	/// <summary>
	/// This funtion updates on a fixed time (50 times/sec(???)) 
	/// and gets the input from the controller/keyboard and calls 
	/// handleMovement with these arguments. It also checks wheather 
	/// the object is grounded or not. 
	/// </summary>
	void FixedUpdate () 
	{
		if (GameWideScript.Instance.setKrymp) {

			if (transform.localScale.x < 15) {
				Debug.Log ("boll1 dog");
				GameWideScript.Player4.size = 0;
				timer.onlyOne++;
				gameObject.SetActive (false);
			} else {
				transform.localScale = new Vector3 (transform.localScale.x - 0.04f, transform.localScale.x - 0.04f, transform.localScale.z);
				GameWideScript.Player4.size = transform.localScale.x;
			}
		}

		if (static_shield) {
			sköldRing.SetActive (true);


		} else 
		{
			sköldRing.SetActive (false);
		}
		//får input från tangentbordet (via Edit -> Pro. Set. -> Input)
		float horizontal = Input.GetAxis ("Horizontal4");
		float vertical = Input.GetAxis("Vertical4");
		float jump = Input.GetAxis("Jump4");
		float boost = Input.GetAxis("Boost4");
		facing = snoBoll.velocity.normalized;

		isGrounded ();
		anyObject ();
		groundRadius = (transform.localScale.x) / 10;
        if (PowerUp_Inv)
            handleMovement(horizontal * -1, vertical * -1, jump, boost, facing);
        else
            handleMovement(horizontal, vertical, jump, boost, facing);
        jumpForce = 3000 + (100000 / snoBoll.transform.localScale.x);
        movementSpeed = (50 + (5000 / snoBoll.transform.localScale.x)) * slowerFaster;

    }
	private void handleMovement(float horizontal, float vertical, float jump, float boost, Vector2 facing)
	{
		if (!boosty) { 
			if (snoBoll.velocity.y < 0) 
				snoBoll.velocity = new Vector2 (horizontal * movementSpeed, snoBoll.velocity.y - 3); 
			else 
				snoBoll.velocity = new Vector2(horizontal * movementSpeed, snoBoll.velocity.y); //uppdaterar positionsvektorn med input från tangenbordet
			
			tredBoll.GetComponent<TrailRenderer> ().enabled = false;
		}

		if (PowerUp_Inv == true)
			snoBoll.velocity = new Vector2(horizontal * (-1) * movementSpeed, snoBoll.velocity.y);//inverterar positionsvektorn om PowerUp_Inv är aktiv 

		if (jump != 0 && grounded && ableToJump) 
		{
			grounded = false;
			snoBoll.AddForce(new Vector2(horizontal *movementSpeed, jumpForce));
		}
		if ((Time.time > boostStartTime) && boost != 0)
		{
			tredBoll.GetComponent<TrailRenderer> ().startWidth = transform.localScale.x / 8;
			tredBoll.GetComponent<TrailRenderer> ().enabled = true;
			GetComponent<SpriteRenderer>().color = Color.yellow;
			boostStartTime = Time.time + boostCooldown;
			snoBoll.velocity = new Vector2(horizontal * boostForce, vertical * boostForce);
			boosty = true;

		}
		if(boosty && Time.time > boostStartTime - boostCooldown + boostDuration)
		{
			boosty = false;
		}


		tel();
	}

	void OnCollisionEnter2D(Collision2D coll)
	{

        if (static_shield)
            StartCoroutine(shield_delay());
        if ((coll.gameObject.name == "Boll 2" || coll.gameObject.name == "Boll 3" || coll.gameObject.name == "Boll"))
        {

            if ((coll.gameObject.transform.position.y - transform.position.y > 10) && !static_shield) //när denna boll är under den andra bollen // Hamp och Dag inte bli större av boll med sköld. 
            {
                if (transform.localScale.x > minSize)
                {

                    transform.localScale = new Vector3(transform.localScale.x - changeIfHit, transform.localScale.x - changeIfHit, transform.localScale.z);

                    //  groundRadius -= 1;
                }


            }
            if (coll.gameObject.transform.position.y - transform.position.y < -10 && !((coll.gameObject.name == "Boll 2" && coll.gameObject.GetComponent<SnoBoll2>().shield) || (coll.gameObject.name == "Boll 3" && coll.gameObject.GetComponent<SnoBoll3>().shield) || (coll.gameObject.name == "Boll" && coll.gameObject.GetComponent<SnoBoll>().shield))) //när denna boll är över
            {
                if (transform.localScale.x < maxSize)
                {
                    transform.localScale = new Vector3(transform.localScale.x + changeIfHit, transform.localScale.x + changeIfHit, transform.localScale.z);

                    //  groundRadius += 1;
                }

            }

        }


        GameWideScript.Player4.size = transform.localScale.x;
	}
	/// <summary>
	/// Cecks if object is on a suface marked as "ground",
	/// This uses the SerializeField "whatIsGrounded" to know what sufaces are "ground"
	/// </summary>
	private void isGrounded()
	{
		grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(ground.position, groundRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				grounded = true;
		}
	}
	private void anyObject() 
	{
		isObj = false;
		isVertical = false;
		isHorizontal = false;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(ground.position, groundRadius, whatIsAny);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders [i].gameObject != gameObject) {
				isObj = true;
				if (colliders [i].gameObject.layer == 11)
					isVertical = true;
				if (colliders [i].gameObject.layer == 10)
					isHorizontal = true;

			}	
		}
	}
	/// <summary>
	/// Moves the object to the other side of the camera when moving out of view
	/// </summary>
	private void tel()
	{
		float left = Camera.main.gameObject.transform.position.x
			- ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) - snoBollCollider.radius;
		float right = Camera.main.gameObject.transform.position.x
			+ ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) + snoBollCollider.radius;
		float top = Camera.main.gameObject.transform.position.y
			+ ((2f * Camera.main.orthographicSize) / 2) + snoBollCollider.radius;
		float bott = Camera.main.gameObject.transform.position.y
			- ((2f * Camera.main.orthographicSize) / 2) - snoBollCollider.radius;

		if (this.transform.position.x > right)
		{
			tredBoll.GetComponent<TrailRenderer> ().enabled = false;
			this.transform.position = new Vector2(left, this.transform.position.y);
		}
		if (this.transform.position.x < left)
		{
			tredBoll.GetComponent<TrailRenderer> ().enabled = false;
			this.transform.position = new Vector2(right, this.transform.position.y);

		}
		if (this.transform.position.y > top)
		{
			tredBoll.GetComponent<TrailRenderer> ().enabled = false;
			this.transform.position = new Vector2(this.transform.position.x, bott);

		}
		if (this.transform.position.y < bott)
		{
			tredBoll.GetComponent<TrailRenderer> ().enabled = false;
			this.transform.position = new Vector2(this.transform.position.x, top);

		}
	}
	/// <summary>
	/// Inevrts the controls for 'this'
	/// </summary>
	public void inv() { StartCoroutine(wfs2()); }
	IEnumerator wfs2()
	{
      
        SnoBoll2.PowerUp_Inv = true;
		SnoBoll3.PowerUp_Inv = true;
		SnoBoll.PowerUp_Inv = true;
		yield return new WaitForSeconds(5);
		SnoBoll2.PowerUp_Inv = false;
		SnoBoll3.PowerUp_Inv = false;
		SnoBoll.PowerUp_Inv = false;
	}
	/// <summary>
	/// randomly chooses one of the players and gives that player either half or double the movement speed 
	/// This reverts after 5 sec
	/// </summary>
    public void SlowerFasterF() { StartCoroutine(wfs3()); }
    IEnumerator wfs3()
    {
        float randomSF = Random.Range(1, 3);

        if (randomSF < 1.5)

        {
            slowerFaster = 0.5f;
            PowerUp_Faster_Slower.slowerFasterSound = 0;
        }
        else
        {
            slowerFaster = 1.5f;
            PowerUp_Faster_Slower.slowerFasterSound = 1;
        }
        yield return new WaitForSeconds(5);
        slowerFaster = 1;
    }
	/// <summary>
	/// Sets the shield.
	/// This reverts after 10 sec
	/// </summary>
    public void setShield()
    {
        StartCoroutine(shield_delay());
    }

    IEnumerator shield_delay()
    {
        
        static_shield = true;
        shield = true;
        yield return new WaitForSeconds(10);
        shield = false;
        static_shield = false;
	}	
	/// <summary>
	/// Updates the ground radius
	/// </summary>
	public void updateRad(int a){
        groundRadius = (transform.localScale.x) / 10 + 0.1f * a;
    }
}