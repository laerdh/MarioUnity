using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	public float velocity = -1f;
	public Transform sightStart;
	public Transform sightEnd;
	public bool colliding;
	public LayerMask detectObject;
	public Animator animator;
	private Rigidbody2D enemy;
	private int hit = 0;

	// Koopa Troopa sweep mode
	private bool sweep = false;

	// Timedelay when Goomba dies
	private bool timeDelay;
	private int time = 100;

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		enemy.velocity = new Vector2 (velocity, 0);

		colliding = Physics2D.Linecast (sightStart.position, sightEnd.position, detectObject);

		if (colliding) {
			transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
			velocity *= -1;
		}

		if (sweep) {
			velocity = -velocity * 1;
			Debug.Log ("Sweep activated");
		} else {
			Debug.Log ("Sweep deactivated");
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
		Rigidbody2D player = other.GetComponent<Rigidbody2D>();

		if (other.gameObject.name == "Player") {
			if (this.gameObject.tag == "EnemyTurtle") {
				hit++;
				animator.SetBool ("isHit", true);

				// Add force to Mario if he jumps on a Koopa
				player.AddForce(new Vector2(0,800));

				// If Mario hits enemy every 2nd time, it starts sweeping
				if (hit % 2 == 0) {
					sweep = true;
				} else {
					sweep = false;
				}
			}

			if (this.gameObject.tag == "EnemyGoomba") {
				animator.SetBool ("isHit", true);
				velocity = 0;
				timeDelay = true;

				// Add force to Mario if he jumps on a Goomba
				player.AddForce(new Vector2(0,800));
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		Debug.Log ("" + other.gameObject.tag);
		if (other.gameObject.tag == ("Untagged")) {
			Debug.Log("Funker");
		}
	}

	void OnGUI() {
		GUI.Box (new Rect(100,20, 50,50),"sweep\n"+sweep);
	}
}
