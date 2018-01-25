using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI.Extensions;

public class LoadDataUpgradeScene : MonoBehaviour {

	/*
	 *  list StatusOfSpaceship
	 *  0. earth
	 * 	1. wood
	 *  2. water
	 *  3. fire
	 *  4. metal
	 */

	public StatusOfSpaceship[] statusOfSpaceship;

	public HorizontalScrollSnap horizontalScrollSnap;

	[SerializeField]
	UnityEngine.UI.Button upgradeFuel, upgradeSpeed, upgradeFlexibility;

	[SerializeField]
	UnityEngine.UI.Text nameSpaceshipText;

	[SerializeField]
	UnityEngine.UI.Text totalMoneyTxt;

	int[] fuelArr, speedArr, flexibilityArr, moneyUpgrateFuelArr, moneyUpgrateSpeedArr, moneyUpgrateFlexArr;

	public bool isPlayGamePlane;

	void Awake(){
		horizontalScrollSnap.isUpgradeScene = true;
	}

	// Use this for initialization
	void Start () {
		isPlayGamePlane = false;
		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs.SetInt ("money", 9000000);

		PlayerPrefs.SetInt ("unlockChar0", 1);

		upgradeFuel.onClick.AddListener (() => {
			UpgradeFuel (horizontalScrollSnap.CurrentPage);
		});

		upgradeSpeed.onClick.AddListener (() => {
			UpgradeSpeed (horizontalScrollSnap.CurrentPage);
		});

		upgradeFlexibility.onClick.AddListener (() => {
			UpgradeFlexibility (horizontalScrollSnap.CurrentPage);
		});

		fuelArr = new int[statusOfSpaceship.Length];
		speedArr = new int[statusOfSpaceship.Length];
		flexibilityArr = new int[statusOfSpaceship.Length];

		moneyUpgrateFuelArr = new int[statusOfSpaceship.Length];
		moneyUpgrateSpeedArr = new int[statusOfSpaceship.Length];
		moneyUpgrateFlexArr = new int[statusOfSpaceship.Length];

		GetStatusAllSpaceship ();

		SetStatusAllSpaceship ();



		for (int i = 0; i < statusOfSpaceship.Length; i++) {
			statusOfSpaceship [i].HideEffect ();
			statusOfSpaceship [i].EvolSpaceship (i);
		}

		//
		totalMoneyTxt.text =  AbbrevationUtility.FormatNumber(SaveManager.instance.state.money);

		horizontalScrollSnap.CurrentPage = SaveManager.instance.state.startingScreen;
		ShowInfo ();
	}

	public void GetStatusAllSpaceship(){
		for (int i = 0; i < statusOfSpaceship.Length; i++) {
			fuelArr [i] = PlayerPrefs.GetInt ("curFuel" + i, statusOfSpaceship [i].fuelCur);
			speedArr [i] = PlayerPrefs.GetInt ("curSpeed" + i, statusOfSpaceship [i].speedCur);
			flexibilityArr [i] = PlayerPrefs.GetInt ("curFlexibility" + i, statusOfSpaceship [i].flexibilityCur);

			moneyUpgrateFuelArr [i] = PlayerPrefs.GetInt ("moneyFuel" + i, statusOfSpaceship [i].moneyFuel);
			moneyUpgrateSpeedArr[i] = PlayerPrefs.GetInt ("moneySpeed" + i, statusOfSpaceship [i].moneySpeed);
			moneyUpgrateFlexArr[i] = PlayerPrefs.GetInt ("moneyFlex" + i, statusOfSpaceship [i].moneyFlexibility);

			statusOfSpaceship [i].CheckStatusUnlockCharacter (i, statusOfSpaceship[i].costSpaceship);
		}
	}

	public void SetStatusAllSpaceship(){
		for (int i = 0; i < statusOfSpaceship.Length; i++) {
			statusOfSpaceship [i].fuelCur = fuelArr [i];
			statusOfSpaceship [i].speedCur = speedArr [i];
			statusOfSpaceship [i].flexibilityCur = flexibilityArr [i];

			statusOfSpaceship [i].moneyFuel = moneyUpgrateFuelArr [i];
			statusOfSpaceship [i].moneySpeed = moneyUpgrateSpeedArr [i];
			statusOfSpaceship [i].moneyFlexibility = moneyUpgrateFlexArr [i];


			PlayerPrefs.SetInt ("curFuel" + i, statusOfSpaceship [i].fuelCur);
			PlayerPrefs.SetInt ("curSpeed" + i, statusOfSpaceship [i].speedCur);
			PlayerPrefs.SetInt ("curFlexibility" + i, statusOfSpaceship [i].flexibilityCur);
			//Dat saze o day
		}
	}

	public void UpgradeFuel(int indexSpaceship){
		long totalMoney = SaveManager.instance.state.money;

		if (totalMoney >= statusOfSpaceship [indexSpaceship].moneyFuel) {
			
			// Luu lai
			if (statusOfSpaceship [indexSpaceship].fuelCur < statusOfSpaceship [indexSpaceship].fuelMax) {

				SoundManager.Intance.PlayClickSuccessBuyItem ();

				// Update info
				statusOfSpaceship [indexSpaceship].PlusFuel ();
				statusOfSpaceship [indexSpaceship].UpdatePowerFuel ();
				statusOfSpaceship [indexSpaceship].UpdatePowerFuelMax ();
				statusOfSpaceship [indexSpaceship].UpdateValueFuel ();

				totalMoney -= statusOfSpaceship [indexSpaceship].moneyFuel;
				SaveManager.instance.state.money = totalMoney;
				SaveManager.instance.Save ();

				totalMoneyTxt.text = AbbrevationUtility.FormatNumber (totalMoney);

				// Thay doi gia, hien thi lai gia tien
				statusOfSpaceship [indexSpaceship].ChangeMoneyFuel ();

				PlayerPrefs.SetInt ("moneyFuel" + indexSpaceship, statusOfSpaceship [indexSpaceship].moneyFuel);

				if (statusOfSpaceship [indexSpaceship].fuelCur >= statusOfSpaceship [indexSpaceship].fuelMax)
					statusOfSpaceship [indexSpaceship].MaxFuel ();

				//evol
				statusOfSpaceship [indexSpaceship].EvolSpaceship (indexSpaceship);

			} else {
				statusOfSpaceship [indexSpaceship].MaxFuel ();
				SoundManager.Intance.PlayClickFailBuyItem ();
			}
		} else {
			SoundManager.Intance.PlayClickFailBuyItem ();
		}

		PlayerPrefs.SetInt ("curFuel" + indexSpaceship, statusOfSpaceship [indexSpaceship].fuelCur);
		//SetStatusAllSpaceship ();
	}

	public void UpgradeSpeed (int indexSpaceship){
		long totalMoney = SaveManager.instance.state.money;

		if (totalMoney >= statusOfSpaceship [indexSpaceship].moneySpeed) {
			// Luu lai
			if (statusOfSpaceship [indexSpaceship].speedCur < statusOfSpaceship [indexSpaceship].speedMax) {

				SoundManager.Intance.PlayClickSuccessBuyItem ();

				// Update info
				statusOfSpaceship [indexSpaceship].PlusSpeed ();
				statusOfSpaceship [indexSpaceship].UpdatePowerSpeed ();
				statusOfSpaceship [indexSpaceship].UpdatePowerSpeedMax ();
				statusOfSpaceship [indexSpaceship].UpdateValueSpeed ();

				totalMoney -= statusOfSpaceship [indexSpaceship].moneySpeed;
				SaveManager.instance.state.money = totalMoney;
				SaveManager.instance.Save ();

				totalMoneyTxt.text = AbbrevationUtility.FormatNumber (totalMoney);

				// Thay doi gia, hien thi lai gia tien
				statusOfSpaceship [indexSpaceship].ChangeMoneySpeed ();

				PlayerPrefs.SetInt ("moneySpeed" + indexSpaceship, statusOfSpaceship [indexSpaceship].moneySpeed);

				if (statusOfSpaceship [indexSpaceship].speedCur >= statusOfSpaceship [indexSpaceship].speedMax)
					statusOfSpaceship [indexSpaceship].MaxSpeed ();
				
				//evol
				statusOfSpaceship [indexSpaceship].EvolSpaceship (indexSpaceship);

			} else {
				statusOfSpaceship [indexSpaceship].MaxSpeed ();
				SoundManager.Intance.PlayClickFailBuyItem ();
			}
		} else {
			SoundManager.Intance.PlayClickFailBuyItem ();
		}

		PlayerPrefs.SetInt ("curSpeed" + indexSpaceship, statusOfSpaceship [indexSpaceship].speedCur);
		//SetStatusAllSpaceship ();
	}

	public void UpgradeFlexibility (int indexSpaceship){
		long totalMoney = SaveManager.instance.state.money;

		if (totalMoney >= statusOfSpaceship [indexSpaceship].moneyFlexibility) {
			
			// Luu lai
			if (statusOfSpaceship [indexSpaceship].flexibilityCur < statusOfSpaceship [indexSpaceship].flexibilityMax) {

				SoundManager.Intance.PlayClickSuccessBuyItem ();

				// Update info
				statusOfSpaceship [indexSpaceship].PlusFlexibility ();
				statusOfSpaceship [indexSpaceship].UpdatePowerFlexibility ();
				statusOfSpaceship [indexSpaceship].UpdatePowerFlexibilityMax ();
				statusOfSpaceship [indexSpaceship].UpdateValueFlexibility ();

				totalMoney -= statusOfSpaceship [indexSpaceship].moneyFlexibility;
				SaveManager.instance.state.money = totalMoney;
				SaveManager.instance.Save ();

				totalMoneyTxt.text = AbbrevationUtility.FormatNumber (totalMoney);

				// Thay doi gia, hien thi lai gia tien
				statusOfSpaceship [indexSpaceship].ChangeMoneyFlexibility ();

				PlayerPrefs.SetInt ("moneyFlex" + indexSpaceship, statusOfSpaceship [indexSpaceship].moneyFlexibility);

				if (statusOfSpaceship [indexSpaceship].flexibilityCur >= statusOfSpaceship [indexSpaceship].flexibilityMax)
					statusOfSpaceship [indexSpaceship].MaxFlexibility ();

				//evol
				statusOfSpaceship [indexSpaceship].EvolSpaceship (indexSpaceship);

			} else {
				SoundManager.Intance.PlayClickFailBuyItem ();
				statusOfSpaceship [indexSpaceship].MaxFlexibility ();
			}
		} else {
			SoundManager.Intance.PlayClickFailBuyItem ();
		}

		PlayerPrefs.SetInt ("curFlexibility" + indexSpaceship, statusOfSpaceship [indexSpaceship].flexibilityCur);
		//SetStatusAllSpaceship ();
	}

	[SerializeField]
	private GameObject[] overEffect;

	public void HideOverEffect ()
	{
		for (int i = 0; i < overEffect.Length; i++)
			overEffect [i].SetActive (false);
	}

	public void ShowOverEffect ()
	{
		for (int i = 0; i < overEffect.Length; i++)
			overEffect [i].SetActive (true);
	}

	public void ShowInfo(){
		GetStatusAllSpaceship ();

		statusOfSpaceship [horizontalScrollSnap.CurrentPage].GetNameSpaceship (nameSpaceshipText);

		statusOfSpaceship [horizontalScrollSnap.CurrentPage].UpdatePowerFuel ();
		statusOfSpaceship [horizontalScrollSnap.CurrentPage].UpdatePowerFuelMax ();
		statusOfSpaceship [horizontalScrollSnap.CurrentPage].UpdateValueFuel ();
		if (statusOfSpaceship [horizontalScrollSnap.CurrentPage].fuelCur < statusOfSpaceship [horizontalScrollSnap.CurrentPage].fuelMax)
			statusOfSpaceship [horizontalScrollSnap.CurrentPage].DisplayMoneyFuel ();
		else
			statusOfSpaceship [horizontalScrollSnap.CurrentPage].MaxFuel ();

		statusOfSpaceship [horizontalScrollSnap.CurrentPage].UpdatePowerSpeed ();
		statusOfSpaceship [horizontalScrollSnap.CurrentPage].UpdatePowerSpeedMax ();
		statusOfSpaceship [horizontalScrollSnap.CurrentPage].UpdateValueSpeed ();
		if (statusOfSpaceship [horizontalScrollSnap.CurrentPage].speedCur < statusOfSpaceship [horizontalScrollSnap.CurrentPage].speedMax)
			statusOfSpaceship [horizontalScrollSnap.CurrentPage].DisplayMoneySpeed ();
		else
			statusOfSpaceship [horizontalScrollSnap.CurrentPage].MaxSpeed ();

		statusOfSpaceship [horizontalScrollSnap.CurrentPage].UpdatePowerFlexibility ();
		statusOfSpaceship [horizontalScrollSnap.CurrentPage].UpdatePowerFlexibilityMax ();
		statusOfSpaceship [horizontalScrollSnap.CurrentPage].UpdateValueFlexibility ();
		if (statusOfSpaceship [horizontalScrollSnap.CurrentPage].flexibilityCur < statusOfSpaceship [horizontalScrollSnap.CurrentPage].flexibilityMax)
			statusOfSpaceship [horizontalScrollSnap.CurrentPage].DisplayMoneyFlexibility ();
		else
			statusOfSpaceship [horizontalScrollSnap.CurrentPage].MaxFlexibility ();
	}

	public void UnlockCharacter(int indexChar){
		statusOfSpaceship [indexChar].UnlockCharacter (indexChar, statusOfSpaceship[indexChar].costSpaceship);
		totalMoneyTxt.text = AbbrevationUtility.FormatNumber(SaveManager.instance.state.money);
	}

	public void Play(){
		if (gameObject.activeSelf) {
			int unlockStt = PlayerPrefs.GetInt ("unlockChar" + horizontalScrollSnap.CurrentPage, 0);
			if (unlockStt == 1) {
				SoundManager.Intance.PlayClickBtn ();
				StatusOfSpaceship spaceship = statusOfSpaceship [horizontalScrollSnap.CurrentPage];

				spaceship.ShowEffect (horizontalScrollSnap.CurrentPage);

				PlaneData.index = horizontalScrollSnap.CurrentPage;
				isPlayGamePlane = true;

				SoundManager.Intance.PlaySpaceship ();

				SaveManager.instance.state.startingScreen = horizontalScrollSnap.CurrentPage;
				SaveManager.instance.Save ();
			}
		}
	}
}

[Serializable]
public class StatusOfSpaceship {

	// 4
	[SerializeField]
	private string name;

	// 5
	[SerializeField]
	private GameObject lockFrameOfCharacter;
	
	[SerializeField]
	public int costSpaceship;

	public GameObject[] Effect;

	[Header("Gameobject evol spaceship")]
	public UpgradeSpaceship evol;

	// 1
	[Header("Fuel of spaceship")]
	
	public int fuelCur;
	public int fuelMax;
	[SerializeField]
	List<GameObject> lstPowerFuel, lstPowerFuelMax;
	[SerializeField]
	UnityEngine.UI.Text valueFuelTxt;
	public int moneyFuel;
	[SerializeField]
	UnityEngine.UI.Text moneyFuelTxt;

	// 2
	[Header("Speed of spaceship")]
	public int speedCur;
	public int speedMax;
	[SerializeField]
	List<GameObject> lstPowerSpeed, lstPowerSpeedMax;
	[SerializeField]
	UnityEngine.UI.Text valueSpeedTxt;
	public int moneySpeed;
	[SerializeField]
	UnityEngine.UI.Text moneySpeedTxt;

	// 3
	[Header("Flexibility of spaceship")]
	public int flexibilityCur;
	public int flexibilityMax;
	[SerializeField]
	List<GameObject> lstPowerFlexibility, lstPowerflexibilityMax;
	[SerializeField]
	UnityEngine.UI.Text valueFlexibilityTxt;
	public int moneyFlexibility;
	[SerializeField]
	UnityEngine.UI.Text moneyFlexibilityTxt;

	public void EvolSpaceship(int indexSpaceship) {
		if ((fuelCur == 4) || speedCur == 4 || flexibilityCur == 4) {
			
			if (indexSpaceship == 0) {
				if (SaveManager.instance.state.indexEvolWater == 0) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol1;
					SaveManager.instance.state.indexEvolWater = 1;
				}
			}

			if (indexSpaceship == 1) {
				if (SaveManager.instance.state.indexEvolFire == 0) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol1;
					SaveManager.instance.state.indexEvolFire = 1;
				}
			}

			if (indexSpaceship == 2) {
				if (SaveManager.instance.state.indexEvolWood == 0) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol1;
					SaveManager.instance.state.indexEvolWood = 1;
				}
			}

			if (indexSpaceship == 3) {
				if (SaveManager.instance.state.indexEvolEarth == 0) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol1;
					SaveManager.instance.state.indexEvolEarth = 1;
				}
			}

			if (indexSpaceship == 4) {
				if (SaveManager.instance.state.indexEvolMetal == 0) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol1;
					SaveManager.instance.state.indexEvolMetal = 1;
				}
			}
		}

		if (fuelCur == 8 || speedCur == 8 || flexibilityCur == 8) {

			if (indexSpaceship == 0) {
				if (SaveManager.instance.state.indexEvolWater == 1) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol2;
					SaveManager.instance.state.indexEvolWater = 2;
				}
			}

			if (indexSpaceship == 1) {
				if (SaveManager.instance.state.indexEvolFire == 1) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol2;
					SaveManager.instance.state.indexEvolFire = 2;
				}
			}

			if (indexSpaceship == 2) {
				if (SaveManager.instance.state.indexEvolWood == 1) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol2;
					SaveManager.instance.state.indexEvolWood = 2;
				}
			}

			if (indexSpaceship == 3) {
				if (SaveManager.instance.state.indexEvolEarth == 1) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol2;
					SaveManager.instance.state.indexEvolEarth = 2;
				}
			}

			if (indexSpaceship == 4) {
				if (SaveManager.instance.state.indexEvolMetal == 1) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol2;
					SaveManager.instance.state.indexEvolMetal = 2;
				}
			}
		}
			
		if (fuelCur == 12 || speedCur == 12 || flexibilityCur == 12) {

			if (indexSpaceship == 0) {
				if (SaveManager.instance.state.indexEvolWater == 2) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol3;
					SaveManager.instance.state.indexEvolWater = 3;
				}
			}

			if (indexSpaceship == 1) {
				if (SaveManager.instance.state.indexEvolFire == 2) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol3;
					SaveManager.instance.state.indexEvolFire = 3;
				}
			}

			if (indexSpaceship == 2) {
				if (SaveManager.instance.state.indexEvolWood == 2) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol3;
					SaveManager.instance.state.indexEvolWood = 3;
				}
			}

			if (indexSpaceship == 3) {
				if (SaveManager.instance.state.indexEvolEarth == 2) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol3;
					SaveManager.instance.state.indexEvolEarth = 3;
				}
			}

			if (indexSpaceship == 4) {
				if (SaveManager.instance.state.indexEvolMetal == 2) {
					evol.indexEvol = UpgradeSpaceship.IndexEvol.evol3;
					SaveManager.instance.state.indexEvolMetal = 3;
				}
			}
		}

		SaveManager.instance.Save ();


	}

	public void HideEffect(){
		for (int i = 0; i < Effect.Length; i++)
			Effect [i].SetActive (false);
	}

	public void ShowEffect(int indexSpaceship){
		if (indexSpaceship == 0) {
			for (int i = 0; i < Effect.Length; i++) {
				if (i == SaveManager.instance.state.indexEvolWater)
					Effect [i].SetActive (true);
				else
					Effect [i].SetActive (false);
			}
		}
		if (indexSpaceship == 1) {
			for (int i = 0; i < Effect.Length; i++) {
				if (i == SaveManager.instance.state.indexEvolFire)
					Effect [i].SetActive (true);
				else
					Effect [i].SetActive (false);
			}
		}
		if (indexSpaceship == 2) {
			for (int i = 0; i < Effect.Length; i++) {
				if (i == SaveManager.instance.state.indexEvolWood)
					Effect [i].SetActive (true);
				else
					Effect [i].SetActive (false);
			}
		}
		if (indexSpaceship == 3) {
			for (int i = 0; i < Effect.Length; i++) {
				if (i == SaveManager.instance.state.indexEvolEarth)
					Effect [i].SetActive (true);
				else
					Effect [i].SetActive (false);
			}
		}
		if (indexSpaceship == 4) {
			for (int i = 0; i < Effect.Length; i++) {
				if (i == SaveManager.instance.state.indexEvolMetal)
					Effect [i].SetActive (true);
				else
					Effect [i].SetActive (false);
			}
		}
		//for (int i = 0; i < Effect.Length; i++)
		//	Effect [i].SetActive (true);
	}

	// 4.1
	public void GetNameSpaceship(UnityEngine.UI.Text nameSpaceship){
		nameSpaceship.text = name;
	}

	// 5.1
	public void UnlockCharacter(int indexChar, int cost){
		long totalMoney = SaveManager.instance.state.money;
		int isUnlock = PlayerPrefs.GetInt ("unlockChar" + indexChar, 0);

		if (isUnlock == 0 && totalMoney >= cost) { 
			totalMoney -= cost;
			lockFrameOfCharacter.SetActive (false);
			PlayerPrefs.SetInt ("unlockChar" + indexChar, 1);
			SaveManager.instance.state.money = totalMoney;
			SaveManager.instance.Save ();
		}
	}

	// 5.2
	public void LockCharacter(int indexChar){
		lockFrameOfCharacter.SetActive (true);
		PlayerPrefs.SetInt ("unlockChar" + indexChar, 0);
	}

	// 5.3
	public void CheckStatusUnlockCharacter(int indexChar, int cost){
		if (costSpaceship == 0) {
			lockFrameOfCharacter.SetActive (false);
			PlayerPrefs.SetInt ("unlockChar" + indexChar, 1);
		}

		int checkUnlock = PlayerPrefs.GetInt ("unlockChar" + indexChar, 0);
		if (checkUnlock == 0)
			lockFrameOfCharacter.SetActive (true);
		else
			lockFrameOfCharacter.SetActive (false);
	}

	// 1.1 Cong fuel
	public void PlusFuel() {
		if (fuelCur < fuelMax)
			fuelCur++;
	}

	// 1.2 Cap nhat frame power fuel
	public void UpdatePowerFuel() {
		for (int i = 0; i < lstPowerFuel.Count; i++) {
			if (i < fuelCur)
				lstPowerFuel [i].SetActive (true);
			else
				lstPowerFuel [i].SetActive (false);
		}	
	}

	// 1.3 Cap nhat frame power fuel max
	public void UpdatePowerFuelMax() {
		for (int i = 0; i < lstPowerFuel.Count; i++) {
			if (i < fuelMax)
				lstPowerFuelMax [i].SetActive (true);
			else
				lstPowerFuelMax [i].SetActive (false);
		}	
	}

	// 1.4 Cap nhat text fuel
	public void UpdateValueFuel() {
		valueFuelTxt.text = fuelCur + "/" + fuelMax;
	}

	// 1.5 Cap nhat money to update fuel
	public void DisplayMoneyFuel() {
		moneyFuelTxt.text = AbbrevationUtility.FormatNumber (moneyFuel);
	}

	// 1.6 Cap nhat gia tien moi khi mua
	public void ChangeMoneyFuel() {
		moneyFuel *= 2;
		DisplayMoneyFuel ();
	}

	// 1.7 Max fuel display
	public void MaxFuel(){
		moneyFuelTxt.text = "MAX";
	}

	// 2.1 Cong speed
	public void PlusSpeed() {
		if (speedCur < speedMax)
			speedCur++;
	}

	// 2.2 Cap nhat frame power speedisPlayGamePlane
	public void UpdatePowerSpeed(){
		for (int i = 0; i < lstPowerSpeed.Count; i++) {
			if (i < speedCur)
				lstPowerSpeed [i].SetActive (true);
			else
				lstPowerSpeed [i].SetActive (false);
		}	
	}

	// 2.3 Cap nhat frame power speed max
	public void UpdatePowerSpeedMax(){
		for (int i = 0; i < lstPowerSpeedMax.Count; i++) {
			if (i < speedMax)
				lstPowerSpeedMax [i].SetActive (true);
			else
				lstPowerSpeedMax [i].SetActive (false);
		}	
	}

	// 2.4 Cap nhat text speed
	public void UpdateValueSpeed(){
		valueSpeedTxt.text = speedCur + "/" + speedMax;
	}

	// 2.5 Cap nhat money to update speed
	public void DisplayMoneySpeed(){
		moneySpeedTxt.text = AbbrevationUtility.FormatNumber(moneySpeed);
	}

	// 2.6 Cap nhat gia tien moi khi mua
	public void ChangeMoneySpeed() {
		moneySpeed *= 2;
		DisplayMoneySpeed ();
	}

	// 2.7 max speed display
	public void MaxSpeed(){
		moneySpeedTxt.text = "MAX";
	}

	// 3.1 Cong flexibility
	public void PlusFlexibility() {
		if (flexibilityCur < flexibilityMax)
			flexibilityCur++;
	}

	// 3.2 Cap nhat frame power flexibilityindexItemindexItem
	public void UpdatePowerFlexibility(){
		for (int i = 0; i < lstPowerFlexibility.Count; i++) {
			if (i < flexibilityCur)
				lstPowerFlexibility [i].SetActive (true);
			else 
				lstPowerFlexibility [i].SetActive (false);
		}	
	}

	// 3.3 Cap nhat frame power flexibility max
	public void UpdatePowerFlexibilityMax(){
		for (int i = 0; i < lstPowerflexibilityMax.Count; i++) {
			if (i < flexibilityMax)
				lstPowerflexibilityMax [i].SetActive (true);
			else
				lstPowerflexibilityMax [i].SetActive (false);
		}	
	}

	// 3.4 Cap nhat text flexibility
	public void UpdateValueFlexibility(){
		valueFlexibilityTxt.text = flexibilityCur + "/" + flexibilityMax;
	}

	// 3.5 Cap nhat money to update speed
	public void DisplayMoneyFlexibility(){
		moneyFlexibilityTxt.text = AbbrevationUtility.FormatNumber (moneyFlexibility);
	}

	// 3.6 Cap nhat gia tien moi khi mua
	public void ChangeMoneyFlexibility() {
		moneyFlexibility *= 2;
		DisplayMoneyFlexibility ();
	}

	// 3.7 Max 
	public void MaxFlexibility(){
		moneyFlexibilityTxt.text = "MAX";
	}
}
