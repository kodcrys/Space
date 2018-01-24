using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {

	/*
	 * 1. shockwave
	 * 2. speedforce
	 * shield
	 */
	[SerializeField]
	private GameObject[] Items;

	void Awake ()
	{
		for (int i = 0; i < Items.Length; i++) 
		{
			int checkEnableItem = PlayerPrefs.GetInt ("statusItem" + i, 0);
			if (checkEnableItem == 1)
				Items [i].SetActive (true);
		}
	}
}
