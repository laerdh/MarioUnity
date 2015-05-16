using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {
	
	// Player RigidBody
	//private Rigidbody2D player;
	
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
<<<<<<< HEAD
	
=======
	private const int DEAD = 5;

	// Mario lives 
	private int playerLives;
	private int playerLivesCurrent;

>>>>>>> c417384e9948d23481dc7497a42289607f4af89a
	// Move variables
	public float moveSpeed;
	public float jumpHeight;
	private float moveSpeedDef;
	private int sprintDelay = 10;
<<<<<<< HEAD
	
=======
	private int dir = 0;
	private bool goDownPipe = false;
	private int goDownPipeCounter = 50;

>>>>>>> c417384e9948d23481dc7497a42289607f4af89a
	// Animator
	private Animator animator;
	
	// variables for ground checking
	public Transform groundChecker;
	public float groundCheckerWidth;
	public LayerMask theGround;
	private bool grounded;
<<<<<<< HEAD
	public bool onPipe = false;
	public LayerMask thePipe;
	
	
=======


>>>>>>> c417384e9948d23481dc7497a42289607f4af89a
	// Direction boolean, used to check if player is facing right and move the camera
	private bool facingRight = true;
	
	// Camera and "wall" objects
	public GameObject cameraWall;
	public Camera camera;
	private bool moveTheCamera;
<<<<<<< HEAD
<<<<<<< HEAD
	//private const float DEADZONE = 0.1f; // The distance mario is allowed to walk before the screen stops 
	
	private const float DEADZONE = 2.1f; // The distance mario is allowed to walk before the screen stops 

	// BLOCKS
	public breakBlockScript breakBlock;
	
=======
	private const float DEADZONE = 1.5f; // The distance mario is allowed to walk before the screen stops 
=======
	private const float DEADZONE = 0.3f; // The distance mario is allowed to walk before the screen stops 
>>>>>>> a3fcb90bb174cc6f122ebaeb275c90beda8478ef
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

>>>>>>> c417384e9948d23481dc7497a42289607f4af89a
	void Start() {
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
	}
<<<<<<< HEAD
	
	void FixedUpdate() {
		
		grounded = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, theGround);
<<<<<<< HEAD
		/*if (!grounded) 
		grounded = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, thePipe);
		if (grounded) {
			onPipe = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, thePipe);
		}
		
		*/

		
		
		
		
		Debug.DrawLine (this.transform.position, groundChecker.position, Color.green);
		onPipe = Physics2D.Linecast (this.transform.position, groundChecker.position, 1 << LayerMask.NameToLayer ("Pipe"));
		//RaycastHit2D h = Physics2D.Raycast(this.transform.position, groundedEnd.position);
		//if(h.collider.tag == "pipe"){
		//	onPipe = true;
		//}
		
		
		/*if (!grounded) 
		grounded = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, thePipe);
		if (grounded) {
			onPipe = Physics2D.OverlapCircle (groundChecker.position, groundCheckerWidth, thePipe);
		}*/



		/*
		Debug.DrawLine (this.transform.position, groundedEnd.position, Color.green);
		//onPipe = Physics2D.Linecast (this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer ("Pipe"));
		RaycastHit2D h = Physics2D.Raycast(this.transform.position, groundedEnd.position);
		if(h.collider.tag == "pipe"){
			onPipe = true;
		}
		*/
=======
>>>>>>> c417384e9948d23481dc7497a42289607f4af89a


		if (!grounded) {
			mario_state = JUMPING;
		} else
			mario_state = IDLE;
<<<<<<< HEAD
		
		if (moveTheCamera) {
			camera.transform.position = new Vector3 (player.position.x, camera.transform.position.y, camera.transform.position.z);
=======

		if (!cameraIsUnderGround) {
			if (moveTheCamera) {
				camera.transform.position = new Vector3 (player.position.x, defaultCameraPos, camera.transform.position.z);
			}
		} else if (cameraIsUnderGround) {
			if(cameraUnderGroundCountdownDelay > -1)
				cameraUnderGroundCountdownDelay--;
			if(cameraUnderGroundCountdownDelay < 0)
				camera.transform.position = new Vector3 (underWorldCameraPosX, underWorldCameraPosY, camera.transform.position.z);
>>>>>>> c417384e9948d23481dc7497a42289607f4af89a
=======

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
>>>>>>> a3fcb90bb174cc6f122ebaeb275c90beda8478ef
		}
	}
	
	void Update() {
	if (!isFinished) {
			if(hasSuperStar) {
				superStarCountDown--;
				if(superStarCountDown<0) {
					hasSuperStar = false;
					animator.SetBool("hasSuperStar", false);
				}
			}

			//Blink mario if hurt
			hurtMario();

			// Load new level if dead
			if (isDead && transform.position.y < -20)
				Application.LoadLevel (3);

<<<<<<< HEAD
		//float dirX = Input.GetAxis ("Horizontal");
		keyBoardInput ();
		//animatePlayer(dirX);
		sprint();
		moveCamera ();
<<<<<<< HEAD
		
		
		
=======

>>>>>>> c417384e9948d23481dc7497a42289607f4af89a
		// Stop the player if he walks to the left of the screen
		if(player.position.x < cameraWall.transform.position.x + DEADZONE)
			player.transform.position = new Vector2(cameraWall.transform.position.x + DEADZONE,player.position.y);
=======
			goDownPipeCountDown ();

			changeColliderSize (playerLives);
>>>>>>> a3fcb90bb174cc6f122ebaeb275c90beda8478ef

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
			Debug.Log (player.GetComponent<BoxCollider2D> ().enabled);
			if (goDownPipe) {
				goDownPipeCounter--;
				//Debug.Log (goDownPipeCounter);

				if (goDownPipeCounter < 5) {
					audioManager.stopBackgroundMusic ();
					audioManager.UnderGroundMusic ();
					player.GetComponent<BoxCollider2D> ().enabled = true;
					if (player.GetComponent<BoxCollider2D> ().enabled == true)
						goDownPipe = false;
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
<<<<<<< HEAD
		
		if (onPipe && Input.GetKey (KeyCode.S)) {
			Debug.Log ("On pipe!");
			player.transform.position = new Vector2(-48,-12);
		
			
			
			
			//animator.SetBool("isOnPipe", true);
			
			
		}
		
	}
	
	// Break block
=======

		animatePlayer(dir);
		//Debug.Log (mario_state);
	}

	// Triger enter
>>>>>>> c417384e9948d23481dc7497a42289607f4af89a
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
<<<<<<< HEAD
	
=======

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
				Debug.Log ("flag");
				isFinished = true;
				other.collider.enabled = false;
				player.isKinematic = true;
				GameObject.Find ("FinishFlag").GetComponent<flagCompleteScript>().activate();
				//if (!grounded && other.gameObject.tag == "Flag")  
				//	Debug.Log ("flag");
			}

			if (other.gameObject.tag == "SuperStarTag") {
				Destroy(other.gameObject);
				hasSuperStar = true;
				superStarCountDown = 500;
				animator.SetBool("hasSuperStar", true);
			}
		}
	}
>>>>>>> c417384e9948d23481dc7497a42289607f4af89a
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
		if (transform.position.x > cameraWall.transform.position.x + 12.79394f && facingRight) {
			moveTheCamera = true;
		} else if (!facingRight) {
			moveTheCamera = false;
		}
	}

<<<<<<< HEAD
	/*void OnGUI() {
		GUI.Box (new Rect(20,20, 80,80),""+grounded);
	}
	*/
=======
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
		} else
			animator.SetBool ("isFinished", true);

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

>>>>>>> c417384e9948d23481dc7497a42289607f4af89a
}
