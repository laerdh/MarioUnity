using UnityEngine;
using System.Collections;

public class powerUpScript : MonoBehaviour {

	private int state;

	private Animator animator;
	private Rigidbody2D obj;

	private int moveSpeed = 3;
	

	public LayerMask theGround;

	// Use this for initialization
	void Start() {
		animator = GetComponent<Animator> ();
		obj = GetComponent<Rigidbody2D> ();


	}

	// Update is called once per frame
	void Update () {
		obj.velocity = new Vector2 (moveSpeed, obj.velocity.y);




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
