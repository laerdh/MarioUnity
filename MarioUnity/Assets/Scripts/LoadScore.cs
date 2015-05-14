using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadScore : MonoBehaviour {
	
	public Text text;
	public ScoreManager scoreManager;
	int currentScore;


	// Use this for initialization
	void Start () {
		GameObject e = GameObject.Find ("SM");
		scoreManager = e.GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		currentScore = scoreManager.returnScore ();
		text.text = currentScore.ToString("000000");
		scoreManager.currentScore (currentScore);
	}
}
