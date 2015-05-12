using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


	public int currentScore;
	public int currentCoin = 10;
	public int coin = 0;
	public int score = 100;

	Text text;

	
	// Use this for initialization
	void Awake () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		//addCoin ();
		//currentScore++; for å teste at score kan oppdateres på skjermen
		text.text = "x" + currentCoin.ToString("00");
	}
	
	public void addCoin() {
		Debug.Log (currentCoin);
		currentCoin++;
	}

	public void addScore() {
		currentScore += score;
	}
}
