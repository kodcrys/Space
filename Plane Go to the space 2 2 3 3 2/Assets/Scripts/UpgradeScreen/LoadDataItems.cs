using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LoadDataItems : MonoBehaviour {

	[SerializeField]
	UnityEngine.UI.Text totalMoneyTxt;

	[SerializeField]
	Items [] items;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs.SetInt ("money", 9000000);

		for (int i = 0; i < items.Length; i++) {
			items [i].GetCost (i);
			items [i].LoadItem (i);
		}
	}
	
	public void BuyItem(int indexItem){
		if(gameObject.activeSelf)
		//Debug.Log(indexItem);
			items [indexItem].BuyItem (totalMoneyTxt, indexItem);
		//items [indexItem].DisplayCost (indexItem);
	}
}

[Serializable]
public class Items {
	[SerializeField]
	string name;

	public bool status;
	[SerializeField]
	private int cost;
	[SerializeField]
	private UnityEngine.UI.Text costTxt;

	[SerializeField]
	UnityEngine.UI.Button btnBuyItem;

	public void GetCost(int indexItem) {
		cost = PlayerPrefs.GetInt ("costItem"+indexItem, cost);
	}

	public void BuyItem(UnityEngine.UI.Text totalMoneyText, int indexItem){
		long totalMoney = SaveManager.instance.state.money;
		if (totalMoney >= cost) {

			SoundManager.Intance.PlayClickSuccessBuyItem ();

			totalMoney -= cost;
			SaveManager.instance.state.money = totalMoney;
			SaveManager.instance.Save ();

			status = true;

			SetStatusItem (indexItem);

			UpdateCost (indexItem);
			DisplayCost (indexItem);
			btnBuyItem.interactable = false;
		} else {
			SoundManager.Intance.PlayClickFailBuyItem ();
		}
		totalMoneyText.text = AbbrevationUtility.FormatNumber(totalMoney);
	}

	public bool GetStatusItem(int indexItem){
		int sttItem = PlayerPrefs.GetInt ("statusItem"+indexItem, 0);
		if (sttItem == 1)
			status = true;
		else
			status = false;
		return status;
	}

	public void SetStatusItem(int indexItem){
		int checkStt = 0;
		if (status)
			checkStt = 1;
		else
			checkStt = 0;
		Debug.Log ("CheckStatus: " + checkStt);
		PlayerPrefs.SetInt ("statusItem" + indexItem, checkStt);
	}

	public void LoadItem(int indexItem){
		bool stt = GetStatusItem (indexItem);
		if (stt)
			btnBuyItem.interactable = false;
		else
			btnBuyItem.interactable = true;
		DisplayCost (indexItem);
	}
	 
	public void UpdateCost(int indexItem){
		cost += 1000;
		PlayerPrefs.SetInt ("costItem" + indexItem, cost);
		//Saze trong nay
	}

	public void DisplayCost(int indexItem){
		if (status == false) {
			int costValue = PlayerPrefs.GetInt ("costItem"+indexItem, cost);
			costTxt.text = AbbrevationUtility.FormatNumber (costValue);
		} else {
			costTxt.text = "Equipped";
		}
	}

	public void UseItem(int indexItem){
		status = false;
		SetStatusItem (indexItem);
		LoadItem (indexItem);
		DisplayCost (indexItem);
	}
}
 