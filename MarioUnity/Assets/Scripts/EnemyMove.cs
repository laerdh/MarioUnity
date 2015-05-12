using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	public float velocity = 1f;
	private Rigidbody2D enemy;
	public Transform sightStart;
	public Transform sightEnd;
	public bool colliding;
	public LayerMask detectObject;


	public Animator animator;
	public Transform groundedEnd;

	private int hit = 0;
	
	// Koopa Troopa sweep mode
	private bool sweep = false;
	private bool stop = false;
	private int speed;

	// Delay before Goomba dies
	private bool timeDelay;
	private int time = 50;


	// Use this for initialization
	void Start () {
		enemy = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// If sweep mode activated, add speed. Stop = stop sweeping, else walk;
		if (sweep || stop) {
			enemy.velocity = new Vector2 (velocity * speed, 0);
		} else {
			enemy.velocity = new Vector2 (velocity, 0);
		}

		colliding = Physics2D.Linecast (sightStart.position, sightEnd.position, detectObject);

		if (colliding) {
			transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
			velocity *= -1;
		}

		// Destroys object after time delay (=100 frames)
		if (timeDelay) {
			time--;
			if (time == 0) {
				Destroy (this.gameObject);
			}
		}

	}

	// Visualize LineCast on enemy
	void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine (sightStart.position, sightEnd.position);
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Player") {
			if (this.gameObject.tag == "EnemyKoopa") {
				hit++;

				animator.SetBool ("isHit", true);

				// If Mario hits enemy every 2nd time, it starts sweeping
				if (hit % 2 == 0) {
					sweep = true;
					stop = false;
					speed = 5;
				} else {
					sweep = false;
					stop = true;
					speed = 0;
				}
			}
			if (this.gameObject.tag == "EnemyGoomba") {
				animator.SetBool ("isHit", true);
				velocity = 0;
				timeDelay = true;
			}
		}
	}


	void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.tag == ("Player")) {
			
		}
	}

	void Dies() {
		Destroy (this.gameObject);
	}
}
