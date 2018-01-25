using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDone : MonoBehaviour {

	public static bool doneCutScene;

	public void DoneCutscene(){
		gameObject.GetComponent<Animator> ().enabled = false;
		doneCutScene = true;
	}
}
