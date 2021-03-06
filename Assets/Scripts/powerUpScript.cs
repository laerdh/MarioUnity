﻿using UnityEngine;
using System.Collections;

public class powerUpScript : MonoBehaviour {

	private int state;

	private Animator animator;
	private Rigidbody2D obj;

	private int moveSpeed = 3;
	

	public LayerMask theGround;

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator> ();
		obj = GetComponent<Rigidbody2D> ();
		GameObject.Find ("AudioController").GetComponent<AudioManager> ().pwrUpSpawn();
	}

	// Update is called once per frame
	void Update () {
		if (state == 1) {
			obj.velocity = new Vector2 (moveSpeed, obj.velocity.y);
		}else if(state == 2) {
			obj.velocity = new Vector2 (0, 0);
		}

		RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2(moveSpeed/3,0f), 1f, theGround);
		Debug.DrawRay (transform.position, new Vector2(moveSpeed/3,0), Color.red);

		if (hit) {
			print (hit.collider.tag);
			if(hit.collider.tag == "Untagged") {
				FlipSpeed();
			}
			/*if(hit.collider.gameObject.tag == "untagged") {
				FlipSpeed();
			}*/

		}

	}

	void FlipSpeed() {
		print ("test flip");
		moveSpeed = -moveSpeed;
	}

	public void setState(int state) {
		this.state = state;

		print ("State from powerUp on spawn: " + state);
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
