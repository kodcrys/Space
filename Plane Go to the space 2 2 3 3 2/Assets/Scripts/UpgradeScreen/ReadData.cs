using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour {

	[SerializeField]
	GameObject dataIphone, dataIpad;

	// Use this for initialization
	void Awake () {
		if (MultiResolution.device == "ipad") {
			dataIpad.SetActive (true);
			dataIphone.SetActive (false);
		} else {
			dataIpad.SetActive (false);
			dataIphone.SetActive (true);
		}
	}
}
