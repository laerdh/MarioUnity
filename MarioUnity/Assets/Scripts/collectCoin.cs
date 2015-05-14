using UnityEngine;
using System.Collections;

public class collectCoin : MonoBehaviour {
	
	public  AudioManager audioManager;
	int coin = 1;
	int collectedCoins = 0;

	// Use this for initialization
	public void addCoinUnderGround () {
		audioManager.underGroundCoin ();
		collectedCoins += coin;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
