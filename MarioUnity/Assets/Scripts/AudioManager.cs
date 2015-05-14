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
	AudioSource Pipe;
	AudioSource UnderGroundLvl;
	AudioSource EmptyBlock;
	AudioSource PwrUpAppears;
	AudioSource MarioHitsPwrUp;

	void Start(){
		DontDestroyOnLoad (this.gameObject);

		AudioSource[] audios = GetComponents<AudioSource> ();
		smallJump = audios [0];
		bigJump = audios [1];
		brickSmash = audios [2];
		marioDie = audios [3];
		backgroundMusic = audios [4];
		aSindre = audios [5];
		UnderGroundCoin = audios [6];
		Pipe = audios [7];
		UnderGroundLvl = audios [8];
		EmptyBlock = audios [9];
		PwrUpAppears = audios [10];
		MarioHitsPwrUp = audios [11];

		//startBackgroundMusic ();
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
		UnderGroundCoin.Play();
	}

	public void playPipe(){
		Pipe.Play ();
	}

	public void UnderGroundMusic() {
		UnderGroundLvl.Play ();
	}
	public void stopUnderGroundMusic(){
		UnderGroundLvl.Stop ();
	}
	public void EmptyBlockSound(){
		EmptyBlock.Play ();
	}
	public void pwrUpSpawn(){
		PwrUpAppears.Play ();
	}

	public void MarioPwrUp() {
		MarioHitsPwrUp.Play ();
	}
}
