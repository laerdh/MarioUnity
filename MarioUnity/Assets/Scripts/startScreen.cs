using UnityEngine;
using System.Collections;

public class startScreen : MonoBehaviour {
	
	private bool moveingTop = false;
	private bool moveingBottom = true;
	
	void FixedUpdate()
	{
		Vector2 currentPosition = this.transform.position;

		if(Input.GetKey(KeyCode.DownArrow) && moveingBottom){
			currentPosition = transform.position = (new Vector2(currentPosition.x, (currentPosition.y + -0.18f)));
			moveingBottom = false;
			moveingTop = true;
		}

		if(Input.GetKey(KeyCode.UpArrow) && moveingTop){
			currentPosition = transform.position = (new Vector2(currentPosition.x, (currentPosition.y + 0.18f)));
			moveingTop = false;
			moveingBottom = true;
		}

		if (Input.GetKey (KeyCode.Return)) {
			Application.LoadLevel (1);
		}
	}
}