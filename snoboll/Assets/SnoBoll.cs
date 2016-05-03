using UnityEngine;
using System.Collections;

public class SnoBoll : MonoBehaviour
{
    private Rigidbody2D snoBoll; //pekare till snöboll1
    private CircleCollider2D snoBollCollider;
    private bool grounded;
    private bool isObj;
    private bool isVertical;
    private bool isHorizontal;
    public static bool PowerUp_Inv = false;
    public static bool static_shield = false;
    public bool shield = false;
    public static bool ableToJump = true;
    public static float horizontal;
    public static float slowerFaster = 1;
    Vector2 facing;


    /// <summary>
    /// Defines what layers are "ground"
    /// --jumpable--
    /// </summary>
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

    [SerializeField]
    private float maxSize = 220;
    [SerializeField]
    private float minSize = 30;
    [SerializeField]
    private float changeIfHit = 5;

    public bool boosty = false;
    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        boostStartTime = Time.time;
        GameWideScript.Player1.size = transform.localScale.x;
        snoBoll = GetComponent<Rigidbody2D>();
        snoBollCollider = GetComponent<CircleCollider2D>();
		GameWideScript.Player1.Color = "Yellow";
    }

    /// <summary>
    /// This funtion updates on a fixed time (50 times/sec(???)) 
    /// and gets the input from the controller/keyboard and calls 
    /// handleMovement with these arguments. It also checks wheather 
    /// the object is grounded or not. 
    /// </summary>
	void FixedUpdate()
    {
        //får input från tangentbordet (via Edit -> Pro. Set. -> Input)
        Debug.Log(static_shield);
        horizontal = Input.GetAxis("Horizontal1");
        float vertical = Input.GetAxis("Vertical1");
        float jump = Input.GetAxis("Jump1");
        float boost = Input.GetAxis("Boost1");
        facing = snoBoll.velocity.normalized;

        isGrounded();
        anyObject();
        groundRadius = (transform.localScale.x) / 10;
        if (PowerUp_Inv)
            handleMovement(horizontal * -1, vertical * -1, jump, boost, facing);
        else
            handleMovement(horizontal, vertical, jump, boost, facing);
        jumpForce = 3000 + (100000 / snoBoll.transform.localScale.x);
        movementSpeed =( 50 + (5000 / snoBoll.transform.localScale.x) )* slowerFaster;

    }

    /// <summary>
    /// This function takes two arguments, Horizontal and Float, 
    /// which are generated from input from controller/keyboard 
    /// and decide in what direction the objuect should move
    /// </summary>
    /// <param name="horizontal">Horizontal.</param>
    /// <param name="jump">Jump.</param>
	private void handleMovement(float horizontal, float vertical, float jump, float boost, Vector2 facing)
    {
        if (!boosty)
        {
			if (snoBoll.velocity.y < 0) 
				snoBoll.velocity = new Vector2 (horizontal * movementSpeed, snoBoll.velocity.y - 3); 
			else 
				snoBoll.velocity = new Vector2(horizontal * movementSpeed, snoBoll.velocity.y); //uppdaterar positionsvektorn med input från tangenbordet
			GetComponent<SpriteRenderer>().color = new Color32(240, 227, 157, 255);
        }


        // if (PowerUp_Inv == true)
        //    snoBoll.velocity = new Vector2(horizontal * (-1) * movementSpeed, snoBoll.velocity.y);//inverterar positionsvektorn om PowerUp_Inv är aktiv 



        if (jump != 0 && grounded && ableToJump)
        {
            grounded = false;
            snoBoll.AddForce(new Vector2(horizontal * movementSpeed, jumpForce));
        }
        if ((Time.time > boostStartTime) && boost != 0)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;   //Bollen ändrar färg
            boostStartTime = Time.time + boostCooldown;
            snoBoll.velocity = new Vector2(horizontal * boostForce, vertical * boostForce);
            boosty = true;

        }
        if (boosty && Time.time > boostStartTime - boostCooldown + boostDuration)
        {
           
            boosty = false;
        }

        tel();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {


        if (shield)
            StartCoroutine(shield_delay());
        if ((coll.gameObject.name == "Boll 2" || coll.gameObject.name == "Boll 3" || coll.gameObject.name == "Boll 4"))
        {

            if ((coll.gameObject.transform.position.y - transform.position.y > 10) && !static_shield) //när denna boll är under den andra bollen // Hamp och Dag inte bli större av boll med sköld. 
            {
                if (transform.localScale.x > minSize)
                {

                    transform.localScale = new Vector3(transform.localScale.x - changeIfHit, transform.localScale.x - changeIfHit, 0);

                    //  groundRadius -= 1;
                }


            }
            if (coll.gameObject.transform.position.y - transform.position.y < -10 && !((coll.gameObject.name == "Boll 2" && coll.gameObject.GetComponent<SnoBoll2>().shield) || (coll.gameObject.name == "Boll 3" && coll.gameObject.GetComponent<SnoBoll3>().shield) || (coll.gameObject.name == "Boll 4" && coll.gameObject.GetComponent<SnoBoll4>().shield))) //när denna boll är över
            {
                if (transform.localScale.x < maxSize)
                {
                    transform.localScale = new Vector3(transform.localScale.x + changeIfHit, transform.localScale.x + changeIfHit, 0);

                    //  groundRadius += 1;
                }

            }

        }

        GameWideScript.Player1.size = transform.localScale.x;
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
            if (colliders[i].gameObject != gameObject)
            {
                isObj = true;
                if (colliders[i].gameObject.layer == 11)
                    isVertical = true;
                if (colliders[i].gameObject.layer == 10)
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
    public void inv() { StartCoroutine(wfs2()); }
    IEnumerator wfs2()
    {
        SnoBoll2.PowerUp_Inv = true;
        SnoBoll3.PowerUp_Inv = true;
        SnoBoll4.PowerUp_Inv = true;
        yield return new WaitForSeconds(5);
        SnoBoll2.PowerUp_Inv = false;
        SnoBoll3.PowerUp_Inv = false;
        SnoBoll4.PowerUp_Inv = false;
    }
    public void SlowerFasterF(){ StartCoroutine(wfs3());}
    IEnumerator wfs3()
    {
        float randomSF = Random.Range(1, 3);
        if (randomSF < 1.5)

        {
            slowerFaster = 0.5f;
        }
        else
        {
            slowerFaster = 1.5f;
        }
      
        yield return new WaitForSeconds(5);
        slowerFaster = 1;
       
    }

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
    public void noJump()
    {
        StartCoroutine(wfs4());
    }
    IEnumerator wfs4()
    {
		float randomSF = Random.Range(1, GameWideScript.Instance.setPlayers+1);
        if (randomSF == 1)
        {
            SnoBoll.ableToJump = false;
        }
        else if (randomSF == 2)
        {
            SnoBoll2.ableToJump = false;
        }
        else if (randomSF == 3)
        {
            SnoBoll3.ableToJump = false;
        }
        else
        {
            SnoBoll4.ableToJump = false;
        }
        Debug.Log("random SF " + randomSF);
        yield return new WaitForSeconds(5);
        Debug.Log("väntat ");
        SnoBoll.ableToJump = true;
        SnoBoll2.ableToJump = true;
        SnoBoll3.ableToJump = true;
        SnoBoll4.ableToJump = true;
        Debug.Log("Efter " + randomSF);
    }
	public void updateRad(){
		groundRadius = (transform.localScale.x) / 10;
	}
}