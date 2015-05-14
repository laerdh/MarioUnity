using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int s;
	public static int c;
	public static int t;

	void Awake() {
		//Beholde Verdier
		DontDestroyOnLoad(this.gameObject);
	}
	//Coin
	public void currentCoin(int coin) {
		c = coin;
	}
	//Score
	public void currentScore(int score) {
		s = score;
	}
	//Time
	public void currentTime(int time){
		t = time;
	}
	public int returnCoin(){
		return c;
	}
	public int returnScore(){
		return s;
	}
	public int returnTime(){
		return t;
	}
}
