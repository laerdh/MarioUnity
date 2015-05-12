using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

	// Player RigidBody
	public Rigidbody2D player;

	// Mario states
	private int mario_state = 0;
	private const int IDLE = 0;
	private const int WALKING = 1;
	private const int RUNNING = 2;
	private const int DUCKING = 3;
	private const int JUMPING = 4;

	// Mario lives 
	private int playerLives;
	private int playerLivesCurrent;

	// Move variables
	public float moveSpeed;
	public float jumpHeight;
	private float moveSpeedDef;
	private int sprintDelay = 10;

	// Animator
	private Animator animator;

	// variables for ground checking
	public Transform groundChecker;
	public float groundCheckerWidth;
	public LayerMask theGround;
	private bool grounded;
	public bool onPipe = false;
	public LayerMask thePipe;


	// Direction boolean, used to check if player is facing right and move the camera
	private bool facingRight = true;

	// Camera and "wall" objects
	public GameObject cameraWall;
	public Camera camera;
	private bool moveTheCamera;
	private const float DEADZONE = 2.1f; // The distance mario is allowed to walk before the screen stops 

	// BLOCKS
	public breakBlockScript breakBlock;

	void Start() {
		playerLives = 1;
		playerLivesCurrent = playerLives;



		player = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		animator.SetInteger ("MarioLives",	playerLives);
		moveSpeedDef = moveSpeed;
		camera.transform.position = new Vector3 (player.position.x, camera.transform.position.y, camera.transform.position.z);
	}

	void FixedUpdate() {

		grounded = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, theGround);
		
		
		if (!grounded) 
		grounded = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, thePipe);
		if (grounded) {
			onPipe = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, thePipe);
		}



		/*
		Debug.DrawLine (this.transform.position, groundedEnd.position, Color.green);
		//onPipe = Physics2D.Linecast (this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer ("Pipe"));
		RaycastHit2D h = Physics2D.Raycast(this.transform.position, groundedEnd.position);
		if(h.collider.tag == "pipe"){
			onPipe = true;
		}
		*/

		if (!grounded) {
			mario_state = JUMPING;
		} else
			mario_state = IDLE;

		if (moveTheCamera) {
			camera.transform.position = new Vector3 (player.position.x, camera.transform.position.y, camera.transform.position.z);
		}
	}

	void Update() {
		changeColliderSize (playerLives);

		if(playerLivesCurrent != playerLives)
			animator.SetInteger ("MarioLives",	playerLives);

		float dirX = Input.GetAxis ("Horizontal");
		keyBoardInput ();
		animatePlayer(dirX);
		sprint();
		moveCamera ();



		// Stop the player if he walks to the left of the screen
		if(player.position.x < cameraWall.transform.position.x + DEADZONE)
			player.transform.position = new Vector2(cameraWall.transform.position.x + DEADZONE,player.position.y);
	}

	// Endre collider
	public void changeColliderSize(int playerLives) {
		BoxCollider2D b = GetComponent<BoxCollider2D> ();
		if(b != null)
		{
			if(playerLives == 1) {
				b.size =	new Vector2(0.16f,0.16f);
			}
			else if(playerLives >= 2)
				b.size = new Vector2(0.16f, 0.32f);
		}
	}

	// Method for checking keyboard input
	void keyBoardInput() {
		// check keyboard presses
		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			player.velocity = new Vector2 (player.velocity.x, jumpHeight);
		}

		// check if keys are down
		if (Input.GetKey (KeyCode.A) && !Input.GetKey(KeyCode.S)) {
			facingRight = false;
			if(mario_state != JUMPING)
				mario_state = RUNNING;
			if(player.position.x > cameraWall.transform.position.x + DEADZONE)
				player.velocity = new Vector2 (-moveSpeed, player.velocity.y);
		} else 
		if (Input.GetKey (KeyCode.D)&& !Input.GetKey(KeyCode.S)) {
			facingRight = true;
			if(mario_state != JUMPING)
				mario_state = RUNNING;
			player.velocity = new Vector2 (moveSpeed, player.velocity.y);
		}
		if(Input.GetKey(KeyCode.S)) {
			animator.SetBool("isDucking", true);
		}


		// check keys released
		if (Input.GetKeyUp (KeyCode.A)) {
			mario_state = IDLE;
			if(grounded) {
				player.velocity = new Vector2 (0, 0);
			}
		} 
		if (Input.GetKeyUp (KeyCode.D)) {
			mario_state = IDLE;
			if(grounded) {
				player.velocity = new Vector2 (0, 0);
			}
		}
		if(Input.GetKeyUp(KeyCode.S)) {
			animator.SetBool("isDucking", false);
		}

		if (onPipe && Input.GetKey (KeyCode.S)) {
			Debug.Log ("On pipe!");
			player.transform.position = new Vector2(-48,-12);

		
		
			//animator.SetBool("isOnPipe", true);

		
		}

	}

	// Break block
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "BreakableBlock") {
			player.velocity = new Vector2 (player.velocity.x, (-player.velocity.y / 4));
			other.GetComponent<breakBlockScript> ().setHit (true, 2);
		}
		if (other.gameObject.tag == "powerUp") {
			powerUpScript e = other.GetComponent<powerUpScript>();
			playerLives++;
			animator.SetInteger("MarioLives", playerLives);

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
			player.transform.localScale = new Vector3 (-5, 5, 0);
		} else if (dirX > 0) {
			player.transform.localScale = new Vector3 (5, 5, 0);
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
}
