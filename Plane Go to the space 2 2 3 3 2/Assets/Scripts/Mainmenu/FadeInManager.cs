using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInManager : MonoBehaviour {

	public static FadeInManager instance;

	public Animator fadeIn, fadeOut;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		}
		fadeIn.enabled = true;
	}

	public void OpenIAPMenu(GameObject iapMenu){
		iapMenu.SetActive (true);
	}
}
