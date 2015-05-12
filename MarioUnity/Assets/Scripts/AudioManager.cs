using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	AudioSource smallJump;
	AudioSource bigJump;

	void Start(){
		AudioSource[] audios = GetComponents<AudioSource> ();
		smallJump = audios [0];
		bigJump = audios [1];
	}

	public void playSmallMarioJump(){
		smallJump.Play ();
	}

	public void playBigMarioJump(){
		bigJump.Play ();
	}
}
