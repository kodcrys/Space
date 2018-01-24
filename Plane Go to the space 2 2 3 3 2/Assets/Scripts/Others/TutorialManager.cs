using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

	[SerializeField]
	List<GameObject> tutorialsLst;

	[SerializeField]
	GameObject panelTut;

	[SerializeField]
	GameObject nextBtn, preBtn, closeBtn;

	[SerializeField]
	GameObject FrameMenu;

	int indexPage;

	void Update(){
		if (indexPage != 0 && indexPage < tutorialsLst.Count - 1)
			preBtn.SetActive (true);
		else
			preBtn.SetActive (false);
		
		if (indexPage < tutorialsLst.Count - 1)
			nextBtn.SetActive (true);
		else
			nextBtn.SetActive (false);
	
		if (indexPage >= tutorialsLst.Count - 1) {
			nextBtn.SetActive (false);
			preBtn.SetActive (false);
			closeBtn.SetActive (true);
		}
	}
	
	public void ShowTutorials(){
		indexPage = 0;

		tutorialsLst [indexPage].SetActive (true);

		for (int i = 0; i < tutorialsLst.Count; i++) {
			if (i == indexPage)
				tutorialsLst [i].SetActive (true);
			else
				tutorialsLst [i].SetActive (false);
		}
		//FrameMenu.SetActive (false);

		panelTut.SetActive (false);
		nextBtn.SetActive (true);
		preBtn.SetActive (false);
		closeBtn.SetActive (false);

		panelTut.SetActive (true);
	}

	public void NextPageTut(){
		if (indexPage < tutorialsLst.Count - 1)
			indexPage++;

		for (int i = 0; i < tutorialsLst.Count; i++) {
			if (i == indexPage)
				tutorialsLst [i].SetActive (true);
			else
				tutorialsLst [i].SetActive (false);
		}
	}

	public void PrePageTut(){
		if (indexPage > 0)
			indexPage--;

		for (int i = 0; i < tutorialsLst.Count; i++) {
			if (i == indexPage)
				tutorialsLst [i].SetActive (true);
			else
				tutorialsLst [i].SetActive (false);
		}
	}

	public void CloseTut(){
		FrameMenu.SetActive (true);
		panelTut.SetActive (false);
	}
}
