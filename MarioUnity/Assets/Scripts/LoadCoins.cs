using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadCoins : MonoBehaviour {
	
	public Text text;
	public ScoreManager scoreManager;
	int currentCoin;

	// Use this for initialization
	void Start () {
		GameObject e = GameObject.Find ("SM");
		scoreManager = e.GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		currentCoin = scoreManager.returnCoin ();
		if (currentCoin < 10) {
			text.text = "X0" + currentCoin.ToString ();
		} else {
			text.text = "X" + currentCoin.ToString ();
		}
		scoreManager.currentCoin (currentCoin);
	}
}
