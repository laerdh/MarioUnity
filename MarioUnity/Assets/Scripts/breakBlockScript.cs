using UnityEngine;
using System.Collections;

public class breakBlockScript : MonoBehaviour {

	/*
	 * This script is to make sure that the block is destroyed if th player hits it from the bottom
	 */

	private bool isHit;
	private Vector2 pos;
	public LayerMask playerLayer;
	private Rigidbody2D player;

	void Start() {
		pos = new Vector2 (transform.position.x, transform.position.y -0.2f);
		player = GameObject.Find ("Player").GetComponent<Rigidbody2D>();
	}

	void Update() {
		isHit = Physics2D.OverlapCircle (pos, 0.2f, playerLayer);
		if(isHit) {
			player.velocity = new Vector2(player.velocity.x, 0f);
			Destroy(this.gameObject);
		}
	}
}
