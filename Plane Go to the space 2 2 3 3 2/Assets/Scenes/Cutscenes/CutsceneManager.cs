using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {

	public static int whatScene;

	[SerializeField]
	GameObject cutScene1, cutScene2, skipButton, skipButton2;

	[SerializeField]
	string nameScene1, nameScene2;

	[SerializeField]
	int currentPage;

	[SerializeField]
	float speed, timeNext;


	[Header("Cac cutscene cua cutscene1")]
	[SerializeField]
	List<GameObject> cutSceneItem1 = new List<GameObject>();

	[Header("Cac cutscene cua cutscene2")]
	[SerializeField]
	List<GameObject> cutSceneItem2 = new List<GameObject>();

	enum cutsceneStep{none, Begin, Mid, End}

	cutsceneStep moveStep = cutsceneStep.none;

	void Start(){
		moveStep = cutsceneStep.none;
		currentPage = 0;
		if (whatScene == 1) {
			cutScene1.SetActive (true);
			cutScene2.SetActive (false);
			if (SaveManager.instance.state.cutScene1Saw) {
				skipButton.SetActive (true);
			} else {
				skipButton.SetActive (false);
			}
		} else {
			cutScene2.SetActive (true);
			cutScene1.SetActive (false);
			if (SaveManager.instance.state.cutScene2Saw) {
				skipButton2.SetActive (true);
			} else {
				skipButton2.SetActive (false);
			}
		}
	}

	void Update(){
		animCutScene ();
		if (whatScene == 1) {
			NextScene (nameScene1);
		} else {
			NextScene (nameScene2);
		}
	}

	void animCutScene(){
		switch (moveStep) {
		case cutsceneStep.none:
			if (FadeInDone.isFadeInDone) {
				moveStep = cutsceneStep.Begin;
				if (whatScene == 1) {
					if (SoundManager.Intance.BGCutScene1Sound.isPlaying == false)
						SoundManager.Intance.BGCutScene1Sound.Play ();
				} else {
					if (SoundManager.Intance.BGCutScene2Sound.isPlaying == false)
						SoundManager.Intance.BGCutScene2Sound.Play ();
				}
			}
			break;
		case cutsceneStep.Begin:
			if (whatScene == 1) {
				cutSceneItem1 [currentPage].GetComponent<Animator> ().enabled = true;

				if (CutsceneDone.doneCutScene) {
					cutSceneItem1 [currentPage].SetActive (false);
					moveStep = cutsceneStep.Mid;
					CutsceneDone.doneCutScene = false;
				}
			} else {
				cutSceneItem2 [currentPage].GetComponent<Animator> ().enabled = true;

				if (CutsceneDone.doneCutScene) {
					cutSceneItem2 [currentPage].SetActive (false);
					moveStep = cutsceneStep.Mid;
					CutsceneDone.doneCutScene = false;
				}
			}
			break;

		case cutsceneStep.Mid:
			if (whatScene == 1) {
				if (currentPage < cutSceneItem1.Count - 1) {
					currentPage++;
					cutSceneItem1 [currentPage].SetActive (true);
					moveStep = cutsceneStep.Begin;
				} else {
					moveStep = cutsceneStep.End;
				}
			} else {
				if (currentPage < cutSceneItem2.Count - 1) {
					currentPage++;
					cutSceneItem2 [currentPage].SetActive (true);
					moveStep = cutsceneStep.Begin;
				} else {
					moveStep = cutsceneStep.End;
				}
			}
			break;

		case cutsceneStep.End:
			if (SoundManager.Intance.BGCutScene1Sound.isPlaying)
				SoundManager.Intance.BGCutScene1Sound.Stop ();
			if (SoundManager.Intance.BGCutScene2Sound.isPlaying)
				SoundManager.Intance.BGCutScene2Sound.Stop ();
			if (whatScene == 1) {
				if (!SaveManager.instance.state.cutScene1Saw)
					SaveManager.instance.state.cutScene1Saw = true;
			} else {
				if (!SaveManager.instance.state.cutScene2Saw)
					SaveManager.instance.state.cutScene2Saw = true;
			}
			FadeInManager.instance.fadeOut.enabled = true;
			FadeInManager.instance.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			break;
		}
	}

	public void SkipButton(){
		FadeInManager.instance.fadeOut.enabled = true;
		FadeInManager.instance.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	void NextScene(string namescene){
		if (FadeOutDone.isFadeOutDone == true) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (namescene);
		}
	}
}
