using UnityEngine;
using UnityEngine.UI.Extensions;

public class StatusUI : MonoBehaviour {

	[SerializeField]
	UnityEngine.UI.Button btnPlay;

	[SerializeField]
	UnityEngine.UI.Button btnBack;

	[SerializeField]
	string nameBackScene;

	[Header("Scroll rect plane")]
	[SerializeField]
	HorizontalScrollSnap horizontalPlane;

	[Header("Button Upgrade")]
	[SerializeField]
	UnityEngine.UI.Button btnPlusFuel;
	[SerializeField]
	UnityEngine.UI.Button btnPlusSpeed;
	[SerializeField]
	UnityEngine.UI.Button btnPlusFlexibility;

	[Header("Data Spaceship")]
	[SerializeField]
	LoadDataUpgradeScene dataSpaceship;

	[SerializeField]
	GameObject canvasIphone, canvasIpad;

	[SerializeField]
	Sprite enablePlus, disablePlus;

	[SerializeField]
	UnityEngine.UI.Image plusFuleImg, plusSpeedImg, plusFlexImg;

	[SerializeField]
	UnityEngine.UI.Text totalCoin;

	void Awake(){
		if (MultiResolution.device == "ipad") {
			canvasIpad.SetActive (true);
			canvasIphone.SetActive (false);
		} else {
			canvasIpad.SetActive (false);
			canvasIphone.SetActive (true);
		}
	}

	// Use this for initialization
	void Start () {
		btnBack.onClick.AddListener (() => {
			BackSceneButton (nameBackScene);
		});
	}
	
	// Update is called once per frame
	void Update () {
		CheckStatusUI ();
		UpdateCoin ();
	}

	[SerializeField]
	HorizontalScrollSnap horizontalScrollsnap;
	public void BackSceneButton(string nameScene){
		SoundManager.Intance.PlayClickBtn ();
		UIManager.Instance.BackHome (nameScene);
		SaveManager.instance.state.startingScreen = horizontalScrollsnap.CurrentPage;
		SaveManager.instance.Save ();
	}

	public void UpdateCoin() {
		totalCoin.text = AbbrevationUtility.FormatNumber(SaveManager.instance.state.money);
	}

	void CheckStatusUI(){
		if (horizontalPlane != null) {

			int statusChar = PlayerPrefs.GetInt ("unlockChar" + horizontalPlane.CurrentPage, 0);
			double totalMoney = SaveManager.instance.state.money;

			if (btnPlay != null && btnPlusFuel != null && btnPlusSpeed != null && btnPlusFlexibility != null) {
				if (statusChar == 0) {
					btnPlay.interactable = false;

					btnPlusFuel.interactable = false;
					plusFuleImg.sprite = disablePlus;

					btnPlusSpeed.interactable = false;
					plusSpeedImg.sprite = disablePlus;

					btnPlusFlexibility.interactable = false;
					plusFlexImg.sprite = disablePlus;
				} else {
					if (dataSpaceship.statusOfSpaceship [horizontalPlane.CurrentPage].moneyFuel <= totalMoney &&
					    dataSpaceship.statusOfSpaceship [horizontalPlane.CurrentPage].fuelCur < dataSpaceship.statusOfSpaceship [horizontalPlane.CurrentPage].fuelMax) {
						btnPlusFuel.interactable = true;
						plusFuleImg.sprite = enablePlus;
					} else {
						btnPlusFuel.interactable = false;
						plusFuleImg.sprite = disablePlus;
					}
					if (dataSpaceship.statusOfSpaceship [horizontalPlane.CurrentPage].moneySpeed <= totalMoney &&
					    dataSpaceship.statusOfSpaceship [horizontalPlane.CurrentPage].speedCur < dataSpaceship.statusOfSpaceship [horizontalPlane.CurrentPage].speedMax) {
						btnPlusSpeed.interactable = true;
						plusSpeedImg.sprite = enablePlus;
					} else {
						btnPlusSpeed.interactable = false;
						plusSpeedImg.sprite = disablePlus;
					}
					if (dataSpaceship.statusOfSpaceship [horizontalPlane.CurrentPage].moneyFlexibility <= totalMoney &&
					    dataSpaceship.statusOfSpaceship [horizontalPlane.CurrentPage].flexibilityCur < dataSpaceship.statusOfSpaceship [horizontalPlane.CurrentPage].flexibilityMax) {
						btnPlusFlexibility.interactable = true;
						plusFlexImg.sprite = enablePlus;
					} else {
						btnPlusFlexibility.interactable = false;
						plusFlexImg.sprite = disablePlus;
					}
					btnPlay.interactable = true;
				}
			}
		}
	}
}
