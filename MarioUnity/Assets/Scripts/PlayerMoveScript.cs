using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

	/*
	Rigidbody player;
	Animator animator;
	public float moveSpeedIncrease;
	public float gravity;
	public float jumpHeight;

	// Use this for initialization
	void Start () {
		player = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	// FixedUpdate
	void FixedUpdate() {
		float dirX = Input.GetAxis ("Horizontal");

		if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.RightArrow)) {
			dirX = 0;
		} 


		movePlayer (dirX);
		animatePlayer (dirX);
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			player.velocity = new Vector3(0f, jumpHeight, 0f);
		}

	}

	// move player
	void movePlayer(float dirX) {
		player.velocity = new Vector3 (dirX * moveSpeedIncrease, 0f, 0f);


	}

	// Animate the player 
	void animatePlayer(float dirX) {
		// Checks if the player is running
		if (dirX != 0) {
			animator.SetBool ("isRunning", true);
		} else animator.SetBool ("isRunning", false);
	}
	*/

	public float moveSpeed;
	public float jumpHeight;

	private Animator animator;

	void Start() {
		animator = GetComponent<Animator> ();
	}

	void Update() {

		if (Input.GetKeyDown (KeyCode.Space)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
		}
		if (Input.GetKey (KeyCode.A)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed,GetComponent<Rigidbody2D>().velocity.y);
		}
		if (Input.GetKey (KeyCode.D)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed,GetComponent<Rigidbody2D>().velocity.y);
		}

		float dirX = Input.GetAxis ("Horizontal");
		animatePlayer(dirX);

	}

	// Animate the player 
	void animatePlayer(float dirX) {
		// Checks if the player is running
		if (dirX != 0) {
			animator.SetBool ("isRunning", true);
		} else animator.SetBool ("isRunning", false);

		// Checks the direction the player is moving and flips the player
		if (dirX < 0) {
			GetComponent<Rigidbody2D>().transform.localScale = new Vector3 (-6, 6, 0);
		} else if (dirX > 0) {
			GetComponent<Rigidbody2D>().transform.localScale = new Vector3 (6, 6, 0);
		}
	}

}
