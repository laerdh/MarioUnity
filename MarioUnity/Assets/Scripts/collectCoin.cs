using UnityEngine;
using System.Collections;

public class collectCoin : MonoBehaviour {

	public ScoreManager scoreManager;

	// Use this for initialization
	void Start () {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "coin") {
			//print ("traff en mynt");
			Destroy(other.gameObject);
			scoreManager.addCoin();
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
