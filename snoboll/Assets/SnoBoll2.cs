﻿using UnityEngine;
using System.Collections;

public class SnoBoll2 : MonoBehaviour 
{
	private Rigidbody2D snoBoll; //pekare till snöboll1
	private CircleCollider2D snoBollCollider;
	private bool grounded;
	/// <summary>
	/// Defines what layers are "ground"
	/// --jumpable--
	/// </summary>
	[SerializeField] 
	private LayerMask whatIsGround;

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

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () 
	{
		snoBoll = GetComponent<Rigidbody2D> ();
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
		//får input från tangentbordet (via Edit -> Pro. Set. -> Input)
		float horizontal = Input.GetAxis ("Horizontal2"); 
		float jump = Input.GetAxis("Jump2");

		isGrounded ();

		handleMovement (horizontal, jump);
	}

	/// <summary>
	/// This function takes two arguments, Horizontal and Float, 
	/// which are generated from input from controller/keyboard 
	/// and decide in what direction the objuect should move
	/// </summary>
	/// <param name="horizontal">Horizontal.</param>
	/// <param name="jump">Jump.</param>
	private void handleMovement(float horizontal, float jump) 
	{
		snoBoll.velocity = new Vector2(horizontal * movementSpeed, snoBoll.velocity.y); //uppdaterar positionsvektorn med input från tangenbordet

		if (jump != 0 && grounded) 
		{
			grounded = false;
			snoBoll.AddForce(new Vector2(0, jumpForce));
		}
		tel ();	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {


        if (coll.gameObject.name == "Boll")
        {

            if (coll.gameObject.transform.position.y - transform.position.y > 10) //när denna boll är under den andra bollen
            {
                if (transform.localScale.x > 30)
                {

                    transform.localScale = new Vector3(transform.localScale.x - 10, transform.localScale.x - 10, 0);

                    groundRadius -= 1;
                }


            }
            if (coll.gameObject.transform.position.y - transform.position.y < -10) //när denna boll är över
            {
                if (transform.localScale.x < 170)
                {
                    transform.localScale = new Vector3(transform.localScale.x + 10, transform.localScale.x + 10, 0);

                    groundRadius += 1;
                }

            }

        }

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
            this.transform.position = new Vector2(left, this.transform.position.y);
        }
        if (this.transform.position.x < left)
        {
            this.transform.position = new Vector2(right, this.transform.position.y);

        }
        if (this.transform.position.y > top)
        {
            this.transform.position = new Vector2(this.transform.position.x, bott);

        }
        if (this.transform.position.y < bott)
        {
            this.transform.position = new Vector2(this.transform.position.x, top);

        }
    
	}
}
