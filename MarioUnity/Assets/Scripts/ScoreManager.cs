using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	int s;
	int c;
	int t;

	//Coin
	public void currentCoin(int coin) {
		c = coin;
	}
	//Score
	public void currentScore(int Score) {
		s = Score;
	}
	//Time
	public void currentTime(int time){
		t = time;
	}
}
