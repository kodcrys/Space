using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutDone : MonoBehaviour {

	public static bool isFadeOutDone;

	void Start(){
		FadeOutDone.isFadeOutDone = false;
	}

	public void FadeisDone(){
		gameObject.GetComponent<Animator> ().enabled = false;
		FadeOutDone.isFadeOutDone = true;
		UIAnimation.isFadeBegin = false;
	}
}
