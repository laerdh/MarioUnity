using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

	public ScoreManager scoreManager;

	public Text text;
	public int currentCoin = 0;
	public int coin = 0;

	void Update () {
		text.text = "x" + currentCoin.ToString("00");
		scoreManager.currentCoin (currentCoin);
	}

	public void addCoin() {
		currentCoin++;
	}
}
