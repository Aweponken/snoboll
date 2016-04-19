using UnityEngine;
using System.Collections;

public class SnoBoll2 : MonoBehaviour 
{
	private Rigidbody2D snoBoll; //pekare till snöboll1
	private CircleCollider2D snoBollCollider;
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
		snoBollCollider = GetComponent<CircleCollider2D>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		//får input från tangentbordet (via Edit -> Pro. Set. -> Input)
		float horizontal = Input.GetAxis ("Horizontal2"); 
		float jump = Input.GetAxis("Jump2");

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
		tel ();	
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
	private void tel()
	{
		float left = Camera.main.gameObject.transform.position.x
		             - ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) - snoBollCollider.radius;
		float right = Camera.main.gameObject.transform.position.x
		              + ((Camera.main.aspect * 2f * Camera.main.orthographicSize) / 2) + snoBollCollider.radius;
		
		if (this.transform.position.x > right) 
		{
			this.transform.position = new Vector2 (left ,this.transform.position.y);
		}
		if (this.transform.position.x < left) 
		{
			this.transform.position = new Vector2 (right,this.transform.position.y);
		}
	}
}
