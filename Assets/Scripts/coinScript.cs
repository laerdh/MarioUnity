using UnityEngine;
using System.Collections;

public class coinScript : MonoBehaviour {

	// To decide if the coin is static or not. 1 is static, 0 is not.s
	public bool isMovable;
	private bool hasCollected = false;

	private Coins coin;
	public Score scores;
	//private AudioManager audioManager;
	private int timer = 50;

	// Use this for initialization
	void Start () {
		scores = GameObject.Find ("Score").GetComponent<Score> ();
		GameObject e = GameObject.Find("CoinsManager");
		coin = e.GetComponent<Coins> ();

		GameObject.Find ("AudioController").GetComponent<AudioManager> ().underGroundCoin();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasCollected) {
			coin.addCoin();
			hasCollected = true;
			scores.AddScoreAmount(200);
		}
		if (!isMovable) {
			timer--;
			if (timer <= 0) {
				Destroy (this.gameObject);
			}
		}
	}
}
