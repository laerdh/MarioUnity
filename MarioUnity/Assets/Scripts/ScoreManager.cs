using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public AudioManager audioManager;

	public static int s;
	public static int c;
	public static int t;
	private float pitch = 1.2f;

	private bool badTime = false;

	private bool isFinished = false;

	private Score score;

	void Awake() {
		//Beholde Verdier
		audioManager = audioManager.GetComponent<AudioManager> ();
		DontDestroyOnLoad(this.gameObject);
		score = GameObject.Find ("Score").GetComponent<Score> ();
	}

	void Update() {
		if(t < 100){
			audioManager.addPitch();
		}

		if (isFinished) {
			if(t > 0) {
				t--;
				score.AddScoreAmount(100);
				audioManager.underGroundCoin();
			}
		}
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
		if (!isFinished) {
			t = time;


			if (time == 100 && !badTime) {
				audioManager.setHurryUp ();
				badTime = true;
			}
		}
	}
	public void setComplete() {
		isFinished = true;
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
