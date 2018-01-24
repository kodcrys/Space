using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBG : MonoBehaviour {

	[SerializeField]
	GameObject iphoneBG, iphonexBG, ipadBG;

	// Use this for initialization
	void Start () {
		ChangeBGScreen ();
	}

	public void ChangeBGScreen(){
		if (MultiResolution.device == "iphone") {
			iphoneBG.SetActive (true);
			iphonexBG.SetActive (false);
			ipadBG.SetActive (false);
		}
		if (MultiResolution.device == "ipad") {
			iphoneBG.SetActive (false);
			iphonexBG.SetActive (false);
			ipadBG.SetActive (true);
		}
		if (MultiResolution.device == "iphonex") {
			iphoneBG.SetActive (false);
			iphonexBG.SetActive (true);
			ipadBG.SetActive (false);
		}
	}
}
