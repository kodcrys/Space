using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDone : MonoBehaviour {

	Animator thisAnimator;

	void Start(){
		thisAnimator = gameObject.GetComponent<Animator> ();
	}

	public void OffAnimator(){
		thisAnimator.enabled = false;
	}
}
