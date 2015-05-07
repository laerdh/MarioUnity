using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private Rigidbody2D enemyTurtle;
	private float speed = -1f;
	private int direction = 5;
	// Use this for initialization
	void Start () 
	{
		enemyTurtle = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		enemyTurtle.transform.localScale = new Vector3 (direction, 5, 0);
		enemyTurtle.velocity = new Vector2(speed, 0);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "pipe") {
			Debug.Log ("Test");
			speed = -speed;
			direction = -direction;
		}
	}
}
