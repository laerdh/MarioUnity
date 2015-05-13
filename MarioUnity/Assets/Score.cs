using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public Text text;
	public int currentScore;
	public int score = 100;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		text.text = currentScore.ToString ("000000");
	}

	public void AddScore () 
	{
		currentScore += score;
	}
}
