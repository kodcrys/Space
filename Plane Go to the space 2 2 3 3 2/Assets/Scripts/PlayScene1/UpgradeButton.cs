using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// Gameobject: Performent, Storage, Countdown, TimeCoin

public class UpgradeButton : MonoBehaviour {

	/// <summary>
	/// State update cua cac nut update.
	/// </summary>
	public enum stateUpdate { none, updateCoinClick, updateStorage, updateCountDown, updateTimeCoin};
	public stateUpdate updateState = stateUpdate.none;

	[Header("Option of Update Button")]
	[SerializeField]
	UpdateOption updateGame;

	void Start(){
		switch (updateState) {

		// Thiet lap thong so rieng cho nut update coin click
		case stateUpdate.updateCoinClick:
			updateGame.price = SaveManager.instance.state.coinUpdateClick;
			updateGame.effect = SaveManager.instance.state.coinClick;
			updateGame.valueEffect = (updateGame.effect + 1);
			updateGame.levelUpdate = SaveManager.instance.state.levelCoinClick;
			updateGame.updateEffectText.text = AbbrevationUtility.FormatNumber (updateGame.effect);
			break;

		// Thiet lap thong so rieng cho nut update storage
		case stateUpdate.updateStorage:
			updateGame.price = SaveManager.instance.state.coinUpdateStorage;
			updateGame.effect = SaveManager.instance.state.storage;
			updateGame.valueEffect = (updateGame.effect * 2);
			updateGame.levelUpdate = SaveManager.instance.state.levelStorage;
			updateGame.updateEffectText.text = AbbrevationUtility.FormatNumber (updateGame.effect);
			break;

		// Thiet lap thong so rieng cho nut update count down time
		case stateUpdate.updateCountDown:
			updateGame.price = SaveManager.instance.state.coinUpdateBonusTime;
			updateGame.effect = (int)SaveManager.instance.state.bonusTime;
			updateGame.valueEffect = 5;
			updateGame.isBonus = false;
			updateGame.levelUpdate = SaveManager.instance.state.levelBonusTime;
			updateGame.updateEffectText.text = "+" + updateGame.valueEffect;
			break;

		// Thiet lap thong so rieng cho nut update time coin
		case stateUpdate.updateTimeCoin:
			updateGame.price = SaveManager.instance.state.coinUpdateTimeCoin;
			updateGame.effect = SaveManager.instance.state.timeCoin;
			updateGame.valueEffect = (updateGame.effect + 5);
			updateGame.levelUpdate = SaveManager.instance.state.levelTimeCoin;
			updateGame.updateEffectText.text = AbbrevationUtility.FormatNumber (updateGame.effect) + "/s";
			break;
		}

		// Dong bo gia tien tong game
		updateGame.money = SaveManager.instance.state.money;

		// hien thi level cho cac update
		updateGame.levelUpdateText.text = "Level " + updateGame.levelUpdate;

		// Dong bo gia tien cua cac nut update
		updateGame.coinUpdateText.text = AbbrevationUtility.FormatNumber(updateGame.price);


	}

	void Update(){
		UpdateAndChange ();
	}

	/// <summary>
	/// Buies the update.
	/// </summary>
	void BuyUpdate(){

		// thanh toan tien khi du tien
		if (updateGame.coin >= updateGame.price) {
			
			SoundManager.Intance.PlayClickSuccessBuyItem ();

			updateGame.coin -= updateGame.price;
			updateGame.effect = updateGame.valueEffect;
			updateGame.price *= 2; // gia tien se gap doi sau moi lan mua
			updateGame.levelUpdate++; // level tang len 1
			updateGame.isBonus = true;

			switch (updateState){
			case stateUpdate.updateCountDown:
				if (StaticOption.limitTimeBonus >= 0) {
					StaticOption.limitTimeBonus--;
				}
				break;
			}
		} else if (updateGame.coin < updateGame.price && (updateGame.coin + updateGame.money) >= updateGame.price) {

			SoundManager.Intance.PlayClickSuccessBuyItem ();

			updateGame.temp = updateGame.price - updateGame.coin;
			updateGame.coin -= (updateGame.price - updateGame.temp);
			updateGame.money -= updateGame.temp;
			updateGame.effect = updateGame.valueEffect;
			updateGame.price *= 2; // gia tien se gap doi sau moi lan mua
			updateGame.levelUpdate++;
			updateGame.isBonus = true;
			// Dong bo gia tien tong game
			SaveManager.instance.state.money = updateGame.money;

			switch (updateState){
			case stateUpdate.updateCountDown:
				if (StaticOption.limitTimeBonus >= 0) {
					StaticOption.limitTimeBonus--;
				}
				break;
			}
		} else {
			SoundManager.Intance.PlayClickFailBuyItem ();
		}

		// Save lai cac thong so sau khi thuc hien thanh toan
		SaveManager.instance.state.coin = updateGame.coin;
		switch (updateState) {

		// Save lai thong so da thay doi cua update coin click
		case stateUpdate.updateCoinClick:
			SaveManager.instance.state.coinUpdateClick = updateGame.price;
			SaveManager.instance.state.coinClick = updateGame.effect;
			updateGame.valueEffect = (updateGame.effect + 1);
			SaveManager.instance.state.levelCoinClick = updateGame.levelUpdate;
			updateGame.updateEffectText.text = AbbrevationUtility.FormatNumber (updateGame.effect);
			break;
		
		// Save lai thong so da thay doi cua update storage
		case stateUpdate.updateStorage:
			SaveManager.instance.state.coinUpdateStorage = updateGame.price;
			SaveManager.instance.state.storage = updateGame.effect;
			updateGame.valueEffect = (updateGame.effect * 2);
			SaveManager.instance.state.levelStorage = updateGame.levelUpdate;
			updateGame.updateEffectText.text = AbbrevationUtility.FormatNumber (updateGame.effect);
			break;

		// Save lai thong so da thay doi cua update count down time
		case stateUpdate.updateCountDown:
			SaveManager.instance.state.coinUpdateBonusTime = updateGame.price;
			if (updateGame.isBonus == true) {
				SaveManager.instance.state.isBonusTime = true;
				updateGame.isBonus = false;
			}
			SaveManager.instance.state.levelBonusTime = updateGame.levelUpdate;
			break;

		// Save lai thong so da thay doi cua update time coin
		case stateUpdate.updateTimeCoin:
			SaveManager.instance.state.coinUpdateTimeCoin = updateGame.price;
			SaveManager.instance.state.timeCoin = updateGame.effect;
			updateGame.valueEffect = (updateGame.effect + 5);
			SaveManager.instance.state.levelTimeCoin = updateGame.levelUpdate;
			updateGame.updateEffectText.text = AbbrevationUtility.FormatNumber (updateGame.effect) + "/s";
			break;
		}
		SaveManager.instance.Save (); // Save lai tat ca thay doi tren SaveManager

		// Cap nhat lai level text sau khi thuc hien giao dich
		updateGame.levelUpdateText.text = "Level " + updateGame.levelUpdate;

		// Cap nhat lai coin update text sau khi thuc hien giao dich
		updateGame.coinUpdateText.text = "" + AbbrevationUtility.FormatNumber(updateGame.price);
	}
		
	/// <summary>
	/// Clicks the update.
	/// </summary>
	public void ClickUpdate(){
		switch (updateState) {

		case stateUpdate.updateCoinClick:
			BuyUpdate ();
			break;

		case stateUpdate.updateStorage:
			BuyUpdate ();
			break;

		case stateUpdate.updateCountDown:
			if (StaticOption.limitTimeBonus > 0) {
				BuyUpdate ();
			} else {
				SoundManager.Intance.PlayClickFailBuyItem ();
			}
			break;

		case stateUpdate.updateTimeCoin:
			BuyUpdate ();
			break;
		}
	}

	/// <summary>
	/// Cap nhat va thay doi
	/// </summary>
	void UpdateAndChange(){
		updateGame.coin = SaveManager.instance.state.coin;
		updateGame.money = SaveManager.instance.state.money;
		if (updateGame.coin >= updateGame.price) {
			updateGame.priceImage.sprite = updateGame.canBuy;
			updateGame.coinUpdateText.color = new Color32 (0, 0, 0, 255);
			updateGame.iconImage.sprite = updateGame.iconBlack;
			CanBuyAnim.canBuyItem = true;
			updateGame.canBuyAnimtion.GetComponent<Image> ().enabled = true;
		} else if (updateGame.coin < updateGame.price && (updateGame.coin + updateGame.money) >= updateGame.price) {
			updateGame.priceImage.sprite = updateGame.canBuy;
			updateGame.coinUpdateText.color = new Color32 (0, 0, 0, 255);
			updateGame.iconImage.sprite = updateGame.iconBlack;
			CanBuyAnim.canBuyItem = true;
			updateGame.canBuyAnimtion.GetComponent<Image> ().enabled = true;
		} else {
			updateGame.priceImage.sprite = updateGame.cantBuy;
			updateGame.coinUpdateText.color = new Color32 (255, 255, 255, 255);
			updateGame.iconImage.sprite = updateGame.iconWhite;
			CanBuyAnim.canBuyItem = false;
			updateGame.canBuyAnimtion.GetComponent<Image> ().enabled = false;
		}
	}
}

/// <summary>
/// Update option.
/// </summary>
[Serializable]
public class UpdateOption {	
	public int countDownCoin, x2Coin, levelUpdate;

	public long price, coin, effect, valueEffect, money, temp;

	public bool isBonus;

	public Text levelUpdateText, coinUpdateText, updateEffectText;

	public Sprite cantBuy, canBuy , iconBlack, iconWhite;

	public Image priceImage ,iconImage;

	public GameObject canBuyAnimtion;
}
