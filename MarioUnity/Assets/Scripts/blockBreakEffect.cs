using UnityEngine;
using System.Collections;

public class blockBreakEffect : MonoBehaviour {

	private int direction;
	private Rigidbody2D obj;
	
	void Awake() {
		obj = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0,0,-5f));
		if (obj.position.y < -20) {
			Destroy(this.gameObject);
		}
	}

	public void setVelocity(int dir) {
		this.direction = dir;
		
		//Rigidbody2D obj = GetComponent<Rigidbody2D> ();
		
		switch (direction) {
		case 1:
			obj.velocity = new Vector2(-2, 6);
			break;
		case 2: 
			obj.velocity = new Vector2(-3, 10);
			break;
		case 3:
			obj.velocity = new Vector2(2, 6);
			break;
		case 4:
			obj.velocity = new Vector2(3, 10);
			break;
		}
	}


}
