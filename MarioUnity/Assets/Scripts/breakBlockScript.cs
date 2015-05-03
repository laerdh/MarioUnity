using UnityEngine;
using System.Collections;

public class breakBlockScript : MonoBehaviour {

	private bool isHit;
	private Vector2 pos;
	public LayerMask player;

	void Start() {
		pos = new Vector2 (transform.position.x, transform.position.y);
	}

	void Update() {
		isHit = Physics2D.OverlapCircle (pos, 0.5f, player);
		if(isHit) {
			print("hit");
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		//Destroy (this.gameObject);
	}
}
