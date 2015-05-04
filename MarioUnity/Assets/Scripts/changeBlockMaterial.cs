using UnityEngine;
using System.Collections;

public class changeBlockMaterial : MonoBehaviour {

	/*
	 * This script is to make sure that the player slides when colliding next to a brick,
	 * while still not sliding when on top of it.
	 * It does this by changing the PhysicsMaterial on the block
	 */

	public PhysicsMaterial2D mat;
	private bool isHit;
	private Vector2 pos;
	public LayerMask playerLayer;
	private Rigidbody2D player;
	private Collider2D coll;

	void Start() {
		coll = GetComponent<Collider2D> ();
		pos = new Vector2 (transform.position.x, transform.position.y +0.2f);
		player = GameObject.Find ("Player").GetComponent<Rigidbody2D>();
	}
	
	void Update() {
		isHit = Physics2D.OverlapCircle (pos, 0.2f, playerLayer);
		if (isHit) {
			coll.sharedMaterial = null;
		} else coll.sharedMaterial = mat;

	}
}
