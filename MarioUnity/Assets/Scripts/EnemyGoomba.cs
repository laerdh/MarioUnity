using UnityEngine;
using System.Collections;

public class EnemyGoomba : MonoBehaviour {
	public PlayerMoveScript mario;
	public Score score;

	private Animator anim;
	public Rigidbody2D enemy;
	public float velocity = -1f;
	public Transform sightStart;
	public Transform sightEnd;
	public LayerMask detectObject;
	public Transform weakness;


	private bool colliding;

	// Use this for initialization
	void Start () 
	{
		enemy = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		enemy.velocity = new Vector2 (velocity, enemy.velocity.y);

		colliding = Physics2D.Linecast (sightStart.position, sightEnd.position, detectObject);

		if (colliding) {
			transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
			velocity *= -1;
		}
	}

	void OnDrawGizmos() 
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine (sightStart.position, sightEnd.position);
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			float height = other.contacts[0].point.y - weakness.position.y;

			if (height > 0) 
			{
				Dies();
				score.AddScore();
				other.rigidbody.AddForce(new Vector2 (0, 300));
			} else 
			{
				mario.Dies ();
			}
		}
	}

	void Dies() 
	{
		velocity = 0;
		anim.SetBool ("isHit", true);
		Destroy (this.gameObject, 0.5f);
		gameObject.tag = "neutralized";
	}

	// Method if enemy is hit by fireBall
	public void gotShot() {
		//velocity = 0;
		enemy.velocity = new Vector2(enemy.velocity.x, 5f);
		enemy.transform.localScale = new Vector2 (enemy.transform.localScale.x, -enemy.transform.localScale.y);
		
		// Disable collider so enemy falls through the floor
		this.GetComponent<Collider2D> ().enabled = false;
	}
}
