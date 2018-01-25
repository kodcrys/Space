using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

// Gameobject: GameManager

public class GameManager : MonoBehaviour {

	[Header("Option of GameManager")]
	[SerializeField]
	GameOption opGame;

	void Awake(){
		opGame.pressSkip = false;
	}

	void Start(){
		opGame.gameDone = false;
		opGame.chooseWhat = 0;
		opGame.totalTime = 15f;
		StaticOption.isEffectDone = false;
		StaticOption.limitTimeBonus = 5;
		StaticOption.isAdsSaw = false;
		StaticOption.isiAp = false;
		opGame.fadeIn.enabled = true;
		opGame.continuePanel.SetActive (false);
		StaticOption.isVideoReward = false;
		opGame.isX2Time = false;
		opGame.X2ButtonAnim.image.fillAmount = 1f;
		opGame.timeAdsX2Colddown = SaveManager.instance.state.timeColdDown;
	}

	void Update(){
		ParameterUpdate ();
		X2Active ();
		SkipActive ();
		SkipNextScene ();
		ButtonCheckInternet ();
		if (opGame.chooseWhat == 1) {
			CancelContinueActive ();
		} else if (opGame.chooseWhat == 2) {
			OkContinueActive ();
		}
	}

	void ButtonCheckInternet(){
		if (SaveManager.instance.state.haveInternet == true) {
			opGame.X2ButtonAnim.interactable = true;
		} else {
			opGame.X2ButtonAnim.interactable = false;
		}
	}

	public void BackScene(string nameSceneBack){
		SoundManager.Intance.PlayClickBtn ();
		UIManager.Instance.BackHome (nameSceneBack);
	}

	public void OpenIAPMenu(GameObject iapMenu){
		StaticOption.isiAp = true;
		iapMenu.SetActive (true);
	}

	/// <summary>
	/// Touchs down.
	/// </summary>
	public void TouchDown(){
		//neu so coin nho hon thi storage thi coin se tang len
		if (SaveManager.instance.state.coin < SaveManager.instance.state.storage) {
			SaveManager.instance.state.coin += SaveManager.instance.state.coinClick * opGame.x2Score;
		} else {
			// Khien tien luon luon bang hoac nho hon dung luong tien
			SaveManager.instance.state.coin = SaveManager.instance.state.storage;
		}
		SaveManager.instance.Save ();

		if (MultiResolution.device == "iphone") {
			for (int i = 0; i < opGame.coinSafe.Length; i++) {
				opGame.coinSafe [i].enabled = true;
			}
		} else if (MultiResolution.device == "iphonex") {
			for (int i = 0; i < opGame.coinSafeX.Length; i++) {
				opGame.coinSafeX [i].enabled = true;
			}
		} else {
			for (int i = 0; i < opGame.coinSafeIpad.Length; i++) {
				opGame.coinSafeIpad [i].enabled = true;
			}
		}

		SoundManager.Intance.PlayCoinDrop ();

		if (StaticOption.isEffectDone == false) {
			Instantiate (opGame.coinRain);
			StaticOption.isEffectDone = true;
		}
	}

	/// <summary>
	/// ham quy doi thoi gian tu tong so giay ra phut : giay.
	/// </summary>
	/// <param name="totalSeconds">Total seconds.</param>
	void UpdateLevelTimer(float totalSeconds)
	{
		int minutes = Mathf.FloorToInt(totalSeconds / 60f);
		int seconds = Mathf.RoundToInt(totalSeconds % 60f);

//		string formatedSeconds = seconds.ToString();

		if (seconds == 60)
		{
			seconds = 0;
			minutes += 1;
		}

		opGame.countDownText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
		opGame.countDownTextX.text = minutes.ToString("00") + ":" + seconds.ToString("00");
		opGame.countDownTextIpad.text = minutes.ToString("00") + ":" + seconds.ToString("00");
	}

	// Ham chay cap nhat cho cac thong so
	void ParameterUpdate(){
		//Countdown Time
		if (FadeInDone.isFadeInDone) {
			if (!StaticOption.isiAp) {
				if (!opGame.gameDone) {
					if (!TutorialUpgradeScene.isTut) {
						if (!StaticOption.isVideoReward) {
							if (!opGame.pressSkip) {
								CountdownTime ();
							}
						}
					}
				}
			}
		}

		// Time coin
		TimeCoin();

		// Change coin text
		opGame.coinText.text = AbbrevationUtility.FormatNumber(SaveManager.instance.state.coin) + " / " + AbbrevationUtility.FormatNumber(SaveManager.instance.state.storage);
		opGame.coinTextX.text = AbbrevationUtility.FormatNumber(SaveManager.instance.state.coin) + " / " + AbbrevationUtility.FormatNumber(SaveManager.instance.state.storage);
		opGame.coinTextIpad.text = AbbrevationUtility.FormatNumber(SaveManager.instance.state.coin) + " / " + AbbrevationUtility.FormatNumber(SaveManager.instance.state.storage);

		// Change time coin text
		opGame.timeCoin.text = SaveManager.instance.state.timeCoin + " / s";
		opGame.timeCoinX.text = SaveManager.instance.state.timeCoin + " / s";
		opGame.timeCoinIpad.text = SaveManager.instance.state.timeCoin + " / s";

		// Change all money
		opGame.moneyText.text = AbbrevationUtility.FormatNumber(SaveManager.instance.state.money);
		opGame.moneyTextX.text = AbbrevationUtility.FormatNumber(SaveManager.instance.state.money);
		opGame.moneyTextIpad.text = AbbrevationUtility.FormatNumber(SaveManager.instance.state.money);

		opGame.limitBonus.text = StaticOption.limitTimeBonus + " / 5";
		opGame.limitBonusX.text = StaticOption.limitTimeBonus + " / 5";
		opGame.limitBonusIpad.text = StaticOption.limitTimeBonus + " / 5";

		opGame.timer += Time.deltaTime;
	}

	void CountdownTime(){
		if (opGame.totalTime <= 0) {
			opGame.gameDone = true;
			opGame.continuePanel.SetActive (true);
			opGame.textCoinConinute.text = AbbrevationUtility.FormatNumber (SaveManager.instance.state.coin);
			opGame.totalTime = 0;
			UpdateLevelTimer (opGame.totalTime);
		} else {
			if (SaveManager.instance.state.isBonusTime == false) {
				opGame.totalTime -= Time.deltaTime;
			} else {
				opGame.totalTime += SaveManager.instance.state.bonusTime;
				SaveManager.instance.state.isBonusTime = false;
				SaveManager.instance.Save ();
			}
			UpdateLevelTimer (opGame.totalTime);
		}
	}

	public void CanelButton(){
		opGame.chooseWhat = 1;
		opGame.fadeOut.enabled = true;
		opGame.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		SaveMoney (SaveManager.instance.state.coin);
		ResetCoin ();
	}

	public void OkButton(){
		opGame.chooseWhat = 2;
		opGame.fadeOut.enabled = true;
		opGame.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		SaveMoney (SaveManager.instance.state.coin);
		ResetCoin ();
	}

	void CancelContinueActive(){
		if (opGame.gameDone) {
			if (FadeOutDone.isFadeOutDone == true) {
				//SceneManager.LoadScene (opGame.nameScene);
				int showCutScene = PlayerPrefs.GetInt ("isOnCutScene", 0);

				if (showCutScene == 1)
					SceneManager.LoadScene (opGame.nameScene);
				else
					UnityEngine.SceneManagement.SceneManager.LoadScene ("CutScene1");

				CutsceneManager.whatScene = 2;
				FadeOutDone.isFadeOutDone = false;
			}
		}
	}

	void OkContinueActive(){
		if (opGame.gameDone) {
			if (FadeOutDone.isFadeOutDone == true) {
				SceneManager.LoadScene ("Bitcoin");
				FadeOutDone.isFadeOutDone = false;
			}
		}
	}

	void TimeCoin(){
		if (!opGame.gameDone) {
			if (opGame.timer >= opGame.timeInter) {
				if (SaveManager.instance.state.coin < SaveManager.instance.state.storage) {
					SaveManager.instance.state.coin += SaveManager.instance.state.timeCoin;
				}
				opGame.timer = 0;
			}
			SaveManager.instance.Save ();
		}
	}

	// Event Skip button
	public void SkipButton(){
		opGame.pressSkip = true;
		int ranMoney;
		if (SaveManager.instance.state.storage <= 100) {
			ranMoney = 10;
		} else if (SaveManager.instance.state.storage > 100 && SaveManager.instance.state.storage <= 3200) {
			ranMoney = 200;
		} else {
			ranMoney = 500;
		}
		SaveMoney (ranMoney);
		ResetCoin ();
		SoundManager.Intance.PlayClickBtn ();
		CutsceneManager.whatScene = 2;
		if (SaveManager.instance.state.haveInternet == true) {
			if (SaveManager.instance.state.isPurchaseRemoveAds == false) {
				LoadScene.count++;
				if (LoadScene.count >= 3) {
					GGAmob.Intance.ShowInterstitial ();
					GGAmob.Intance.RequestInterstitial ();
					LoadScene.count = 0;		
				} else {
					opGame.fadeOut.enabled = true;
					opGame.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
				}
			} else {
				opGame.fadeOut.enabled = true;
				opGame.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			}
		} else {
			opGame.fadeOut.enabled = true;
			opGame.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		}
	}

	void SkipActive(){
		if (SaveManager.instance.state.haveInternet == true) {
			if (SaveManager.instance.state.isPurchaseRemoveAds == false) {
				if (StaticOption.isAdsSaw == true) {
					Time.timeScale = 1;
					opGame.fadeOut.enabled = true;
					opGame.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
					StaticOption.isAdsSaw = false;
				}
			}
		}
	}

	void SkipNextScene (){
		if (opGame.chooseWhat == 0) {
			if (FadeOutDone.isFadeOutDone == true) {
				int showCutScene = PlayerPrefs.GetInt ("isOnCutScene", 0);

				if (showCutScene == 1)
					SceneManager.LoadScene (opGame.nameScene);
				else
					UnityEngine.SceneManagement.SceneManager.LoadScene ("CutScene1");
			
				FadeOutDone.isFadeOutDone = false;
			}
		}
	}

	public void X2Button(){
		if (opGame.isX2Time == false) {
			opGame.isX2Time = true;
			GGAmob.Intance.ShowRewardBasedVideo ();
		}
		GGAmob.Intance.isRunx2Money = true;
	}

	void X2Active(){
		if (GGAmob.Intance.rewardedCoin == true) {
			opGame.x2Score = 2;
			if (opGame.timer >= opGame.timeInter) {
				if (opGame.X2ButtonAnim.image.fillAmount <= 1f) {
					opGame.X2ButtonAnim.image.fillAmount -= 0.1f;
				}
				opGame.timer = 0;
			}	
			StartCoroutine (waitForEndX2 (10f));
		} else {
			opGame.isX2Time = false;
		}

		if (SaveManager.instance.state.isSawAds == true) {
			opGame.X2ButtonAnim.interactable = false;
			if (opGame.timer >= opGame.timeInter) {
				if (opGame.X2ButtonAnim.image.fillAmount >= 0f) {
					SaveManager.instance.state.timeCountdown += (1f / opGame.timeAdsX2Colddown);
					SaveManager.instance.Save ();
					opGame.X2ButtonAnim.image.fillAmount = SaveManager.instance.state.timeCountdown;
				} else if(opGame.X2ButtonAnim.image.fillAmount == 1) {
					SaveManager.instance.state.isSawAds = false;
					opGame.X2ButtonAnim.interactable = true;
					SaveManager.instance.state.timeCountdown = 0;
					SaveManager.instance.Save ();
				}
				opGame.timer = 0;
			}
		}
	}

	private void OnApplicationQuit(){
		opGame.timer += Time.deltaTime;
		if (SaveManager.instance.state.isSawAds == true) {
			opGame.X2ButtonAnim.interactable = false;
			if (opGame.timer >= opGame.timeInter) {
				if (opGame.X2ButtonAnim.image.fillAmount >= 0f) {
					SaveManager.instance.state.timeCountdown += (1f / opGame.timeAdsX2Colddown);
					SaveManager.instance.Save ();
					opGame.X2ButtonAnim.image.fillAmount = SaveManager.instance.state.timeCountdown;
				} else if(opGame.X2ButtonAnim.image.fillAmount == 1) {
					SaveManager.instance.state.isSawAds = false;
					opGame.X2ButtonAnim.interactable = true;
					SaveManager.instance.state.timeCountdown = 0;
					SaveManager.instance.Save ();
				}
				opGame.timer = 0;
			}
		}
	}

	// Cong them tien vao gia tien tong
	void SaveMoney(long value){
		SaveManager.instance.state.money += value;
		SaveManager.instance.Save ();
	}

	// Reset lai coin khi hoan thanh man choi
	void ResetCoin(){
		SaveManager.instance.state.coin = 0;
		SaveManager.instance.Save ();
	}

	IEnumerator waitForEndX2(float time){
		yield return new WaitForSeconds (time);
		opGame.x2Score = 1;
		GGAmob.Intance.rewardedCoin = false;
		SaveManager.instance.state.isSawAds = true;
	}
}


/// <summary>
/// Game option.
/// </summary>
[Serializable]
public class GameOption {	

	public float totalTime;
	public string nameScene;
	public float timer = 0;
	public float timeInter = 1f; 
	public int x2Score = 1;
	public bool isX2Time;
	public bool pressSkip, gameDone;
	public float timeAdsX2Colddown;

	public int chooseWhat;

	public Animator fadeIn;
	public Animator fadeOut;

	public GameObject coinRain, continuePanel;

	public UnityEngine.UI.Button X2ButtonAnim;

	public UnityEngine.UI.Text textCoinConinute;

	[Header("Animation for Iphone")]
	public Animator[] coinSafe;

	[Header("Animation for IphoneX")]
	public Animator[] coinSafeX;

	[Header("Animation for Ipad")]
	public Animator[] coinSafeIpad;

	[Header("Text for Iphone")]
	public UnityEngine.UI.Text moneyText, coinText, countDownText, timeCoin, limitBonus;

	[Header("Text for IphoneX")]
	public UnityEngine.UI.Text moneyTextX, coinTextX, countDownTextX, timeCoinX, limitBonusX;

	[Header("Text for Ipad")]
	public UnityEngine.UI.Text moneyTextIpad, coinTextIpad, countDownTextIpad, timeCoinIpad, limitBonusIpad;
}

public static class StaticOption{
	public static double tongDouble;
	public static int tongInt;
	public static bool isVideoReward = false;
	public static bool isSawVideo = false;
	public static bool isAdsSaw = false;
	public static bool isEffectDone = false;
	public static int limitTimeBonus = 5;
	public static bool isiAp = false;
}

