using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	public float velocity = 1f;
	private Rigidbody2D enemy;
	public Transform sightStart;
	public Transform sightEnd;
	public bool colliding;
	public LayerMask detectObject;

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		enemy.velocity = new Vector2 (velocity, enemy.velocity.y);

		colliding = Physics2D.Linecast (sightStart.position, sightEnd.position, detectObject);

		if (colliding) {
			transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
			velocity = -velocity;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine (sightStart.position, sightEnd.position);
	
	}
}
