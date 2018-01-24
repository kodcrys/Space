using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * p - Parameter
 * This cripts is on the GameObjects: Explosion prefabs
 */

public class DestroyEffect : MonoBehaviour {

	// 1-p The time countdown to destroy the effect
	[SerializeField]
	float countdownDestroy;

	// Use this for initialization
	void OnEnable () {
		// 1. Call the functio
		StartCoroutine (DestroyObj ());
	}

	// 1.1 After the affect end, destroy the affect.
	IEnumerator DestroyObj()
	{
		yield return new WaitForSeconds (countdownDestroy);
		transform.gameObject.SetActive (false);
		SpawnerPool.countRockets--;
	}
}
