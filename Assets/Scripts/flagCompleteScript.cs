using UnityEngine;
using System.Collections;

public class flagCompleteScript : MonoBehaviour {

	private bool fall = false;

	public Transform bottom;

	// Update is called once per frame
	void Update () {
		if (transform.position.y > bottom.transform.position.y && fall) {
			transform.position = new Vector2(transform.position.x, transform.position.y - 0.08f);
		}
	}

	public void activate() {
		if(!fall) fall = true;
	}
}
