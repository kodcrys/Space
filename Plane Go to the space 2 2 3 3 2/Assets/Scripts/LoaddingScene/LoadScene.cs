using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public static bool showAds;
	public static int count;
	public static int countDead;

	private CanvasGroup fadeGroup;
	private float loadTime;
	private float LogoTime = 4.0f;

	public string leaderboardIdEndless;
	public string leaderboardIdLevel;

	public static string leaderboardIdEndlessStr;
	public static string leaderboardIdLevelStr;

	void Awake(){

		leaderboardIdEndlessStr = leaderboardIdEndless;
		leaderboardIdLevelStr = leaderboardIdLevel;

		count = 0;
		countDead = 0;
		showAds = false;

		GGAmob.Intance.RequestBanner ();
		GGAmob.Intance.RequestInterstitial ();
		GGAmob.Intance.RequestRewardBasedVideo ();

		if (SaveManager.instance.state.fadeInOut == true) {
			BackFadeFirstTime ();
		}
	}

	// Use this for initialization
	void Start () {
		fadeGroup = FindObjectOfType<CanvasGroup> ();
		fadeGroup.alpha = 1;

		if (Time.time < LogoTime) {
			loadTime = LogoTime;
		} else {
			loadTime = Time.time;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time < LogoTime) {
			fadeGroup.alpha = 1 - Time.time;
		}

		if (Time.time > LogoTime && loadTime != 0) {
			fadeGroup.alpha = Time.time - LogoTime;
			if (fadeGroup.alpha >= 1) {
				Debug.Log ("isPurchaseRemoeAds: " + SaveManager.instance.state.isPurchaseRemoveAds);
				if (SaveManager.instance.state.isPurchaseRemoveAds == false)
					GGAmob.Intance.ShowBanner ();
				else
					GGAmob.Intance.HideBanner ();
				SceneManager.LoadScene(1);
			}
		}
	}
		
	/// khoi phuc lai fade khi vao lai game 
	void BackFadeFirstTime(){
		SaveManager.instance.state.fadeInOut = false;
		SaveManager.instance.Save ();
	}
}
