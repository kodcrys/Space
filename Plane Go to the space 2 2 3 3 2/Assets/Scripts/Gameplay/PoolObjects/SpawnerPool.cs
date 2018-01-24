using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * p - Parameter
 * This cripts is on the GameObjects: Spawner - Left, right, Up.
 */

public class SpawnerPool : MonoBehaviour {

	// 1-p Div number you want to div with the postion of the plane. 
	[SerializeField]
	private int Div, MaxRockets, MaxClouds;
	[SerializeField]
	private GameObject[] SpawnerPos;
	// Check Endless Mode
	public bool checkEndless;

	public static bool endless;
	public static int countRockets;
	int maxRocketsAvailable = 8;

	void Awake ()
	{
		endless = checkEndless;
	}

	float time;
	// Use this for initialization
	void OnEnable () {
		countRockets = 0;
		StartCoroutine (Countdown());
		// 1. Loop unlimit and call the spawner ball function
		StartCoroutine (Spawner());
	}
	
	// 1.1 Create a new rocket.
	IEnumerator Spawner()
	{
		// Time create the rockets will depend on position of the plane.
		float addRockets = (transform.position.y / Div);
		maxRocketsAvailable += Mathf.RoundToInt (addRockets);
		// If the time create the rockets are too small, just force it create rocket every second.
			yield return new WaitForSeconds (2f);

		if (countRockets <= maxRocketsAvailable)
		// Create a random rocket with random position y
			{
			if (transform.position.y < 800f) 
			{
				int RandPos = Random.Range (0, SpawnerPos.Length - 2);
				countRockets++;
				int randRocket = Random.Range (0, MaxRockets - 1);
				Vector3 tempRocket = new Vector3 (0, Random.Range(-10, -12), 0);
				PoolManager.Intance.lstPool [randRocket].getindex ();
				PoolManager.Intance.lstPool [randRocket].GetPoolObject ().transform.position = SpawnerPos [RandPos].transform.position + tempRocket;
				PoolManager.Intance.lstPool [randRocket].GetPoolObject ().SetActive (true);
			} 
			else 
			{
				for (int i = 0; i < 2; i++) 
				{
					countRockets++;
					int randRocket = Random.Range (0, MaxRockets - 1);
					Vector3 tempRocket = new Vector3 (0, Random.Range (-10, -12), 0);
					PoolManager.Intance.lstPool [randRocket].getindex ();
					PoolManager.Intance.lstPool [randRocket].GetPoolObject ().transform.position = SpawnerPos [i].transform.position + tempRocket;
					PoolManager.Intance.lstPool [randRocket].GetPoolObject ().SetActive (true);
				}
			}
			}	
		if (!checkEndless) 
		{
			// Random the position for the new cloud
			int randCloud = Random.Range (0, MaxClouds - 1);
			// Because the cloud behind the rocket on the Pool Manager, so you will get the cloud object right if you add random + maxrockets 
			PoolManager.Intance.lstPool [MaxRockets + 1 + randCloud].getindex ();
			Vector3 tempCloud = new Vector3 (0, Random.Range (-1, 7), 0);

			// The clouds will always fly from the right to left
			int randPos = Random.Range (0, SpawnerPos.Length - 2);
			if (randPos == 0 && transform.position.y <= 300f) 
			{
				PoolManager.Intance.lstPool [MaxRockets + 1 + randCloud].GetPoolObject ().transform.position = SpawnerPos [randPos].transform.position + tempCloud;
				PoolManager.Intance.lstPool [MaxRockets + 1 + randCloud].GetPoolObject ().SetActive (true);
			}
		}
			// Repeat the action create rocket
		StartCoroutine (Spawner ());
	}

	IEnumerator Countdown()
	{
		if (transform.position.y <= 800) 
		{
			yield return new WaitForSeconds (3f);

			var randItem = Random.Range (10, 13);

			PoolManager.Intance.lstPool [randItem].getindex ();
			Vector3 tempItem = new Vector3 (Random.Range (-5, 5), 0, 0);
			PoolManager.Intance.lstPool [randItem].GetPoolObject ().GetComponent <Rigidbody2D> ().gravityScale = Random.Range (0.1f, 0.2f);
			PoolManager.Intance.lstPool [randItem].GetPoolObject ().transform.position = SpawnerPos [SpawnerPos.Length - 2].transform.position + tempItem;
			PoolManager.Intance.lstPool [randItem].GetPoolObject ().SetActive (true);


			StartCoroutine (Countdown ());
		} 
		else 
		{
			yield return new WaitForSeconds (3f);

			for (int i = 0; i < 2; i++) 
			{
				var randItem = Random.Range (10, 14);
				if (randItem == 13)
					randItem = 10;
				PoolManager.Intance.lstPool [randItem].getindex ();
				Vector3 tempItem = new Vector3 (Random.Range (-5, 5), 0, 0);
				PoolManager.Intance.lstPool [randItem].GetPoolObject ().GetComponent <Rigidbody2D> ().gravityScale = Random.Range (0.05f, 0.4f);
				PoolManager.Intance.lstPool [randItem].GetPoolObject ().transform.position = SpawnerPos [SpawnerPos.Length - 2].transform.position + tempItem;
				PoolManager.Intance.lstPool [randItem].GetPoolObject ().SetActive (true);
			}
			StartCoroutine (Countdown ());
		}
	}
}
