using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {


	Rigidbody player;
	Animator animator;
	public float moveSpeedIncrease;

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

	}


	// move player
	void movePlayer(float dirX) {
		player.velocity = new Vector3 (dirX * moveSpeedIncrease, 0f, 0f);

		if (dirX < 0) {
			player.transform.localScale = new Vector3 (-6, 6, 0);
		} else if (dirX > 0) {
			player.transform.localScale = new Vector3 (6, 6, 0);
		}
	}

	void animatePlayer(float dirX) {
		if (dirX != 0) {
			animator.SetBool ("isRunning", true);
		} else animator.SetBool ("isRunning", false);


	}
}
