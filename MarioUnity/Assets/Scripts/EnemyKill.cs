using UnityEngine;
using System.Collections;

public class EnemyKill : MonoBehaviour {

	private Rigidbody2D enemy;

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Rigidbody2D> ();
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
