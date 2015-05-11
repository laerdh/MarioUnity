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
	private bool sweepMode;
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
		//Debug.Log ("" + other.gameObject.tag);
		if (other.gameObject.tag == ("Untagged")) {
			//Debug.Log("Funker");
		
		}
	}

	void Dies() {
		Destroy (this.gameObject);
	}
}
