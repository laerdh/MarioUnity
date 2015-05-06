using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

	// Mario states
	private int mario_state = 0;
	private const int IDLE = 0;
	private const int WALKING = 1;
	private const int RUNNING = 2;
	private const int DUCKING = 3;
	private const int JUMPING = 4;

	public float moveSpeed;
	public float jumpHeight;
	private float moveSpeedDef;
	private int sprintDelay = 10;

	private Animator animator;

	// variables for ground checking
	public Transform groundChecker;
	public float groundCheckerWidth;
	public LayerMask theGround;
	private bool grounded;

	private bool facingRight = true;

	// Camera and "wall" objects
	public GameObject cameraWall;
	public Camera camera;
	private bool moveTheCamera;
	private const float DEADZONE = 0.1f; // The distance mario is allowed to walk before the screen stops 

	// BLOCKS
	public breakBlockScript breakBlock;

	void Start() {
		animator = GetComponent<Animator> ();
		moveSpeedDef = moveSpeed;
		camera.transform.position = new Vector3 (GetComponent<Rigidbody2D> ().position.x, camera.transform.position.y, camera.transform.position.z);
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, theGround);

		if (!grounded) {
			mario_state = JUMPING;
		} else
			mario_state = IDLE;

		if (moveTheCamera) {
			camera.transform.position = new Vector3 (GetComponent<Rigidbody2D> ().position.x, camera.transform.position.y, camera.transform.position.z);
		}
	}

	void Update() {
		float dirX = Input.GetAxis ("Horizontal");
		keyBoardInput ();
		animatePlayer(dirX);
		sprint();
		moveCamera ();

		if(GetComponent<Rigidbody2D> ().position.x < cameraWall.transform.position.x + DEADZONE)
			GetComponent<Rigidbody2D> ().transform.position = new Vector2(cameraWall.transform.position.x + DEADZONE,GetComponent<Rigidbody2D> ().position.y);
	}

	// Method for checking keyboard input
	void keyBoardInput() {
		// check keyboard presses
		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpHeight);
		}

		// check if keys are down
		if (Input.GetKey (KeyCode.A)) {
			facingRight = false;
			if(mario_state != JUMPING)
				mario_state = RUNNING;
			if(GetComponent<Rigidbody2D> ().position.x > cameraWall.transform.position.x + DEADZONE)
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		} else 
		if (Input.GetKey (KeyCode.D)) {
			facingRight = true;
			if(mario_state != JUMPING)
				mario_state = RUNNING;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		}
		if(Input.GetKey(KeyCode.S)) {
			animator.SetBool("isDucking", true);
		}


		// check keys released
		if (Input.GetKeyUp (KeyCode.A)) {
			mario_state = IDLE;
			if(grounded) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			}
		} 
		if (Input.GetKeyUp (KeyCode.D)) {
			mario_state = IDLE;
			if(grounded) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			}
		}
		if(Input.GetKeyUp(KeyCode.S)) {
			animator.SetBool("isDucking", false);
		}
	}

	// Break block
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "BreakableBlock") {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, 0);
			other.GetComponent<breakBlockScript>().setHit(true, 2);
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

		// Check if the player is in the air
		if (!grounded) {
			animator.SetBool ("isJumping", true);
		} else
			animator.SetBool ("isJumping", false);
	}

	// Returns true if player is sprinting 
	bool isSprinting() {
		if(Input.GetKey(KeyCode.LeftShift)) {
			return true;
		} return false;
	}

	// Move Camera
	public void moveCamera() {
		if (transform.position.x > cameraWall.transform.position.x + 8 && facingRight) {
			moveTheCamera = true;
		} else if (!facingRight) {
			moveTheCamera = false;
		}
	}
	
	void OnGUI() {
		GUI.Box (new Rect(20,20, 80,80),""+grounded);
	}
}
