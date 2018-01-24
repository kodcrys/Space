using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBuyAnim : MonoBehaviour {

	public static bool canBuyItem;

	[Header("Iphone")]
	[SerializeField]
	List<Animator> canBuyAnimIphone = new List<Animator> ();

	[Header("IphoneX")]
	[SerializeField]
	List<Animator> canBuyAnimIphoneX = new List<Animator> ();

	[Header("IphoneIpad")]
	[SerializeField]
	List<Animator> canBuyAnimIpad = new List<Animator> ();


	// Update is called once per frame
	void Update () {
		if (MultiResolution.device == "iphone") {
			if (canBuyItem == true) {
				for (int i = 0; i < canBuyAnimIphone.Count; i++) {
					canBuyAnimIphone [i].enabled = true;
				}
			} else {
				for (int i = 0; i < canBuyAnimIphone.Count; i++) {
					canBuyAnimIphone [i].enabled = false;
				}
			}
		}
		if (MultiResolution.device == "ipad") {
			if (canBuyItem == true) {
				for (int i = 0; i < canBuyAnimIpad.Count; i++) {
					canBuyAnimIpad [i].enabled = true;
				}
			} else {
				for (int i = 0; i < canBuyAnimIpad.Count; i++) {
					canBuyAnimIpad [i].enabled = false;
				}
			}
		}
		if (MultiResolution.device == "iphonex") {
			if (canBuyItem == true) {
				for (int i = 0; i < canBuyAnimIphoneX.Count; i++) {
					canBuyAnimIphoneX [i].enabled = true;
				}
			} else {
				for (int i = 0; i < canBuyAnimIphoneX.Count; i++) {
					canBuyAnimIphoneX [i].enabled = false;
				}
			}
		}
	}
}
