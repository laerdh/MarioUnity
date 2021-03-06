﻿using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

	// Player RigidBody
	public Rigidbody2D player;
	public BoxCollider2D marioCollider;
	public Score scores;
	private ScoreLableScript sc;
	public GameObject scoreLable;


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
	private int goDownPipeCounter = 60;

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
	private const float DEADZONE = 0.3f; // The distance mario is allowed to walk before the screen stops 
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

	private bool isDead = false;
	private bool hurt = false;
	private int hurtBlink = 10;

	// Boolean checking if mario has finished the level or not
	private bool isFinished;
	private int finishedCounter = 40;
	public Transform finishPoint;

	// Variables for controlling superstar powerup
	private bool hasSuperStar;
	private int superStarCountDown;

	void Start() {

		// Delete One AudioController if multiple are in scene
		GameObject[] audioControllers = GameObject.FindGameObjectsWithTag ("AudioControlTag");
		if (audioControllers.Length > 1) {
			Destroy(audioControllers[audioControllers.Length-1].gameObject);
		}

		isFinished = false;
		playerLives = 1;
		playerLivesCurrent = playerLives;
		
		player = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		animator.SetInteger ("MarioLives",	playerLives);
		moveSpeedDef = moveSpeed;
		camera.transform.position = new Vector3 (player.position.x, camera.transform.position.y, camera.transform.position.z);
		defaultCameraPos = camera.transform.position.y;
		underWorldCameraPosY = -8.3f;
		underWorldCameraPosX = -42.42869f;
		cameraIsUnderGround = false;
		//Test
		GameObject w = GameObject.Find("AudioController");
		audioManager = w.GetComponent<AudioManager>();
		audioManager.startBackgroundMusic ();
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(groundChecker.position, groundCheckerWidth);
	}
	
	void FixedUpdate() {
		if (!isFinished) {
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
				if (cameraUnderGroundCountdownDelay > -1)
					cameraUnderGroundCountdownDelay--;
				if (cameraUnderGroundCountdownDelay < 0)
					camera.transform.position = new Vector3 (underWorldCameraPosX, underWorldCameraPosY, camera.transform.position.z);
			}
		}
	}

	void Update() {
	if (!isFinished) {
			if(hasSuperStar) {
				superStarCountDown--;
				if(superStarCountDown<0) {
					audioManager.StopStarMusic();
					audioManager.startBackgroundMusic();
					hasSuperStar = false;
					animator.SetBool("hasSuperStar", false);
				}
			}

			//Blink mario if hurt
			hurtMario();

			// Load new level if dead
			if (isDead && transform.position.y < -20)
				Application.LoadLevel (3);

			goDownPipeCountDown ();

			changeColliderSize (playerLives);

			if (playerLivesCurrent != playerLives) {
				animator.SetInteger ("MarioLives", playerLives);
				playerLivesCurrent = playerLives;
			}

			//float dirX = Input.GetAxis ("Horizontal");
			keyBoardInput ();
			//animatePlayer(dirX);
			sprint ();
			moveCamera ();

			// Stop the player if he walks to the left of the screen
			if (player.position.x < cameraWall.transform.position.x + DEADZONE)
				player.transform.position = new Vector2 (cameraWall.transform.position.x + DEADZONE, player.position.y);
		} else if (isFinished) {
			animateOnComplete();
		}
	}

	public void goDownPipeCountDown() {
			if (goDownPipe) {
				goDownPipeCounter--;

				if (goDownPipeCounter < 5) {
					audioManager.stopBackgroundMusic ();
					audioManager.UnderGroundMusic ();
					player.GetComponent<BoxCollider2D> ().enabled = true;
					if (player.GetComponent<BoxCollider2D> ().enabled == true) {
						goDownPipe = false;
						goDownPipeCounter = 60;
					}
				}
			}
	}

	// Hurt, make mario blink on taking dama
	void hurtMario() {
		if (hurt) {
			hurtBlink--;
			if(hurtBlink % 2 != 0) {
				GetComponent<SpriteRenderer>().enabled = false;
			} else GetComponent<SpriteRenderer>().enabled = true;
			if(hurtBlink == 0) {
				hurt = false;
				hurtBlink = 10;
			}
		}
	}

	// Endre collider
	public void changeColliderSize(int playerLives) {
		BoxCollider2D b = GetComponent<BoxCollider2D> ();
		if(b != null)
		{
			if(playerLives == 1) {
				b.size =	new Vector2(0.15f,0.16f);
			}
			else if(playerLives >= 2)
				b.size = new Vector2(0.15f, 0.32f);
		}
	}

	// Method for checking keyboard input
	void keyBoardInput() {

		// Get Mario fall speed;
		float fallSpeed = player.velocity.y;

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
			mario_state = IDLE;
			animator.SetBool("isDucking", false);
		}

		animatePlayer(dir);
	}

	// Triger enter
	void OnTriggerEnter2D(Collider2D other) {
		if (!isFinished) {
			if (other.gameObject.tag == "BreakableBlock") {
				player.velocity = new Vector2 (player.velocity.x, (-player.velocity.y / 4));
				other.GetComponent<breakBlockScript> ().setHit (true, playerLives);
	
			}
			if (other.gameObject.tag == "upPipe") {
				audioManager.playPipe ();
				audioManager.stopUnderGroundMusic ();
				audioManager.startBackgroundMusic ();
				player.transform.position = new Vector2 (58.7f, 4.45f);
				cameraIsUnderGround = false;
			}
			if (other.gameObject.tag == "coinUnderGround") {
				collectCoin c = other.gameObject.GetComponent<collectCoin> ();
				if(c != null)
					c.addCoinUnderGround ();
				Destroy (other.gameObject);
			}
			if (other.gameObject.tag == "deathDetection") {
				Dies ();	
			}
			if (other.gameObject.tag == "tagOfDeath") {
				Dies ();
			}
		}
	}

	// If on a pipe
	void OnTriggerStay2D(Collider2D other) {
		if (!isFinished) {
			if (other.gameObject.tag == "DownPipe") {
				if (Input.GetKey (KeyCode.S)) {
					audioManager.playPipe ();
					goDownPipe = true;	
					animator.SetBool ("isDucking", true);
					cameraIsUnderGround = true;
					player.velocity = new Vector2 (0, player.velocity.y);
					player.GetComponent<BoxCollider2D> ().enabled = false;
				}
			}
		}
	}

	// Hitting colliders
	void OnCollisionEnter2D(Collision2D other) {
		if (!isFinished) {
			if (other.gameObject.tag == "powerUp") {

				if (playerLives == 1) {
					animator.Play ("fromSmallToBig");
				} else if (playerLives == 2) {
					animator.Play ("fromBigToFire");
				}
				audioManager.MarioPwrUp ();
				Destroy (other.gameObject);
				scores.AddScoreAmount (1000);
				GameObject e = GameObject.Instantiate (scoreLable);
				e.transform.position = new Vector3 (transform.position.x - 0.5f, transform.position.y + 1.5f, -8f);
				sc = e.GetComponent<ScoreLableScript> ();
				if (sc != null) {
					sc.setScore (1000);
				}
				if (playerLives < 3)
					playerLives++;
			}

			if (other.gameObject.tag == "Flag") {
				isFinished = true;
				other.collider.enabled = false;
				player.isKinematic = true;
				audioManager.stopBackgroundMusic();
				audioManager.PlayFlagPole();
				GameObject.Find ("FinishFlag").GetComponent<flagCompleteScript>().activate();
			}

			if (other.gameObject.tag == "SuperStarTag") {
				audioManager.MarioPwrUp ();
				audioManager.stopBackgroundMusic();
				audioManager.PlayStarMusic();
				Destroy(other.gameObject);
				hasSuperStar = true;
				superStarCountDown = 500;
				animator.SetBool("hasSuperStar", true);
			}
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
		Vector3 middleOfScreen = Camera.main.ViewportToWorldPoint (new Vector3(0.5f, 0f, 0));
		Vector3 leftOfScreen = Camera.main.ViewportToWorldPoint (new Vector3(0f, 0f, 0));
		float distanceToMid = middleOfScreen.x - leftOfScreen.x;
		//if (transform.position.x > cameraWall.transform.position.x + 12.79394f && facingRight) {
		if (transform.position.x > cameraWall.transform.position.x + distanceToMid && facingRight) {
			moveTheCamera = true;
		} else if (!facingRight) {
			moveTheCamera = false;
		}
	}

	// Method for making Mario die
	public void Dies() {
		animator.Play ("PlayerSmallDeadAnimation");

		// Play Mario Die sound
		audioManager.stopBackgroundMusic ();
		audioManager.playMarioDie ();

		// Make Mario jump out of the map
		player.GetComponent<BoxCollider2D> ().enabled = false;
		player.velocity = new Vector2(player.velocity.x, 15f);

		// Disable collider so Mario falls through the floor
		marioCollider.enabled = false;

		isDead = true;
	}

	void animateOnComplete() {
		if (finishedCounter > 0) {
			finishedCounter--;

			switch (playerLives) {
			case 1:
				animator.Play ("MarioSmallOnPole");
				break;
			case 2:
				animator.Play ("MarioBigOnPole");
				break;
			case 3:
				animator.Play ("MarioFireOnPole");
				break;
			}
		} else {
			animator.SetBool ("isFinished", true);

		}

		if (finishedCounter == 1) {
			audioManager.PlayComplete ();
		}

		if (finishedCounter == 0) {

			if(player.transform.position.x < finishPoint.transform.position.x) {
				player.transform.position = new Vector2(player.transform.position.x + 0.05f,player.transform.position.y);
			} else if(player.transform.position.x >= finishPoint.transform.position.x) {
				GetComponent<SpriteRenderer>().enabled = false;
			}
		}

	}

	public void removeLife() {
		playerLives--;
		hurt = true;
		if (playerLives == 0) {
			Dies();
		}
	}

	// Return lives
	public int getLives() {
		return playerLives;
	}

	// Return direction
	public int getDir() {
		return dir;
	}

	// return grounded
	public bool isGrounded() {
		return grounded;
	}

	public Vector2 getPos() {
		return transform.position;
	}

	public bool getHasSuperStar() {
		return hasSuperStar;
	}

	/*
	void OnGUI() {
		GUI.Box (new Rect(20, 20, 100, 100), "" + playerLives);
	}*/

}
