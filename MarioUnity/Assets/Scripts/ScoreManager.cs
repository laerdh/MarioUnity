using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


	public int currentScore;
	public int coin = 100;
	Text text;
	
	// Use this for initialization
	void Awake () {
		text = GetComponent<Text>();
		currentScore = 000000;
	}
	
	// Update is called once per frame
	void Update () {
		//currentScore++; for å teste at score kan oppdateres på skjermen
		text.text = "" + currentScore.ToString("000000");
	}
	
	public void addCoin() {
		currentScore += coin;
	}
}
