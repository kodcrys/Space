using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneData : MonoBehaviour {

	public static int index, speedchoosePlane, rotationchoosePlane, fuelchoosePlane;
	[SerializeField]
	private GameObject[] Planes;
	void Awake ()
	{
		fuelchoosePlane = PlayerPrefs.GetInt("curFuel"+index, 0);
		speedchoosePlane = PlayerPrefs.GetInt("curSpeed"+index, 0);
		rotationchoosePlane = PlayerPrefs.GetInt("curFlexibility"+index, 0);
		Planes [index].SetActive (true);
	}
}
