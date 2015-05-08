using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {

	public GUIText scoreText;
	public int score;


	// Use this for initialization
	void UpdateScore () {
		scoreText.text = "Score " + score;
	}
}
