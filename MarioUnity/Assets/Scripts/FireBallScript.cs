using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour {

	// Get access to the
	private PlayerShotScript shotList;
	private Transform playerPos;

	public int moveSpeed = 1;
	public int bounceHeight;

	private bool grounded;
	public LayerMask theGround;
	public LayerMask theEnemies;

	Rigidbody2D obj;

	// Use this for initialization
	void Start () {
		obj = GetComponent<Rigidbody2D> ();
		shotList = GameObject.Find("Player").GetComponent<PlayerShotScript> ();
		playerPos = GameObject.Find("Player").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerPos = GameObject.Find("Player").GetComponent<Transform> ();
		grounded = Physics2D.OverlapCircle (new Vector2(transform.position.x, transform.position.y-0.01f), 0.2f, theGround);
		if (grounded) {
			obj.velocity = new Vector2 (moveSpeed, bounceHeight);
		} else obj.velocity = new Vector2 (moveSpeed, obj.velocity.y);
	
		hitEnemies ();
		hitWalls ();
		outOfBounds ();
	}

	// Check if hitting walls
	void hitWalls() {
		RaycastHit2D hitRight = Physics2D.Raycast (transform.position, new Vector2(0.5f, 0f) , 0.2f, theGround);
		Debug.DrawRay (transform.position,  new Vector2(0.3f, 0f), Color.red);
		
		RaycastHit2D hitLeft = Physics2D.Raycast (transform.position, new Vector2(-0.5f, 0f), 0.2f, theGround);
		Debug.DrawRay (transform.position,  new Vector2(-0.3f, 0f), Color.red);
		if (hitRight || hitLeft) {
			destroy ();
		}
	}

	// Check if hitting enemies
	void hitEnemies() {
		RaycastHit2D hitRight = Physics2D.Raycast (transform.position, new Vector2(0.5f, 0f) , 0.2f, theEnemies);
		Debug.DrawRay (transform.position,  new Vector2(0.3f, 0f), Color.red);

		RaycastHit2D hitLeft = Physics2D.Raycast (transform.position, new Vector2(-0.5f, 0f), 0.2f, theEnemies);
		Debug.DrawRay (transform.position,  new Vector2(-0.3f, 0f), Color.red);

		RaycastHit2D hitUp = Physics2D.Raycast (transform.position, new Vector2(0f, 0.5f), 0.2f, theEnemies);
		Debug.DrawRay (transform.position,  new Vector2(0f, 0.3f), Color.red);

		RaycastHit2D hitDown = Physics2D.Raycast (transform.position, new Vector2(0f, -0.5f), 0.2f, theEnemies);
		Debug.DrawRay (transform.position,  new Vector2(0f, -0.3f), Color.red);

		if (hitRight || hitLeft || hitUp || hitDown) {

			// Put enemy death here
			if(hitRight.collider != null) {
				Rigidbody2D enemy = hitRight.collider.attachedRigidbody;
				if(enemy != null)
				enemy.GetComponent<EnemyKill>().gotShot();
			}

			if(hitLeft.collider != null) {
				Rigidbody2D enemyLeft = hitLeft.collider.attachedRigidbody;
				if(enemyLeft != null)
				enemyLeft.GetComponent<EnemyKill>().gotShot();
			}

			if(hitUp.collider != null) {
				Rigidbody2D enemyUp = hitUp.collider.attachedRigidbody;
				if(enemyUp != null)
					enemyUp.GetComponent<EnemyKill>().gotShot();
			}

			if(hitDown.collider != null) {
				Rigidbody2D enemyDown = hitDown.collider.attachedRigidbody;
				if(enemyDown != null)
					enemyDown.GetComponent<EnemyKill>().gotShot();
			}

			destroy ();
		}
	}

	// Check if outside of screen
	void outOfBounds() {
		float distance = Vector2.Distance (this.transform.position, playerPos.transform.position);
		if (distance > 15)
			destroy ();
	}

	// Set the direction of the bull
	public void setDirection(int dir) {
		if (dir == -1) {
			moveSpeed = -moveSpeed;
		}
	}

	public void destroy() {
		shotList.removeFromList(this.gameObject);
		Destroy(this.gameObject);
	}

}
