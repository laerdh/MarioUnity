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
	AudioSource MarioHurryUp;
	AudioSource Kick;

	private bool startedMusicAfterBadTime = false;

	void Start(){
		DontDestroyOnLoad (this.gameObject);

		AudioSource[] audios = GetComponents<AudioSource> ();
		smallJump = audios [0];
		bigJump = audios [1];
		brickSmash = audios [2];
		marioDie = audios [3];
		backgroundMusic = audios [4];
		UnderGroundCoin = audios [5];
		Pipe = audios [6];
		UnderGroundLvl = audios [7];
		EmptyBlock = audios [8];
		PwrUpAppears = audios [9];
		MarioHitsPwrUp = audios [10];
		MarioHurryUp = audios [11];
		Kick = audios [12];


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
		if(backgroundMusic != null)
			backgroundMusic.Play ();
	}

	public void stopBackgroundMusic() {
		backgroundMusic.Stop ();
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

	public void KickPlay() {
		Kick.Play ();
	}

	public void setHurryUp() {
		stopBackgroundMusic ();
		MarioHurryUp.Play ();
	}
	public void addPitch(){
		if(!MarioHurryUp.isPlaying) {
			if(!startedMusicAfterBadTime) {
				startedMusicAfterBadTime = true;
				backgroundMusic.pitch = 1.3f;
				startBackgroundMusic();
			}
		}
	}
}
