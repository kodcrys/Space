using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gameobject: MainmenuManager

public class FadeInOut : MonoBehaviour {

	private CanvasGroup fadeGroup;
	private float fadeSpeed = 0.8f;

	// Use this for initialization
	void Start () {
		fadeGroup = FindObjectOfType<CanvasGroup> ();
		fadeGroup.alpha = 1;
	}
	
	// Update is called once per frame
	void Update () {
		fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeSpeed;
		if (fadeGroup.alpha == 0) {
			
		}
	}
}
