using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AppAdvisory.social;

public class UIAnimation : MonoBehaviour {

	// Cac trang thai cua Animation, khong chay ani, ani cua menuBar, ani khi bam nut, duoc dung trong update
	public enum AniState {none, menuBarAni, pressBtnAni, pressBtnAniDontLoop, planeTakeOff, modeAniScene, changeOption, pressOption, handAniTut};

	[SerializeField]
	LoadDataUpgradeScene loadDataUpgradeScene;

	// Bien aniState nhan trang thai

	public AniState aniState = AniState.none;

	// Bien chay thoi gian doi
	float timeWait = 0;

	// Bien gioi han thoi gian
	[SerializeField]
	float timeWaitMax;

	// Bien khoi tao tu class UIAni ben duoi
	public UIAni ani;

	[SerializeField]
	UIAnimation frameMenu;

	public ObjectWhenPlaneTakeOff objWhenTakeOff;

	[SerializeField]
	int index;

	// UI img cua object dat script nay, dung de thay doi hinh anh cua image
	UnityEngine.UI.Image img;

	[SerializeField]
	Animator fade;

	public static bool isFadeBegin;

	bool onclick;

	float startTimeDisplayBtn;

	[SerializeField]
	bool cutSceneOnOffAni;

	[SerializeField]
	TutorialManager tutManager;
	[SerializeField]
	GameObject FrameMenu;

	[Header ("ChangeOption")]
	public bool changeOption;
	public bool isOption;
	[SerializeField]
	GameObject menu, option, buttonBackOption;

	[Header("RemoveAds")]
	[SerializeField]
	bool isPurchase;
	[SerializeField]
	bool isRestore;
	[SerializeField]
	UnityEngine.UI.Button btnRemoveAds;

	// Use this for initialization
	void Start () {
		isOption = false;
		changeOption = true;

		onclick = false;
		// Lay Image component
		img = GetComponent<UnityEngine.UI.Image> ();

		objWhenTakeOff.isFinish = false;

		if(ani.spriteFrame1 != null && ani.spriteFrame2 != null)
			CheckStatusSound ();

		startTimeDisplayBtn = Time.time;
		ani.timeWaitChangeColor = 0;
		ani.timeWaitChangeColorFinish = startTimeDisplayBtn + 1;

		if(ani.offSymbolCutScene != null)
			ani.HandleCutScene ();
	}
	
	// Update is called once per frame
	void Update () {

		// Kiem tra aniState giong if else
		switch (aniState) {
			case AniState.none:
				// Neu state la none thi khong lam gi ca
				break;

			case AniState.menuBarAni:
				// Neu State la menuBarAni, thi thuc hien di chuyen doi tuong len xuong vi tri cua 2 object showPos, hidePos neu cho phep
				ani.ShowHideObject (transform);
				break;

			case AniState.pressBtnAni:
				// Neu State la pressBtnAni, thi thuc hien scale doi tuong tu 2 gia tri minScale va maxScale
				if (ani.isChangeAni) {
					// Thoi gian can de chay ani
					timeWait += Time.deltaTime;
					if (timeWait < timeWaitMax) {
						ani.ScaleObject (transform);
					} else {
					if (isFadeBegin == true) {
							fade.enabled = true;
							if (FadeOutDone.isFadeOutDone == true) {
								Handle ();
								FadeOutDone.isFadeOutDone = false;
							}
						} else {
							Handle ();
						}
					}
				}
				break;

			case AniState.planeTakeOff:
				if (loadDataUpgradeScene.isPlayGamePlane && index != 0 && loadDataUpgradeScene.horizontalScrollSnap.CurrentPage + 1 == index) {
					
					objWhenTakeOff.scrollSnap.enabled = false;
					objWhenTakeOff.Move (objWhenTakeOff.panelItem, objWhenTakeOff.desPanelItem, 7);
					objWhenTakeOff.Move (objWhenTakeOff.panelStatus, objWhenTakeOff.desPanelStatus, 11);
					objWhenTakeOff.Move (objWhenTakeOff.btnPlay, objWhenTakeOff.desBtnPlay, 5);
					objWhenTakeOff.Move (objWhenTakeOff.btnNext, objWhenTakeOff.desBtnNext, 5);
					objWhenTakeOff.Move (objWhenTakeOff.btnPre, objWhenTakeOff.desBtnPre, 5);
					objWhenTakeOff.Move (objWhenTakeOff.btnClose, objWhenTakeOff.desBtnClose, 5);
					objWhenTakeOff.Move (objWhenTakeOff.totalGold, objWhenTakeOff.desTotalGold, 10);
					
					if (objWhenTakeOff.isFinish) {
					
						LightFollowObject.Intance.objectFollow = transform;
						ani.ShowHideObject (transform);
						if (ani.finishHide) {
							FadeInManager.instance.fadeOut.enabled = true;
							FadeInManager.instance.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
							if (FadeOutDone.isFadeOutDone == true) {
								if(ChooseModeSceneManager.mode == "endless")
									UnityEngine.SceneManagement.SceneManager.LoadScene ("Gameplay");
								else
									UnityEngine.SceneManagement.SceneManager.LoadScene ("ChooseLevelScene");
								SoundManager.Intance.StopSpaceship ();
								FadeOutDone.isFadeOutDone = false;
							}
						}
					}
				}
				break;

			case AniState.pressBtnAniDontLoop:
				ani.ScaleObjectDontLoop (transform);
				break;

			case AniState.modeAniScene:
				if (MultiResolution.device == "iphonex")
					transform.GetComponent<RectTransform> ().localScale = new Vector3 (0.9f, 0.9f, 1);
				else
					transform.GetComponent<RectTransform> ().localScale = new Vector3 (1.1f, 1.1f, 1);
				ani.PlayAniChooseModeScene (transform.GetComponent<UnityEngine.UI.Image> (), startTimeDisplayBtn);
				break;

			case AniState.changeOption:
				if (changeOption == false) {
					transform.localScale = Vector3.MoveTowards (transform.localScale, Vector3.zero, ani.speed / 2 * Time.deltaTime);
					if (transform.localScale == Vector3.zero) {
						changeOption = true;

						if (isOption) {
							option.SetActive (true);
							menu.SetActive (false);
							buttonBackOption.SetActive (true);
						} else {
							option.SetActive (false);
							menu.SetActive (true);
							buttonBackOption.SetActive (false);
						}
					}
				} else {
					transform.localScale = Vector3.MoveTowards (transform.localScale, new Vector3(1, 1, 1), ani.speed / 2 * Time.deltaTime);
				}
				break;

			case AniState.pressOption:
				if (ani.isChangeAni) {
					// Thoi gian can de chay ani
					timeWait += Time.deltaTime;
					if (timeWait < timeWaitMax) {
						ani.ScaleObject (transform);
					} else {
						Handle ();
					}
				}
			break;

			case AniState.handAniTut:
				ani.ScaleObject (transform);
				break;
		}
	}


	// Ham duoc dung khi click vao Button duoc dung de gan vao button do
	public void PressButton(string name){
		if (onclick == false) {
			SoundManager.Intance.PlayClickBtn ();
			onclick = true;
			// Bien dung de chay ani cua doi tuong tuong ung
			ani.isChangeAni = true;
			// Tra thoi gian ve 0
			timeWait = 0;

			if (name != "" && name == "Play") {
				isFadeBegin = true;
				CutsceneManager.whatScene = 1;
			} else {
				isFadeBegin = false;
			}
		}
	}

	// Ham xu ly sau khi chay xong Ani cua button
	void Handle() {
		// Bien ngung chay ani
		ani.isChangeAni = false;

		// Tra doi tuong ve scale nguyen goc
		transform.localScale = ani.originScale;

		// Neu bien nameSceneLoad cua ani khac null thi chuyen sang Scene co ten duoc dat trong nameSceneLoad
		if (ani.nameSceneLoad != "") {
			int showCutScene = PlayerPrefs.GetInt ("isOnCutScene", 0);

			if (showCutScene == 1)
				UnityEngine.SceneManagement.SceneManager.LoadScene (ani.nameSceneLoad);
			else
				UnityEngine.SceneManagement.SceneManager.LoadScene ("CutScene1");
		}

		// Neu 2 bien spriteFrame khac null thi thuc hien chuyen Sprite va thuc hien tat mo am thanh
		if (ani.spriteFrame1 != null && ani.spriteFrame2 != null) {
			if (img == null)
				return;
			else
				ChangeStatusSound ();
		}

		if (ani.linkRate != "")
			Application.OpenURL (ani.linkRate);

		if (ani.linkShare != "")
			Application.OpenURL (ani.linkShare);

		if (ani.linkLeaderboard != "")
			LeaderboardManager.ShowLeaderboardUI ();

		if (frameMenu != null) {
			frameMenu.aniState = AniState.changeOption;
			if (SaveManager.instance.state.isPurchaseRemoveAds) {
				if (btnRemoveAds != null)
					btnRemoveAds.interactable = false;
			}
			frameMenu.changeOption = false;
			frameMenu.isOption = !frameMenu.isOption;
		}

		if (cutSceneOnOffAni) {
			ani.ChangeOnOffleCutScene ();
			ani.HandleCutScene ();

		}

		if (tutManager != null) {
			tutManager.ShowTutorials ();
			FrameMenu.SetActive (false);
		}

		if (isPurchase){
			if (SaveManager.instance.state.isPurchaseRemoveAds == false)
				Purchaser.intance.BuyNonConsumable ();
			else if (btnRemoveAds != null)
				btnRemoveAds.interactable = false;
		}

		if (isRestore)
			Purchaser.intance.RestorePurchases ();

		onclick = false;
	}

	// 3
	// Thay doi bieu tuong nut sound and tat / mo am thanh
	void CheckStatusSound(){
		int statusSound = PlayerPrefs.GetInt ("Sound", 0);
		if (statusSound == 0) {
			img.sprite = ani.spriteFrame1;
			//SoundManager.mute = false;
			SoundManager.Intance.PlaySound();
		} else {
			img.sprite = ani.spriteFrame2;
			//SoundManager.mute = true;
			SoundManager.Intance.MuteSound();
		}
	}

	// 3 - Xu ly khi nguoi dung click vao nut sound (chuyen sang off/on sound)
	void ChangeStatusSound(){
		int statusSound = PlayerPrefs.GetInt ("Sound", 0);
		if (statusSound == 0) {
			statusSound = 1;
			SoundManager.Intance.MuteSound ();
		} else {
			statusSound = 0;
			SoundManager.Intance.PlaySound ();
		}
		PlayerPrefs.SetInt ("Sound", statusSound);
		CheckStatusSound ();
	}

	public void BackOption(){
		buttonBackOption.SetActive (false);
		changeOption = false;
		isOption = !isOption;
	}
}

[Serializable]
public class UIAni {

	// 1 - p
	[SerializeField]
	GameObject showPos;

	// 1 - p
	[SerializeField]
	GameObject hidePos;

	[SerializeField]
	GameObject hidePos2;

	// 3 - p
	public Sprite spriteFrame1, spriteFrame2;

	public bool isChangeAni;

	// 3 - p
	[HideInInspector]
	public bool isRunAniWhenPress;

	// 2 - p
	[SerializeField]
	Vector3 maxScale, minScale;

	// 2 - p
	public Vector3 originScale;

	public static bool isOnCutScene;
	public GameObject offSymbolCutScene;

	[SerializeField]
	GameObject [] tutorialScreen;


	// Toc do di chuyen cua doi tuong
	public float speed;

	public string nameSceneLoad;
	public string linkRate;
	public string linkShare;
	public string linkLeaderboard;

	public bool finishShow, finishHide;

	// 1
	public void ShowHideObject(Transform target){
		// Neu State la menuBarAni, thi thuc hien di chuyen doi tuong len xuong vi tri cua 2 object showPos, hidePos neu cho phep
		if (isChangeAni) {
			if (showPos != null) {
				target.position = Vector3.MoveTowards (target.position, showPos.transform.position, speed * Time.deltaTime);
				if (target.position == showPos.transform.position) {
					finishShow = true;
					finishHide = false;
				}
			}
		} else {
			if (hidePos != null) {
				target.position = Vector3.MoveTowards (target.position, hidePos.transform.position, speed * Time.deltaTime);
				if (target.position == hidePos.transform.position) {
					finishHide = true;
					finishShow = false;
				}
			}
		}
	}

	// 2
	public void ScaleObject(Transform target){
		// Scale object theo maxScale, minScale
		if (isRunAniWhenPress) {
			target.localScale = Vector3.MoveTowards (target.localScale, maxScale, speed * Time.deltaTime);
			if (target.localScale == maxScale)
				isRunAniWhenPress = false;
		} else {
			target.localScale = Vector3.MoveTowards (target.localScale, minScale, speed * Time.deltaTime);
			if (target.localScale == minScale)
				isRunAniWhenPress = true;
		}
	}

	[HideInInspector]
	public bool isFinishScaleDontLoop;
	public void ScaleObjectDontLoop(Transform target){
		// Scale object theo maxScale, minScale
		if (isChangeAni) {
			target.localScale = Vector3.MoveTowards (target.localScale, maxScale, speed * Time.deltaTime);
			isFinishScaleDontLoop = false;
		}
		else {
			target.localScale = Vector3.MoveTowards (target.localScale, minScale, speed * Time.deltaTime);
			if (target.localScale == minScale)
				isFinishScaleDontLoop = true;
		}
	}

	[Header("AniChooseModeScene")]
	public float timeWaitChangeColor = 0;
	public float timeWaitChangeColorFinish;

	public void PlayAniChooseModeScene(UnityEngine.UI.Image target, float startTime){
		if (isChangeAni) {
			float t = (Time.time - startTime) * speed;
			target.color = Color32.Lerp (new Color32 (255, 255, 255, 0), new Color32 (255, 255, 255, 255), t);
		} else {
			timeWaitChangeColor += Time.deltaTime;

			if (timeWaitChangeColor >= 1) {
				float t = (Time.time - timeWaitChangeColorFinish) * speed;
				target.color = Color32.Lerp (new Color32 (255, 255, 255, 0), new Color32 (255, 255, 255, 255), t);
			}
		}
	}

	public void ChangeOnOffleCutScene(){
		isOnCutScene = !isOnCutScene;

		if(isOnCutScene)
			PlayerPrefs.SetInt ("isOnCutScene", 0);
		else
			PlayerPrefs.SetInt ("isOnCutScene", 1);
	}

	public void HandleCutScene(){
		int showCutScene = PlayerPrefs.GetInt ("isOnCutScene", 0);

		if (showCutScene == 0)
			isOnCutScene = true;
		else
			isOnCutScene = false;

		if (isOnCutScene)
			offSymbolCutScene.SetActive (false);
		else
			offSymbolCutScene.SetActive (true);
	}
}

[Serializable]
public class ObjectWhenPlaneTakeOff {
	
	public UnityEngine.UI.ScrollRect scrollSnap;
	public GameObject panelStatus;
	public GameObject btnPlay;
	public GameObject btnNext;
	public GameObject btnPre;
	public GameObject panelItem;
	public GameObject btnClose;
	public GameObject totalGold;

	public GameObject desPanelItem;
	public GameObject desPanelStatus;
	public GameObject desBtnPlay;
	public GameObject desBtnNext;
	public GameObject desBtnPre;
	public GameObject desBtnClose;
	public GameObject desTotalGold;

	public bool isFinish;

	public void Move(GameObject target, GameObject des, float speed){
		target.transform.position = Vector3.MoveTowards (target.transform.position, des.transform.position, speed * Time.deltaTime);
		if (target.transform.position == des.transform.position) {
			isFinish = true;
		}
	}
}