using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

	// Player RigidBody
	public Rigidbody2D player;
	public BoxCollider2D marioCollider;

	// Mario states
	private int mario_state = 0;
	private const int IDLE = 0;
	private const int WALKING = 1;
	private const int RUNNING = 2;
	private const int DUCKING = 3;
	private const int JUMPING = 4;
	private const int DEAD = 5;

	// Mario lives 
	private int playerLives;
	private int playerLivesCurrent;

	// Move variables
	public float moveSpeed;
	public float jumpHeight;
	private float moveSpeedDef;
	private int sprintDelay = 10;
	private int dir = 0;
	private bool goDownPipe = false;
	private int goDownPipeCounter = 50;

	// Animator
	private Animator animator;

	// variables for ground checking
	public Transform groundChecker;
	public float groundCheckerWidth;
	public LayerMask theGround;
	private bool grounded;


	// Direction boolean, used to check if player is facing right and move the camera
	private bool facingRight = true;

	// Camera and "wall" objects
	public GameObject cameraWall;
	public Camera camera;
	private bool moveTheCamera;
	private const float DEADZONE = 1.5f; // The distance mario is allowed to walk before the screen stops 
	private bool cameraIsUnderGround;
	private float defaultCameraPos;
	private float underWorldCameraPosY;
	private float underWorldCameraPosX;
	private int cameraUnderGroundCountdownDelay = 20;

	// BLOCKS
	public breakBlockScript breakBlock;

	//AudioController
	public AudioManager audioManager;

	// Time Wait
	private int waitTime;
	

	void Start() {
		playerLives = 1;
		playerLivesCurrent = playerLives;
		
		player = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		animator.SetInteger ("MarioLives",	playerLives);
		moveSpeedDef = moveSpeed;
		camera.transform.position = new Vector3 (player.position.x, camera.transform.position.y, camera.transform.position.z);
		defaultCameraPos = camera.transform.position.y;
		print (defaultCameraPos);
		underWorldCameraPosY = -8.3f;
		underWorldCameraPosX = -42.42869f;
		cameraIsUnderGround = false;
		//Test
		GameObject w = GameObject.Find("AudioController");
		audioManager = w.GetComponent<AudioManager>();
	}

	void FixedUpdate() {

		grounded = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, theGround);

		if (!grounded) {
			mario_state = JUMPING;
		} else
			mario_state = IDLE;

		if (!cameraIsUnderGround) {
			if (moveTheCamera) {
				camera.transform.position = new Vector3 (player.position.x, defaultCameraPos, camera.transform.position.z);
			}
		} else if (cameraIsUnderGround) {
			if(cameraUnderGroundCountdownDelay > -1)
				cameraUnderGroundCountdownDelay--;
			if(cameraUnderGroundCountdownDelay < 0)
				camera.transform.position = new Vector3 (underWorldCameraPosX, underWorldCameraPosY, camera.transform.position.z);
		}
	}

	void Update() {
		goDownPipeCountDown ();

		changeColliderSize (playerLives);

		if (playerLivesCurrent != playerLives) {
			animator.SetInteger ("MarioLives", playerLives);
			playerLivesCurrent = playerLives;
		}

		//float dirX = Input.GetAxis ("Horizontal");
		keyBoardInput ();
		//animatePlayer(dirX);
		sprint();
		moveCamera ();

		// Stop the player if he walks to the left of the screen
		if(player.position.x < cameraWall.transform.position.x + DEADZONE)
			player.transform.position = new Vector2(cameraWall.transform.position.x + DEADZONE,player.position.y);

	}

	public void goDownPipeCountDown() {
		Debug.Log (player.GetComponent<BoxCollider2D>().enabled);
		if (goDownPipe) {
			goDownPipeCounter--;
			//Debug.Log (goDownPipeCounter);

			if(goDownPipeCounter < 5) {
				audioManager.stopBackgroundMusic();
				audioManager.UnderGroundMusic();
				player.GetComponent<BoxCollider2D>().enabled = true;
				if(player.GetComponent<BoxCollider2D>().enabled == true)
				goDownPipe = false;
			}
		}
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
			//Calls method in AudioManager
			if(playerLives == 1){
				audioManager.playSmallMarioJump();
			}else{
				audioManager.playBigMarioJump();
			}
			player.velocity = new Vector2 (player.velocity.x, jumpHeight);
		}

		// check if keys are down
		if (Input.GetKey (KeyCode.A) && !Input.GetKey(KeyCode.S)) {
			dir = -1;
			facingRight = false;
			if(mario_state != JUMPING)
				mario_state = RUNNING;
			if(player.position.x > cameraWall.transform.position.x + DEADZONE && grounded)
				player.velocity = new Vector2 (-moveSpeed, player.velocity.y);
			else if (player.position.x > cameraWall.transform.position.x + DEADZONE && !grounded)
				player.velocity = new Vector2 (-(moveSpeed*0.8f), player.velocity.y);
		} else 
		if (Input.GetKey (KeyCode.D)&& !Input.GetKey(KeyCode.S)) {
			dir = 1;
			facingRight = true;
			if(mario_state != JUMPING)
				mario_state = RUNNING;

			if(grounded)
				player.velocity = new Vector2 (moveSpeed, player.velocity.y);
			else if(!grounded)
				player.velocity = new Vector2 ((moveSpeed/0.8f), player.velocity.y);
		}
		if(Input.GetKey(KeyCode.S)) {
			animator.SetBool("isDucking", true);
			mario_state = DUCKING;
		}


		// check keys released
		if (Input.GetKeyUp (KeyCode.A)) {
			mario_state = 1;
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
			mario_state = IDLE;
			animator.SetBool("isDucking", false);
		}

		animatePlayer(dir);
		//Debug.Log (mario_state);
	}

	// Triger enter
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "BreakableBlock") {
			player.velocity = new Vector2 (player.velocity.x, (-player.velocity.y / 4));
			other.GetComponent<breakBlockScript> ().setHit (true, playerLives);
			audioManager.breakBlocks();
		}
		if (other.gameObject.tag == "upPipe") {
			audioManager.playPipe();
			audioManager.stopUnderGroundMusic();
			audioManager.startBackgroundMusic();
			player.transform.position = new Vector2(58.7f, 4.45f);
			cameraIsUnderGround = false;
		}
		if (other.gameObject.tag == "coinUnderGround") {
			collectCoin c = other.gameObject.GetComponent<collectCoin>();
			c.addCoinUnderGround();
			Destroy (other.gameObject);
		}
	}

	// If on a pipe
	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.tag == "DownPipe"){
			if(Input.GetKey(KeyCode.S)) {
				audioManager.playPipe();
				goDownPipe = true;	
				animator.SetBool("isDucking", true);
				cameraIsUnderGround = true;
				player.velocity = new Vector2(0, player.velocity.y);
				player.GetComponent<BoxCollider2D>().enabled = false;
			}
		}
	}

	// Hitting colliders
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "powerUp") {
			Destroy (other.gameObject);
			if (playerLives < 3)
				playerLives++;
		}

		if (!grounded && other.gameObject.tag == "Flag") {
			Debug.Log ("flag");
			if (!grounded && other.gameObject.tag == "Flag") 
				Debug.Log ("flag");
		}

	}
	// Method for making the player sprint
	void sprint() {
		if (isSprinting ()) {
			if(sprintDelay > 0)
				sprintDelay--;
			if(sprintDelay <= 0 && grounded)
				moveSpeed = 8;
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

	// Method for making Mario die
	public void Dies() {
		animator.SetBool ("isDead", true);

		// Play Mario Die sound
		audioManager.stopBackgroundMusic ();
		audioManager.playMarioDie ();

		// Make Mario jump out of the map
		player.velocity = new Vector2(player.velocity.x, 15f);

		// Disable collider so Mario falls through the floor
		marioCollider.enabled = false;

		Application.LoadLevel(3);
	}
	

	// Return lives
	public int getLives() {
		return playerLives;
	}

	// Return direction
	public int getDir() {
		return dir;
	}

	/*
	void OnGUI() {
		GUI.Box (new Rect(20, 20, 100, 100), "" + playerLives);
	}*/

}
