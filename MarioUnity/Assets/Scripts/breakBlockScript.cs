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
	private const int HARDBLOCK = 2;
	private const int HIDDENBLOCK = 3;
	private const int MULTICOIN = 4;
	public GameObject breakBlockPrefab;
	public enum BoxTypes {
		NORMALBLOCK, QUIESTIONBLOCK, HARDBLOCK, HIDDENBLOCK
	}
	public BoxTypes boxTypes;

	private Animator animator;

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
		BumpBox ();

		if(isHit) {
			switch(boxtype) {
			case NORMALBLOCK:
				if(playerLives == 1) {
					bumpBox = true;
				}
				else if(playerLives >= 2) {
					SpawnExplosion();
					Destroy(this.gameObject);
				}
				break;
			case QUESTIONBLOCK:
				bumpBox = true;
				animator.SetBool("isEmpty", true);
				canBeHit = false;
				break;
			case HARDBLOCK:
				break;
			case HIDDENBLOCK:
				break;
			case MULTICOIN:
				break;
			}
		}
	}

	// Method for bouncing the box if mario hits when small
	void BumpBox() {
		if (bumpBox) {
			if((Vector2)transform.position != pos2 && isHit) {
				transform.position = new Vector2(transform.position.x, transform.position.y +0.1f);
			} else if((Vector2)transform.position == pos2) {
				isHit = false;
			}
			if((Vector2)transform.position != pos && !isHit) {
				transform.position = new Vector2(transform.position.x, transform.position.y -0.1f);
			}
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

	public void setHit(bool isHit, int playerLives) {
		if (canBeHit) {
			canBeHit = false;
			this.isHit = isHit;
			this.playerLives = playerLives;
		}
	}

	void setAnimation(int i) {
		if(i == 1)
			animator.SetBool("isCoinBox", true);
	}
}
