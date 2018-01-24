using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gameobject: MainmenuManager

public class MainMenuManager : MonoBehaviour {

	private CanvasGroup fadeGroup;
	private float loadTime;
	private float fadeSpeed = 1f;

	// Use this for initialization
	void Start () {
		fadeGroup = FindObjectOfType<CanvasGroup> ();
		if (SaveManager.instance.state.fadeInOut == false) {
			fadeGroup.alpha = 1;
		} else {
			fadeGroup.alpha = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (SaveManager.instance.state.fadeInOut == false) {
			fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeSpeed;
			if (fadeGroup.alpha == 0) {
				fadeGroup.gameObject.SetActive (false);
				FadePlayOneShot ();
				if (LoadScene.showAds == false) {
					// Quang Cao
					LoadScene.showAds = true;
				}
			}
		} else {
			fadeGroup.gameObject.SetActive (false);
		}
	}
		
	// Save lai dieu kien de fade chi chay dc mot lan khi vao game
	void FadePlayOneShot(){
		SaveManager.instance.state.fadeInOut = true;
		SaveManager.instance.Save ();
	}
}
