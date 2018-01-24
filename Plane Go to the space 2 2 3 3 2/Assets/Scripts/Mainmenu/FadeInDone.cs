using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInDone : MonoBehaviour {

	public static bool isFadeInDone;

	void Start(){
		FadeInDone.isFadeInDone = false;
	}

	public void FadeisDone(){
		gameObject.GetComponent<Animator> ().enabled = false;
		gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		FadeInDone.isFadeInDone = true;
	}
}
