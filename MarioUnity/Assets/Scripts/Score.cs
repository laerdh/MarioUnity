using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public ScoreManager scoreManager;

	public Text text;
	public int currentScore;
	public int score = 100;
	
	// Update is called once per frame
	void Update () {
		text.text = currentScore.ToString ("000000");
		scoreManager.currentScore (currentScore);
	}

	public void AddScore () 
	{
		currentScore += score;
	}

	public void AddScoreFlag(int scoreAmount){
		currentScore += scoreAmount;
	}
}
