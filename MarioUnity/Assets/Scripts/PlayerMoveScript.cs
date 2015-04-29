using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

	// Mario states
	private int mario_state = 0;
	private const int IDLE = 0;
	private const int WALKING = 1;
	private const int RUNNING = 2;
	private const int DUCKING = 3;

	public float moveSpeed;
	public float jumpHeight;
	private float moveSpeedDef;
	private int sprintDelay = 10;

	private Animator animator;

	public Transform groundChecker;
	public float groundCheckerWidth;
	public LayerMask theGround;
	private bool grounded;

	private bool facingRight;

	// Camera and "wall" objects
	public GameObject cameraWall;
	public Camera camera;

	void Start() {
		animator = GetComponent<Animator> ();
		moveSpeedDef = moveSpeed;
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, theGround);
	}

	void Update() {
		float dirX = Input.GetAxis ("Horizontal");
		animatePlayer(dirX);
		sprint();
		keyBoardInput ();
	}

	// Method for checking keyboard input
	void keyBoardInput() {
		// check keyboard presses
		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpHeight);
		}

		// check if keys are down
		if (Input.GetKey (KeyCode.A) && GetComponent<Rigidbody2D> ().position.x > cameraWall.transform.position.x + 1) {
			mario_state = RUNNING;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		} else if (Input.GetKey (KeyCode.D)) {
			mario_state = RUNNING;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			facingRight = true;
		}

		// check keys released
		if (Input.GetKeyUp (KeyCode.A)) {
			mario_state = IDLE;
			animator.SetBool ("isRunning", false);
			if(grounded) {
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
			}
		} 
		if (Input.GetKeyUp (KeyCode.D)) {
			mario_state = IDLE;
			facingRight = false;
			animator.SetBool ("isRunning", false);
			if(grounded) {
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
			}
		}
	}




	// Method for making the player sprint
	void sprint() {
		if (isSprinting ()) {
			if(sprintDelay > 0)
				sprintDelay--;
			if(sprintDelay <= 0 && grounded)
				moveSpeed = 10;
		} else {
			moveSpeed = moveSpeedDef;
			sprintDelay = 10;
		}
	}

	// Animate the player 
	void animatePlayer(float dirX) {
		// Checks if the player is running
		if (mario_state == RUNNING) {
			animator.SetBool ("isRunning", true);
		} else animator.SetBool ("isRunning", false);

		// Checks the direction the player is moving and flips the player
		if (dirX < 0) {
			GetComponent<Rigidbody2D>().transform.localScale = new Vector3 (-5, 5, 0);
		} else if (dirX > 0) {
			GetComponent<Rigidbody2D>().transform.localScale = new Vector3 (5, 5, 0);
		}

		if(Input.GetKey(KeyCode.S)) {
			animator.SetBool("isDucking", true);
		}
		if(Input.GetKeyUp(KeyCode.S)) {
			animator.SetBool("isDucking", false);
		}
	}

	// Returns true if player is sprinting 
	bool isSprinting() {
		if(Input.GetKey(KeyCode.LeftShift)) {
			return true;
		} return false;
	}

	// Move Camera
}
