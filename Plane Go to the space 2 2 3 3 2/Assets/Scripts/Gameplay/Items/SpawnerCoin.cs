using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerCoin : MonoBehaviour {

	[SerializeField]
	private int PoolBitcoin;
	[SerializeField]
	private GameObject UpSpawner;

	float time;
	// Use this for initialization
	void OnEnable () {
		time = 3f;
		StartCoroutine (Countdown());
		// 1. Loop unlimit and call the spawner ball function
		StartCoroutine (Spawner());
	}

	// 1.1 Create a new coin.
	IEnumerator Spawner()
	{
		yield return new WaitForSeconds (Random.Range (1f, 3f));
		int rand = Random.Range (0, 2);
		if (rand == 0) 
		{
			PoolManager.Intance.lstPool [PoolBitcoin].getindex ();
			Vector3 tempItem = new Vector3 (Random.Range (-5f, 5f), 0, 0);
			PoolManager.Intance.lstPool [PoolBitcoin].GetPoolObject ().transform.position = UpSpawner.transform.position + tempItem;
			PoolManager.Intance.lstPool [PoolBitcoin].GetPoolObject ().SetActive (true);
		}
		StartCoroutine (Spawner ());
	}

	IEnumerator Countdown()
	{
		yield return new WaitForSeconds (1f);
		time--;
		if (time>=0)
			StartCoroutine (Countdown());
	}
}
