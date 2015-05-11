using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


	public int currentScore;
	public int currentCoin;
	public int coin = 1;
	public int score = 100;

	public Text Coin;
<<<<<<< HEAD
=======

>>>>>>> b2929c8e7cee0d0445bbd86dd72db197cf15bc9e
	
	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		//currentScore++; for å teste at score kan oppdateres på skjermen
		Coin.text = "x" + currentCoin.ToString ("00");
	}
	
	public void addCoin() {
		currentCoin++;
	}

	public void addScore(int score) {
		currentScore += score;
	}
}
