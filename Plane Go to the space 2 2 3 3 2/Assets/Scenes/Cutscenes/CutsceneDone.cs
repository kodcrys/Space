using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDone : MonoBehaviour {


	public void DoneCutscene(){
		gameObject.GetComponent<Animator> ().enabled = false;
		CutsceneManager.nextScene = true;
		CutsceneManager.animDone = true;
	}
}
