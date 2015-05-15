using UnityEngine;
using System.Collections;

public class collectCoin : MonoBehaviour {

	int coin = 1;
	int collectedCoins = 0;
	
	// Use this for initialization
	public void addCoinUnderGround () {
		AudioManager audioManager = GameObject.Find ("AudioController").GetComponent<AudioManager> ();
		audioManager.underGroundCoin ();
		collectedCoins += coin;
		GameObject.Find ("CoinsManager").GetComponent<Coins> ().addCoin();
	}
}
