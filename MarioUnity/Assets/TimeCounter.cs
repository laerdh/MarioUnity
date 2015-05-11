using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour {

	Text text;
	int startTime;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		startTime = 400;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "" + startTime.ToString ();
	}
}
