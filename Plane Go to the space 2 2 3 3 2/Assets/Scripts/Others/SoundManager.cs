using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	[Header("AudioClip")]
	[SerializeField]
	private AudioClip clickBtn;
	[SerializeField]
	private AudioClip clickScreenBC;
	[SerializeField]
	private AudioClip clickSuccesBuyItem;
	[SerializeField]
	private AudioClip clickFailBuyItem;
	[SerializeField]
	private AudioClip eatItem;
	[SerializeField]
	private AudioClip exp;
	[SerializeField]
	private AudioClip loseGame;
	[SerializeField]
	private AudioClip winGame;
	[SerializeField]
	private AudioClip dropCoin;

	[Header("AudioSource")]
	public AudioSource BGCutScene1Sound;
	public AudioSource BGCutScene2Sound;
	public AudioSource OthersSound;
	public AudioSource SpaceshipSound;
	public AudioSource GamePlaySound;

	public static SoundManager Intance;

	void Awake(){
		if (Intance == null)
			Intance = this;
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}

	public void MuteSound(){
		if (BGCutScene1Sound != null)
			BGCutScene1Sound.mute = true;
		if (OthersSound != null)
			OthersSound.mute = true;
		if (SpaceshipSound != null)
			SpaceshipSound.mute = true;
	}

	public void PlaySound(){
		if (BGCutScene1Sound != null)
			BGCutScene1Sound.mute = false;
		if (OthersSound != null)
			OthersSound.mute = false;
		if (SpaceshipSound != null)
			SpaceshipSound.mute = false;
	}

	public void PlayClickBtn(){
		if(clickBtn != null)
			OthersSound.PlayOneShot (clickBtn);
	}

	public void PlayClickScreenBC(){
		if(clickScreenBC != null)
			OthersSound.PlayOneShot (clickScreenBC);
	}

	public void PlayClickSuccessBuyItem(){
		if(clickSuccesBuyItem != null)
			OthersSound.PlayOneShot (clickSuccesBuyItem);
	}

	public void PlayClickFailBuyItem(){
		if(clickFailBuyItem != null)
			OthersSound.PlayOneShot (clickFailBuyItem);
	}

	public void PlayEatItem(){
		if(eatItem != null)
			OthersSound.PlayOneShot (eatItem);
	}

	public void PlayExpSound(){
		if(exp != null)
			OthersSound.PlayOneShot (exp);
	}	

	public void PlayWinGame(){
		if (winGame != null)
			OthersSound.PlayOneShot (winGame);
	}

	public void PlayLoseGame(){
		if (loseGame != null)
			OthersSound.PlayOneShot (loseGame);
	}

	public void PlayCoinDrop(){
		if (dropCoin != null)
			OthersSound.PlayOneShot (dropCoin);
	}

	public void PlaySoundCutScene1BG(){
		if (BGCutScene1Sound != null)
			BGCutScene1Sound.Play ();
	}

	public void PlaySoundCutScene2BG(){
		if (BGCutScene2Sound != null)
			BGCutScene2Sound.Play ();
	}

	public void MuteSoundCutScene1BG(){
		if (BGCutScene1Sound != null)
			BGCutScene1Sound.Stop ();
	}

	public void MuteSoundCutScene2BG(){
		if (BGCutScene2Sound != null)
			BGCutScene2Sound.Stop ();
	}

	public void PlaySpaceship(){
		if (SpaceshipSound != null)
			SpaceshipSound.Play ();
	}

	public void StopSpaceship(){
		if (SpaceshipSound != null)
			SpaceshipSound.Stop ();
	}

	public void PlaySoundGamePlay(){
		if (GamePlaySound != null)
			GamePlaySound.Play ();
	}

	public void StopSoundGamePlay(){
		if (GamePlaySound != null)
			GamePlaySound.Stop ();
	}
}
