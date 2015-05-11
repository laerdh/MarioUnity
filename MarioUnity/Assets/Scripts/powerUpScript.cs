using UnityEngine;
using System.Collections;

public class powerUpScript : MonoBehaviour {

	private int state;

	private Animator animator;
	private Rigidbody2D obj;

	private int moveSpeed = 3;
	
	public Transform sightStart;
	public Transform sightEnd; 

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
		obj = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		obj.velocity = new Vector2 (moveSpeed, obj.velocity.y);

		RaycastHit2D hit = Physics2D.Raycast(sightStart.position, sightEnd.position, 0.2F);
		
		if (hit != null)
		{

			print (hit.collider.gameObject.tag);
			if (hit.collider.gameObject.tag == "pipe")
			{
				//FlipSpeed();
				print ("vi vinner ikke gull i år");
			}
		}

	}
	/*
	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "ground")
			print ("tjolahopp");

		if(other.gameObject.tag == "pipe")
			FlipSpeed();
	}*/

	void FlipSpeed() {
		print ("VI FLIPPPPPEEEEEEEEEEEEEEEEEEEEEEEER");
		moveSpeed = -moveSpeed;
	}

	// Visualize LineCast on enemy
	void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine (sightStart.position, sightEnd.position);
		
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
