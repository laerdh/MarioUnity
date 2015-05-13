using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int currentScore;
	public int currentCoin = 0;
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
	//Coin
	public void addCoin() {
		currentCoin++;
	}
<<<<<<< HEAD
	
=======
	//Score
	public void addScore() {
		currentScore += score;
	}
	//Time
	public void currentTime(int time){

	}
>>>>>>> 3a590827ac177e23f92f61e32569c64c82fbded2
}
