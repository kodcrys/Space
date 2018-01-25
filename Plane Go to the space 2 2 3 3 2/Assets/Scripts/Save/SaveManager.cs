using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

	public static SaveManager instance{ get; set;}
	public SaveState state;

	// Use this for initialization
	void Awake () {
	//	ResetSave ();
		_MakeSingleInstance ();
		Load ();
	}
	
	void _MakeSingleInstance(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	void Update(){
		CheckInternet ();
	}

	public void Save(){
		PlayerPrefs.SetString ("save", Helper.Serialize<SaveState> (state));
	}

	public void Load(){
		if (PlayerPrefs.HasKey ("save")) {
			state = Helper.Deserialize<SaveState> (PlayerPrefs.GetString ("save"));
		} else {
			state = new SaveState ();
			Save ();
		}
	}

/*	public bool IsCharacterOwned(int index){
		return(state.character & (1 << index)) != 0;
	}

	public void UnlockCharacter(int index){
		state.character |= 1 << index;
	}
		
	public bool BuyCharacter(int index, int cost){
		if (state.coin >= cost) {
			state.coin -= cost;
			UnlockCharacter (index);
			Save ();
			return true;
		} else {
			return false;
		}
	}*/

	public void ResetSave(){
//		PlayerPrefs.DeleteKey ("save");
		PlayerPrefs.DeleteAll ();
	}

	public void CheckInternet(){
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			state.haveInternet = true;
		} else {
			state.haveInternet = false;
		}
		Save ();
	}
}
