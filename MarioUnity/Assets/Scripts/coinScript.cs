using UnityEngine;
using System.Collections;

public class coinScript : MonoBehaviour {

	// To decide if the coin is static or not. 1 is static, 0 is not.s
	public bool isMovable;

	private int timer = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!isMovable) {
			timer--;
			if (timer <= 0) {
				Destroy (this.gameObject);
			}
		}
	}
}
