using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	AudioSource smallJump;
	AudioSource bigJump;

	void Start(){
		AudioSource[] audios = GetComponents<AudioSource> ();
		smallJump = audios [0];
		bigJump = audios [1];
	}

	void playSmallMarioJump(){
		smallJump.Play ();
	}

	void playBigMarioJump(){
		bigJump.Play ();
	}
}
