using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * p - Parameter
 * This cripts is on the GameObjects: Rocket2 Prefabs, Rocket3 Prefabs
 */

public class CountdownDestroy : MonoBehaviour {
	// 1-p Countdown the time to destroy the gameobject, when the count >= runAni, the rocket will hide and show.
	[SerializeField]
	int countDestroy, runAni;
	// 1-p Time to delay the hide and show animation.
	[SerializeField]
	float delay;
	[SerializeField] 
	int numberPrefab;
	// 1-p count the time run the function HideandShow
	int count = 0;

	// Update is called once per frame
	void Start () {
		
	}
		
	void OnEnable ()
	{
		transform.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		count = 0;
		// 1. Run the countdown here
		StartCoroutine (HideandShow ());
	}

	/// 1.1 
	/// <summary>
	/// Hide and show animation.
	/// </summary>
	IEnumerator HideandShow()
	{
		count++;
		// The animation will run in here if the count > runAni
		if (count > runAni)
		{
			if (count % 2 == 0)
				transform.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.3f);
			else
				transform.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		}
		yield return new WaitForSeconds (delay);

		// If the count still < countDestroy, the function still run or the rocket will be destroyed.
		if (count <= countDestroy)
			StartCoroutine (HideandShow ());
		else 
			if (numberPrefab >= 0)
				DestroyRocket ();
			else
				transform.gameObject.SetActive (false);
	}
		
	/// 1.2
	/// <summary>
	/// Destroy the rocket.
	/// </summary>
	void DestroyRocket()
	{
		// Set active for the affect here.
			PoolManager.Intance.lstPool [numberPrefab].getindex ();
			PoolManager.Intance.lstPool [numberPrefab].GetPoolObject ().transform.position = transform.position;
			PoolManager.Intance.lstPool [numberPrefab].GetPoolObject ().transform.rotation = transform.rotation;
			PoolManager.Intance.lstPool [numberPrefab].GetPoolObject ().SetActive (true);

		// Hide the rocket on the screen.
		transform.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D ()
	{
	}
}
