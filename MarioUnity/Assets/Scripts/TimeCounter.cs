using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour {

	Text text;
	int startTime;
	bool timeUp = false;
	int decrese = 1;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		startTime = 400;
		decreseTime ();
	}

	public void decreseTime(){
		StartCoroutine("waitTime");
	}

	IEnumerator waitTime(){
		timeUp = true;
		while (timeUp) {
			yield return new WaitForSeconds(1);
			startTime -= decrese;
			Debug.Log (startTime);
			if (startTime == 0) {
				timeUp = false;
				Debug.Log ("TimesUp!");
			}
			text.text = "" + startTime;
		}
	}
}
