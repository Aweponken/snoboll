using UnityEngine;
using System.Collections;

public class SnoBoll : MonoBehaviour 
{
	private Rigidbody2D snoBoll; //pekare till snöboll1
	private bool grounded;

	[SerializeField] 
	private LayerMask whatIsGround;

	[SerializeField] 
	private Transform ground;

	[SerializeField]
	private float groundRadius; 

	[SerializeField]     //för att kunna styra speed från GUI:t
	private float movementSpeed;

	[SerializeField]     //för att kunna styra jumpforce från GUI:t
	private float jumpForce;

	// Use this for initialization
	void Start () 
	{
		snoBoll = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//får input från tangentbordet (via Edit -> Pro. Set. -> Input)
		float horizontal = Input.GetAxis ("Horizontal"); 
		float jump = Input.GetAxis("Jump");

		isGrounded ();

		handleMovement (horizontal, jump);
	}

	private void handleMovement(float horizontal, float jump) 
	{
		snoBoll.velocity = new Vector2(horizontal * movementSpeed, snoBoll.velocity.y); //uppdaterar positionsvektorn med input från tangenbordet

		if (jump != 0 && grounded) 
		{
			grounded = false;
			snoBoll.AddForce(new Vector2(0, jumpForce));
		}
			
	}

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
}
