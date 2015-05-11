using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	public float velocity = -1f;
	private Rigidbody2D enemy;
	public Transform sightStart;
	public Transform sightEnd;
	public bool colliding;
	public LayerMask detectObject;
<<<<<<< HEAD
	public Animator animator;

	public Transform groundedEnd;


	private Rigidbody2D enemy;
=======
	public Transform weakness;
>>>>>>> 0670135d9fff654d039e3a9ca9547bf06863095c
	private int hit = 0;
	private bool sweepMode;
	public Animator animator;

<<<<<<< HEAD
	// Koopa Troopa sweep mode
	private bool sweep = false;
	private bool stop = false;
	private int speed;

	// Delay before Goomba dies
	private bool timeDelay;
	private int time = 50;

<<<<<<< HEAD
	public BoxCollider2D boxCol;
	public CircleCollider2D cirCol;
	public GameObject other;
	private EnemyMove enemyScript;

=======
=======
>>>>>>> 0670135d9fff654d039e3a9ca9547bf06863095c
>>>>>>> b07f7044ccaa6ff1b640a27500e08e2e84a1f240

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		enemyScript = other.GetComponent<EnemyMove> ();
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		// If sweep mode activated, add speed. Stop = stop sweeping, else walk;
		if (sweep || stop) {
			enemy.velocity = new Vector2 (velocity * speed, 0);
		} else {
			enemy.velocity = new Vector2 (velocity, 0);
		}
=======
		enemy.velocity = new Vector2 (velocity, 0);
>>>>>>> b07f7044ccaa6ff1b640a27500e08e2e84a1f240

		colliding = Physics2D.Linecast (sightStart.position, sightEnd.position, detectObject);

		if (colliding) {
			transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
			velocity *= -1;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine (sightStart.position, sightEnd.position);
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Player") {
			if (this.gameObject.tag == "EnemyTurtle") {
				hit++;
				animator.SetBool ("isHit", true);

				// If Mario hits enemy for the 2nd time, it starts sweeping
				if (hit > 1) {
					sweepMode = true;
				} else {
					velocity = 0;
				}
			}

			if (this.gameObject.tag == "EnemyGoomba") {
				animator.SetBool ("isHit", true);
				Dies ();

				Rigidbody2D other1 = other.GetComponent<Rigidbody2D>();
				//other.GetComponent<Rigidbody2D>
				//other1.velocity = new Vector2 (other.transform.position.x, 2);
				other1.AddForce(new Vector2(0,1300));
			}
			//float height = other.contacts[0].point.y - weakness.position.y;

			//other.rigidbody.AddForce(new Vector2(0, 300));

	
		}
	}

	void OnCollisionEnter2D(Collision2D other){
<<<<<<< HEAD

=======
>>>>>>> 0670135d9fff654d039e3a9ca9547bf06863095c
		//Debug.Log ("" + other.gameObject.tag);
		if (other.gameObject.tag == ("Untagged")) {
			//Debug.Log("Funker");
		
<<<<<<< HEAD

			if (other.gameObject.tag == ("Player")) {
				if (!stop) {
					Destroy (other.gameObject);
				}

			}
=======
>>>>>>> 0670135d9fff654d039e3a9ca9547bf06863095c
		}

		// Destroy other enemies if sweeping Koopa collides with them
		if (sweep && other.gameObject.tag == this.gameObject.tag) {
			this.other.boxCol.enabled = false;
			enemyScript.cirCol.enabled = false;
		}
	}

	void Dies() {
		Destroy (this.gameObject);
	}
}
