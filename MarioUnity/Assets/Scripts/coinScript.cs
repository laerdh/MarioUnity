using UnityEngine;
using System.Collections;

public class coinScript : MonoBehaviour {

	// To decide if the coin is static or not. 1 is static, 0 is not.s
	public bool isMovable;
	private bool hasCollected = false;

	private ScoreManager scoreManager;

	private int timer = 50;

	// Use this for initialization
	void Start () {
		GameObject e = GameObject.Find("CoinsManager");
		scoreManager = e.GetComponent<ScoreManager> ();

		//if(isMovable) {
		//	GetComponent<Rigidbody2D> ().isKinematic = true;
		//}
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasCollected) {
			scoreManager.addCoin();
			hasCollected = true;
		}
		if (!isMovable) {
			timer--;
			if (timer <= 0) {
				Destroy (this.gameObject);
			}
		}
	}
}
