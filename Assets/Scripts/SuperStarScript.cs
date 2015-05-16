using UnityEngine;
using System.Collections;

public class SuperStarScript : MonoBehaviour {

	Rigidbody2D obj;

	public float moveSpeed;

	private bool grounded;

	public LayerMask theGround;

	private int ttl = 500;

	// Use this for initialization
	void Start () {
		obj = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		ttl--;
		if (ttl < 0)
			Destroy (this.gameObject);

		grounded = Physics2D.OverlapCircle (new Vector2(obj.transform.position.x, obj.transform.position.y - 0.5f), 0.3f, theGround);
		if(grounded) {
			obj.velocity = new Vector2 (obj.velocity.x,6.5f);
			print ("hit the floor");
		}

		obj.velocity = new Vector2 (moveSpeed,obj.velocity.y);
	}
}
