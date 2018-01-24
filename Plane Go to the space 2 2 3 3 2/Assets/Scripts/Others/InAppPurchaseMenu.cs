using UnityEngine;
using UnityEngine.UI;

public class InAppPurchaseMenu : MonoBehaviour {

	[Header("Button buy IAP")]
	[SerializeField]
	Button Buy1;
	[SerializeField]
	Button Buy5;
	[SerializeField]
	Button Buy10;
	[SerializeField]
	Button Buy15;
	[SerializeField]
	Button Buy30;
	[SerializeField]
	Button Buy50;
	[SerializeField]
	Button Buy100;

	[Header("button close IAP menu")]
	[SerializeField]
	Button CloseIAPMenu;
	[SerializeField]
	GameObject IAPmenu;

	// Use this for initialization
	void Start () {
		Buy1.onClick.AddListener (buy1);

		Buy5.onClick.AddListener (buy5);

		Buy10.onClick.AddListener (buy10);

		Buy15.onClick.AddListener (buy15);

		Buy30.onClick.AddListener (buy30);

		Buy50.onClick.AddListener (buy50);

		Buy100.onClick.AddListener (buy100);

		CloseIAPMenu.onClick.AddListener (closeIAP);
	}

	void buy1() {
		Purchaser.intance.BuyConsumable1 ();
	}

	void buy5() {
		Purchaser.intance.BuyConsumable5 ();
	}

	void buy10() {
		Purchaser.intance.BuyConsumable10 ();
	}

	void buy15() {
		Purchaser.intance.BuyConsumable15 ();
	}

	void buy30() {
		Purchaser.intance.BuyConsumable30 ();
	}

	void buy50() {
		Purchaser.intance.BuyConsumable50 ();
	}

	void buy100() {
		Purchaser.intance.BuyConsumable100 ();
	}

	void closeIAP() {
		StaticOption.isiAp = false;
		IAPmenu.SetActive (false);
	}
}
