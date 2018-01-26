using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUpgradeScene : MonoBehaviour {

	public static bool isTut;

	[SerializeField]
	GameObject panelTut;

	[SerializeField]
	GameObject hand;

	[SerializeField]
	GameObject [] posHand;

	[SerializeField]
	GameObject [] posHandX;

	int indexPosHand;

	[SerializeField]
	bool isSceneUpgrade;

	// Use this for initialization
	void Awake () {
		indexPosHand = 0;
		if (isSceneUpgrade) {
			if (SaveManager.instance.state.isFirstGameUpgradeScene) {
				if (MultiResolution.device == "iphonex") {
					hand.transform.position = posHandX [indexPosHand].transform.position;
				} else {
					hand.transform.position = posHand [indexPosHand].transform.position;
				}
				panelTut.SetActive (true);
				SaveManager.instance.state.isFirstGameUpgradeScene = false;
				SaveManager.instance.Save ();
			} else
				panelTut.SetActive (false);
		} else {
			if (SaveManager.instance.state.isFirstGameBitCoin) {
				if (MultiResolution.device == "iphonex") {
					hand.transform.position = posHandX [indexPosHand].transform.position;
				} else {
					hand.transform.position = posHand [indexPosHand].transform.position;
				}
				isTut = true;
				panelTut.SetActive (true);
				SaveManager.instance.state.isFirstGameBitCoin = false;
				SaveManager.instance.Save ();
			} else
				panelTut.SetActive (false);
		}
	}
	
	public void Click(){
		indexPosHand++;
		if (indexPosHand < posHand.Length) {
			hand.transform.position = posHand [indexPosHand].transform.position;
		} else {
			isTut = false;
			panelTut.SetActive (false); 
		}
	}
}
