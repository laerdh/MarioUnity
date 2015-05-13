using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	public AudioManager audiomanager;

	void Start(){
		GameObject e = GameObject.Find ("AudioController");
		audiomanager = e.GetComponent<AudioManager> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			audiomanager.stopBackgroundMusic();
			audiomanager.finishLvl();
		}
	}
}
