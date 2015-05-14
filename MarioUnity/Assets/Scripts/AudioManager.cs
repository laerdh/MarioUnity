using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	AudioSource smallJump;
	AudioSource bigJump;
	AudioSource brickSmash;
	AudioSource marioDie;
	AudioSource backgroundMusic;
	AudioSource aSindre;
	AudioSource UnderGroundCoin;

	void Start(){
		AudioSource[] audios = GetComponents<AudioSource> ();
		smallJump = audios [0];
		bigJump = audios [1];
		brickSmash = audios [2];
		marioDie = audios [3];
		backgroundMusic = audios [4];
		aSindre = audios [5];
		UnderGroundCoin = audios [6];

		startBackgroundMusic ();
	}

	public void playSmallMarioJump(){
		smallJump.Play ();
	}

	public void playBigMarioJump(){
		bigJump.Play ();
	}

	public void breakBlocks(){
		brickSmash.Play ();
	}

	public void playMarioDie(){
		marioDie.Play ();
	}

	public void startBackgroundMusic() {
		backgroundMusic.Play ();
	}

	public void stopBackgroundMusic() {
		backgroundMusic.Stop ();
	}
	public void finishLvl(){
		aSindre.Play ();
	}

	public void underGroundCoin(){
		UnderGroundCoin.Play ();
	}
}
