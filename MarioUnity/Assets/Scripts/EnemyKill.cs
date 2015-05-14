using UnityEngine;
using System.Collections;

public class EnemyKill : MonoBehaviour {

	private Rigidbody2D enemy;

	public GameObject scoreLable;
	private ScoreLableScript sc;

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Rigidbody2D> ();
	}
	
	// Method if enemy is hit by fireBall
	public void gotShot() {

		gameObject.layer = 2;

		GameObject e = GameObject.Instantiate (scoreLable);
		e.transform.position = new Vector3(enemy.transform.position.x - 0.5f,enemy.transform.position.y + 1.5f, -8f);
		sc = e.GetComponent<ScoreLableScript> ();
		if (sc != null) {
			sc.setScore(200);
		}

		//velocity = 0;
		enemy.velocity = new Vector2(enemy.velocity.x, 5f);
		enemy.transform.localScale = new Vector2 (enemy.transform.localScale.x, -enemy.transform.localScale.y);
		
		// Disable collider so enemy falls through the floor
		this.GetComponent<Collider2D> ().enabled = false;
	}
}
