using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private Rigidbody2D enemy;
	private float speed = -1f;
	private int direction = 5;
	private Animator animate;
	private int hit = 0;

	// Use this for initialization
	void Start () 
	{
		enemy = GetComponent<Rigidbody2D> ();
		animate = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		enemy.transform.localScale = new Vector3 (direction, 5, 0);
		enemy.velocity = new Vector2(speed, 0);
		animateEnemy (hit);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "pipe") {
			speed = -speed;
			direction = -direction;
		}

		if (other.gameObject.name == "Player") {
			speed = 0;
			if (gameObject.name == "Enemy_Turtle") {
				hit++;
				if (hit == 2) {
					DestroyObject(gameObject);
				}
			}
			if (gameObject.name == "Enemy_Goomba") {
				DestroyObject(gameObject);
			}
		}
	}

	void animateEnemy(int hit) {
		if (hit >= 1) {
			animate.SetBool ("isHit", true);
			
		} else {
			animate.SetBool ("isHit", false);
		}
		
	}
	
}
