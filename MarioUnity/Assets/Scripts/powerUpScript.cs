using UnityEngine;
using System.Collections;

public class powerUpScript : MonoBehaviour {

	private int state;

	private Animator animator;
	private Rigidbody2D obj;

	private int moveSpeed = 3;
	
	private Transform start;
	private Transform end; 
	public LayerMask theGround;

	// Use this for initialization
	void Start() {
		animator = GetComponent<Animator> ();
		obj = GetComponent<Rigidbody2D> ();

		start = new Vector2 (transform.position.x + 1, transform.position.y);
		end = new Vector2 (transform.position.x + 1, transform.position.y);
	}

	// Update is called once per frame
	void Update () {
		obj.velocity = new Vector2 (moveSpeed, obj.velocity.y);

		start.position = transform.position;
		end.position = new Vector2 (transform.position.x + 1, transform.position.y);
		RaycastHit2D hit = Physics2D.Raycast(start.position, end.position, theGround);
		Debug.DrawRay(start.position, end.position, Color.red);


		/*
		if (hit != null)
		{

			print (hit.collider.gameObject.tag);
			if (hit.collider.gameObject.tag == "pipe")
			{
				FlipSpeed();
				print ("vi vinner ikke gull i år");
			}
		}*/

	}

	// Visualize LineCast on enemy
	void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		if(start != null && end != null)
		Gizmos.DrawLine (start.position, end.position);
		
	}

	/*
	void OnCollisionEnter2D(Collision2D other) {

		print ("Collision!");

		if(other.gameObject.tag == "ground")
			print ("tjolahopp");

		if(other.gameObject.tag == "pipe")
			FlipSpeed();
	}
	*/
	void FlipSpeed() {
		print ("test flip");
		moveSpeed = -moveSpeed;
	}

	public void setState(int state) {
		this.state = state;
		// If mario has more than 2 lives, this powerup will be a flower. This method is called on breakBlock.
		if (state >= 2) { 
			animator.SetBool("isFlower", true);
			animator.SetBool("isMushroom", false);
		} else if (state <= 1) {
			animator.SetBool("isMushroom", true);
			animator.SetBool("isFlower", false);
			obj.velocity = new Vector2(2, 0);
		}
	}
}
