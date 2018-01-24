using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpaceship : MonoBehaviour {
	public enum IndexEvol {none, evol1, evol2, evol3, end}
	public IndexEvol indexEvol;

	[SerializeField]
	int indexSpaceship;

	[SerializeField]
	GameObject[] evol;

	[SerializeField]
	GameObject scaleTarget;

	[SerializeField]
	Vector3 maxScale = new Vector3(0.9f, 0.9f, 1), minScale = new Vector3(0.7f, 0.7f, 1);

	// Use this for initialization
	void Start () {
		isScale = false;
		GetEvolSpaceship(indexSpaceship);
	}
	
	// Update is called once per frame
	void Update () {
		switch (indexEvol) {
			case IndexEvol.none:
				if (evol.Length != 0)
					NoneEvol ();
				break;
			case IndexEvol.evol1:
				Evol1();
				break;
			case IndexEvol.evol2:
				Evol2 ();
				break;
			case IndexEvol.evol3:
				Evol3 ();
				break;
			case IndexEvol.end:
				isScale = false;
				break;
		}
	}

	public void GetEvolSpaceship(int indexSpaceship) {
		int iEvol = 0;

		if (indexSpaceship == 0)
			iEvol = SaveManager.instance.state.indexEvolWater;
		if (indexSpaceship == 1)
			iEvol = SaveManager.instance.state.indexEvolFire;
		if (indexSpaceship == 2)
			iEvol = SaveManager.instance.state.indexEvolWood;
		if (indexSpaceship == 3)
			iEvol = SaveManager.instance.state.indexEvolEarth;
		if (indexSpaceship == 4)
			iEvol = SaveManager.instance.state.indexEvolMetal;

		if (iEvol == 0)
			indexEvol = IndexEvol.none;
		if (iEvol == 1)
			indexEvol = IndexEvol.evol1;
		if (iEvol == 2)
			indexEvol = IndexEvol.evol2;
		if (iEvol == 3)
			indexEvol = IndexEvol.evol3;
	}

	void NoneEvol(){
		for (int i = 0; i < evol.Length; i++)
			evol [i].SetActive (false);
		indexEvol = IndexEvol.end;
	}

	void Evol1(){
		
		if (evol.Length != 0) {
			for (int i = 0; i < evol.Length; i++)
				if (i == 0)
					evol [i].SetActive (true);
				else
					evol [i].SetActive (false);
		}
		ScaleAll ();
		//indexEvol = IndexEvol.end;
	}

	void Evol2(){
		
		if (evol.Length != 0) {
			if (evol [0] != null && evol[0].activeSelf) {
				foreach (Transform gob in evol[0].transform) {
					UIAnimation ui = gob.GetComponent<UIAnimation> ();	
					ui.ani.isChangeAni = false;
					if (ui.ani.isFinishScaleDontLoop)
						evol [0].SetActive (false);
				}
			}

			if (evol [0] != null && evol [0].activeSelf == false)
				evol [1].SetActive (true);
		}
		ScaleAll ();
	}

	void Evol3() {

		if (evol.Length != 0) {
			if (evol [0] != null && evol[0].activeSelf) {
				foreach (Transform gob in evol[0].transform) {
					UIAnimation ui = gob.GetComponent<UIAnimation> ();	
					ui.ani.isChangeAni = false;
					if (ui.ani.isFinishScaleDontLoop)
						evol [0].SetActive (false);
				}
			}
		

			if (evol [0] != null && evol [0].activeSelf == false && evol[1] != null)
				evol [2].SetActive (true);
		}
		ScaleAll ();
	}

	bool isScale;
	void ScaleAll() {
		if (isScale == false) {
			scaleTarget.transform.localScale = Vector3.MoveTowards (scaleTarget.transform.localScale, maxScale, 2f * Time.deltaTime);
			if (scaleTarget.transform.localScale == maxScale)
				isScale = true;
		}
		if(isScale) {
			scaleTarget.transform.localScale = Vector3.MoveTowards (scaleTarget.transform.localScale, minScale, 2f * Time.deltaTime);
			if(scaleTarget.transform.localScale == minScale)
				indexEvol = IndexEvol.end;
		}

	}
}
