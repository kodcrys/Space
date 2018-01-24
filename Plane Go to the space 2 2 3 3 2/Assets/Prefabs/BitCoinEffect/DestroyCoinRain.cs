using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCoinRain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (waitToDestroy (2.1f));
	}
	
	IEnumerator waitToDestroy(float time){
		yield return new WaitForSeconds (time);
		StaticOption.isEffectDone = false;
		Destroy (gameObject);
	}
}
