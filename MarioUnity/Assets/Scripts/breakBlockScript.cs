using UnityEngine;
using System.Collections;

public class breakBlockScript : MonoBehaviour {

	/*
	 * This script is to make sure that the block is destroyed if th player hits it from the bottom
	 */

	// Position and hit variables
	private bool isHit;
	private bool canBeHit;
	private bool bumpBox;
	private Vector2 pos;
	private Vector2 pos2;
	private int playerLives; // is given by player on hit. decides what will happen once a hit occurs

	// Variables what type of box
	public GameObject content; // what object the box spawns
	private int boxtype;
	private const int NORMALBLOCK = 0;
	private const int QUESTIONBLOCK = 1;
	private const int SOLIDBLOCK = 2;
	private const int HIDDENBLOCK = 3;
	private const int MULTICOIN = 4;
	private const int EMPTYBLOCK = 5;
	public GameObject breakBlockPrefab;
	public enum BoxTypes {
		NORMALBLOCK, QUESTIONBLOCK, SOLIDBLOCK, HIDDENBLOCK, MULTICOIN
	}
	public BoxTypes boxTypes;

	private int coinCounter = 12;

	Animator animator;

	//AudioManager
	public AudioManager audioManager;

	void Start() {
		animator = GetComponent<Animator> ();
		boxtype = (int)boxTypes;
		setAnimation (boxtype);
		bumpBox = false;
		isHit = false;
		canBeHit = true;
		pos = transform.position;
		pos2 = new Vector2 (transform.position.x, transform.position.y +0.3f);
	}

	void Update() {


		if(isHit) {
			switch(boxtype) {
			case NORMALBLOCK:
				if(playerLives == 1) {
					bumpBox = true;
					//bump lyd
				}
				else if(playerLives >= 2) {
					GameObject.Find ("AudioController").GetComponent<AudioManager> ().breakBlocks();
					SpawnExplosion();
					Destroy(this.gameObject);
				}
				break;
			case QUESTIONBLOCK:
				boxtype = EMPTYBLOCK;
				// Only let the box "bump" first time it is hit 
				if(canBeHit){				
					bumpBox = true;
				}
				isHit = false;
				animator.SetBool("isEmpty", true);
				canBeHit = false;
				break;
			case SOLIDBLOCK:
				canBeHit = false;
				break;
			case HIDDENBLOCK:
				bumpBox = true;
				animator.SetBool("isEmpty", true);
				SpriteRenderer sp = GetComponent<SpriteRenderer>();
				sp.enabled = true;
				canBeHit = false;
				break;
			case MULTICOIN:
				// Only let the box "bump" first time it is hit 
				if(canBeHit){				
					bumpBox = true;
				}
				bumpBox = true;
				canBeHit = true;
				isHit = false;
				break;
			case EMPTYBLOCK:

				break;
			}
		}
		BumpBox ();
	}

	// Method for bouncing the box if mario hits when small
	void BumpBox() {
		if (bumpBox) {
			// Move box up towards higher position
			if((Vector2)transform.position != pos2 && isHit) {
				transform.position = new Vector2(transform.position.x, transform.position.y +0.1f);
			}
			// If higher position is reached set isHit to false
			else if((Vector2)transform.position == pos2) {
				isHit = false;
			}
			// If it is not at original position and isHit is false, move back down
			if((Vector2)transform.position != pos && !isHit) {
				transform.position = new Vector2(transform.position.x, transform.position.y -0.1f);
			}
			// If the position is the same as the original position, box is hittable and animation stops
			if((Vector2)transform.position == pos) {
				canBeHit = true;
				bumpBox = false;
			}
		} 
	}

	// Method for spawning and explosion of bricks if mario hits when big
	void SpawnExplosion() {
		for (int i = 1; i <= 4; i++) {
			GameObject e = GameObject.Instantiate (breakBlockPrefab);
			e.GetComponent<blockBreakEffect> ().setVelocity (i);
			e.transform.position = transform.position;
		}
	}

	// Method for spawning content
	void SpawnContent() {
		if (content.tag == "coin") {
			if(boxtype == QUESTIONBLOCK) {
				spawnCoin ();
				content = null;
			} else if(boxtype == MULTICOIN) {
				if(coinCounter > 0){
					coinCounter--;
					spawnCoin();
				}
				
			}
		} else if (content.tag == "powerUp" || content.tag == "SuperStarTag" || content.tag == "ExtraLife" ) {
			GameObject e = GameObject.Instantiate (content);
			powerUpScript ps = e.GetComponent<powerUpScript>();
			if(ps != null) {
				ps.setState(playerLives);
			}
			Rigidbody2D re= e.GetComponent<Rigidbody2D>();
			re.transform.position = new Vector3(transform.position.x, transform.position.y+1, 1);
			content = null;
			//re.velocity = new Vector2(0,8);
		}
	}

	// Spawn a coin
	void spawnCoin() {
		GameObject e = GameObject.Instantiate (content);
		Rigidbody2D re= e.GetComponent<Rigidbody2D>();
		re.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
		re.velocity = new Vector2(0,8);
	}

	public void setHit(bool isHit, int playerLives) {
		if (boxtype == EMPTYBLOCK)
			GameObject.Find ("AudioController").GetComponent<AudioManager> ().EmptyBlockSound ();

		this.playerLives = playerLives;
		if(content != null) { 
			bumpBox = true;
			SpawnContent(); 
			//content = null;
		}

		if (canBeHit) {
			canBeHit = false;
			this.isHit = isHit;
			this.playerLives = playerLives;
		}
	}
	void setAnimation(int i) {
		if (i == QUESTIONBLOCK)
			animator.SetBool ("isCoinBox", true);
		else if (i == HIDDENBLOCK) {
			animator.SetBool ("isInvisBox", true);
			SpriteRenderer sp = GetComponent<SpriteRenderer>();
			sp.enabled = false;
		}
		else if (i == SOLIDBLOCK) {
			animator.SetBool ("isSolid", true);
		}
	}
}
