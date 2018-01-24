using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadingSceneAnimationManager : MonoBehaviour {

	/// <summary>
	/// State ani cua cac animation.
	/// </summary>
	public enum stateAni{ none, runloadingAni};
	public stateAni anim = stateAni.none;

	[Header("Loading Text Ani of LoadScene")]
	[SerializeField]
	UiOfLoading uiLoading;

	// Use this for initialization
	void Start () {
		StartCoroutine (StateUpdate ());
	}


	// Ham thuc hien Animation Loadding
	IEnumerator StateUpdate () {
		switch (anim) {
		case stateAni.none:
			anim = stateAni.runloadingAni;
			break;
		case stateAni.runloadingAni:
			uiLoading.loadingTxt.text = uiLoading.loading0cham;
			yield return new WaitForSeconds (uiLoading.timeWait);
			uiLoading.loadingTxt.text = uiLoading.loading1cham;
			yield return new WaitForSeconds (uiLoading.timeWait);
			uiLoading.loadingTxt.text = uiLoading.loading2cham;
			yield return new WaitForSeconds (uiLoading.timeWait);
			uiLoading.loadingTxt.text = uiLoading.loading3cham;
			yield return new WaitForSeconds (uiLoading.timeWait);
			break;
		}

		StartCoroutine (StateUpdate ()); // de cho ham chay lien tuc du chi goi no chay mot lan
	}
}


/// <summary>
/// User interface of loading.
/// </summary>
[Serializable]
public class UiOfLoading{
	public UnityEngine.UI.Text loadingTxt;

	public string loading0cham;
	public string loading1cham;
	public string loading2cham;
	public string loading3cham;

	public float timeWait;
}


