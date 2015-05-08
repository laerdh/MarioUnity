using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


	public int currentScore;
	public int coin = 100;
	Text text;
	
	// Use this for initialization
	void Awake () {
		Text text = GetComponent<Text>();
		currentScore = 000000;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Score " + currentScore;
	}
	
	public void Coin() {
		currentScore += coin;
	}
}
