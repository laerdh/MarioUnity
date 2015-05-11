using UnityEngine;
using System.Collections;

public class loadnewlvl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("nextScene", 3);
	}

	void nextScene(){
		Application.LoadLevel(2);
	}
}
