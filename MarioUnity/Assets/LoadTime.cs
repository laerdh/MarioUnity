using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadTime : MonoBehaviour {
	
	public ScoreManager scoreManager;
	public Text text;
	public int currentTime;

	// Use this for initialization
	void Start () {
		GameObject e = GameObject.Find ("SM");
		scoreManager = e.GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		currentTime = scoreManager.returnTime();
		text.text = currentTime.ToString();
		scoreManager.currentTime (currentTime);
	}
}
