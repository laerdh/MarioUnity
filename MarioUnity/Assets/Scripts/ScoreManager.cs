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
	

	void Awake() {
		//Beholde Verdier
		audioManager = audioManager.GetComponent<AudioManager> ();
		DontDestroyOnLoad(this.gameObject);
	}

	void Update() {
		if(t < 100){
			audioManager.addPitch();
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
		t = time;


		if (time == 100 && !badTime) {
			audioManager.setHurryUp ();
			badTime = true;
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
