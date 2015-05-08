using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	public float velocity = -1f;
	private Rigidbody2D enemy;
	public Transform sightStart;
	public Transform sightEnd;
	public bool colliding;
	public LayerMask detectObject;
	public Transform weakness;
	private int hit = 0;
	public Animator animator;

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
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine (sightStart.position, sightEnd.position);
	
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.name == "Player") {
			if (this.gameObject.tag == "EnemyTurtle") {
				hit++;
				animator.SetBool ("isHit", true);

				// If Mario hits enemy for the 2nd time, it starts sweeping
				if (hit % 2 == 0 && hit != 0) {
					velocity = 8f;
				} else {
					velocity = 0f;
				}
			}

			if (this.gameObject.tag == "EnemyGoomba") {
				animator.SetBool ("isHit", true);
				Dies ();
			}
			//float height = other.contacts[0].point.y - weakness.position.y;

			other.rigidbody.AddForce(new Vector2(0, 300));
		}
	}

	void Dies() {
		Destroy (this.gameObject);
	}
}
