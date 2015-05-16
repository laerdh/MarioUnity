﻿using UnityEngine;
using System.Collections;

public class collectCoin : MonoBehaviour {

	int coin = 1;
	int collectedCoins = 0;
	public Score scores;


	void Start(){
		scores = GameObject.Find ("Score").GetComponent<Score> ();
	}
	// Use this for initialization
	public void addCoinUnderGround () {
		AudioManager audioManager = GameObject.Find ("AudioController").GetComponent<AudioManager> ();
		audioManager.underGroundCoin ();
		collectedCoins += coin;
		scores.AddScoreAmount (200);
		GameObject.Find ("CoinsManager").GetComponent<Coins> ().addCoin();
	}
}
