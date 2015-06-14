using UnityEngine;
using System.Collections;

public class Pirate : MonoBehaviour {
	Animator anim;
	private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
	public float moveTime = 0.1f;           //Time it will take object to move, in seconds.
	private float inverseMoveTime;          //Used to make movement more efficient.

	public LayerMask shirtLayer;
	public LayerMask fieldLayer;

	// Use this for initialization
	void Start () {
		//GameManager.instance.AddEnemyToList(this);
		//Get a component reference to this object's BoxCollider2D
		boxCollider = GetComponent <BoxCollider2D> ();
		
		//Get a component reference to this object's Rigidbody2D
		rb2D = GetComponent <Rigidbody2D> ();
		anim = GetComponent<Animator>();
		
		//By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
		inverseMoveTime = 1f / moveTime;
	}

	//Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
	protected IEnumerator SmoothMovement (Vector3 end)
	{
		anim.SetBool("walking", true);
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		
		//While that distance is greater than a very small amount (Epsilon, almost zero):
		while(sqrRemainingDistance > float.Epsilon)
		{
			//Find a new position proportionally closer to the end, based on the moveTime
			Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
			
			//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
			rb2D.MovePosition (newPostion);
			
			//Recalculate the remaining distance after moving.
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			
			//Return and loop until sqrRemainingDistance is close enough to zero to end the function
			yield return null;
		}
		anim.SetBool("walking", false);
	}

	//Move returns true if it is able to move and false if not. 
	//Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
	protected bool Move (int xDir, int yDir/*, out RaycastHit2D hit*/)
	{
		//Store start position to move from, based on objects current transform position.
		Vector2 start = transform.position;
		
		// Calculate end position based on the direction parameters passed in when calling Move.
		Vector2 end = start + new Vector2 (xDir, yDir);
		
		//Disable the boxCollider so that linecast doesn't hit this object's own collider.
		//boxCollider.enabled = false;
		
		//Cast a line from start point to end point checking collision on blockingLayer.
		RaycastHit2D hit = Physics2D.Linecast (start, end, shirtLayer);
		RaycastHit2D fieldHit = Physics2D.Linecast (start, end, fieldLayer);
		
		//Re-enable boxCollider after linecast
		//boxCollider.enabled = true;


		if(hit.transform == null)
		{
			StartCoroutine (SmoothMovement (end));
			Debug.Log(fieldHit.transform);
			return true;
		}
		else if(hit.transform.tag == "Shirt" ){
			Shirt shirt = (Shirt) hit.transform.gameObject.GetComponent(typeof(Shirt));
			shirt.Hide ();
		}

		
		//If something was hit, return false, Move was unsuccesful.
		return false;
	}

	// Update is called once per frame
	void Update () {
		//If it's not the player's turn, exit the function.
		if(!GameManager.instance.players1Turn) return;

		int horizontal = 0;     //Used to store the horizontal move direction.
		int vertical = 0;       //Used to store the vertical move direction.
		
		//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
		horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		
		//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
		vertical = (int) (Input.GetAxisRaw ("Vertical"));

		//Check if moving horizontally, if so set vertical to zero.
		if(horizontal != 0)
		{
			vertical = 0;
		}
		
		//Check if we have a non-zero value for horizontal or vertical
		if(horizontal != 0 || vertical != 0)
		{
			Move(horizontal, vertical);
			GameManager.instance.players1Turn = false;
			//Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
			//Pass in horizontal and vertical as parameters to specify the direction to move Player in.
			//AttemptMove<Wall> (horizontal, vertical);
		}

	}

	//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
	private void OnTriggerEnter2D (Collider2D other)
	{
		/*if(other.tag == "Shirt")
		{
			Shirt a = (Shirt) other.gameObject.GetComponent(typeof(Shirt));
			a.Hide ();		
		}*/
	}
}
