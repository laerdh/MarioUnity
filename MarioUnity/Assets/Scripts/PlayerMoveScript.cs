using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

	public float moveSpeed;
	public float jumpHeight;
	private float moveSpeedDef;
	private int sprintDelay = 10;

	private Animator animator;

	public Transform groundChecker;
	public float groundCheckerWidth;
	public LayerMask theGround;
	private bool grounded;

	void Start() {
		animator = GetComponent<Animator> ();
		moveSpeedDef = moveSpeed;
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, theGround);
	}

	void Update() {

		sprint();
	
		keyBoardInput ();

		float dirX = Input.GetAxis ("Horizontal");
		animatePlayer(dirX);

	}

	// Method for checking keyboard input
	void keyBoardInput() {
		// check keyboard presses
		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpHeight);
		}

		// check if keys are down
		if (Input.GetKey (KeyCode.A)) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		}
		if (Input.GetKey (KeyCode.D)) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		}

		// check keys released

	}
	// Method for making the player sprint
	void sprint() {
		if (isSprinting ()) {
			if(sprintDelay > 0)
				sprintDelay--;
			if(sprintDelay <= 0)
				moveSpeed = 10;
		} else {
			moveSpeed = moveSpeedDef;
			sprintDelay = 10;
		}
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

	// Returns true if player is sprinting 
	bool isSprinting() {
		if(Input.GetKey(KeyCode.LeftShift)) {
			return true;
		} return false;
	}

}
