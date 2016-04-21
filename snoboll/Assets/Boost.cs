using UnityEngine;
using System.Collections;

public class Boost : MonoBehaviour {
	[SerializeField]     //för att kunna styra speed från GUI:t
	private float movementSpeed;

	[SerializeField]     //för att kunna styra speed från GUI:t
	private float boostSpeed;

	private Rigidbody2D snoBoll; //pekare till snöboll1

	void Start () 
	{
		snoBoll = GetComponent<Rigidbody2D> ();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.B)){
			snoBoll.velocity = new Vector2(snoBoll.velocity.x * movementSpeed * boostSpeed, snoBoll.velocity.y * movementSpeed * boostSpeed);
		}

		else if(Input.GetKeyUp(KeyCode.B)){
			snoBoll.velocity = new Vector2(snoBoll.velocity.x * movementSpeed, snoBoll.velocity.y * movementSpeed);
		}
	}

}
