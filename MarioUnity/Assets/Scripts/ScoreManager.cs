using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


	public int currentScore;
	public int currentCoin;
	public int coin = 1;
	public int score = 100;

	public GUIText Coin;
	
	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		//currentScore++; for å teste at score kan oppdateres på skjermen
		Coin.text = "" + currentCoin.ToString ("00");
	}
	
	public void addCoin() {
		currentCoin++;
	}

	public void addScore(int score) {
		currentScore += score;
	}
}
