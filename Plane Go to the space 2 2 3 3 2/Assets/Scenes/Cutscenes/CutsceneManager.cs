using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {

	public static int whatScene;

	public static bool nextScene, animDone;

	[SerializeField]
	string nameScene;

	[SerializeField]
	int currentCutscene;

	[SerializeField]
	GameObject skipButon;

	[Header("Cutscene 1")]
	[SerializeField]
	List<GameObject> allCutscene1 = new List<GameObject>();

	[Header("Cutscene 2")]
	[SerializeField]
	List<GameObject> allCutscene2 = new List<GameObject>();

	[Header("loi thoai 1")]
	[SerializeField]
	List<GameObject> allLoiThoai1 = new List<GameObject>();

	[Header("loi thoai 2")]
	[SerializeField]
	List<GameObject> allLoiThoai2 = new List<GameObject>();



	// Use this for initialization
	void Start () {
		SoundManager.Intance.BGSound.Play ();
		currentCutscene = 0;
		nextScene = false;
		animDone = false;
		if (whatScene == 1) {
			allCutscene1 [currentCutscene].SetActive (true);
			if (SaveManager.instance.state.cutScene1Saw == true) {
				skipButon.SetActive (true);
			} else {
				skipButon.SetActive (false);
			}
		} else {
			allCutscene2 [currentCutscene].SetActive (true);
			if (SaveManager.instance.state.cutScene2Saw == true) {
				skipButon.SetActive (true);
			} else {
				skipButon.SetActive (false);
			}
		}
	}

	void Update(){
		if (whatScene == 1) {
			if (FadeInDone.isFadeInDone == true) {
				if (nextScene == true) {
					if (animDone == true) {
						StartCoroutine (waitForLoiThoai ());
						StartCoroutine (waitForCutscene ());
					}
					nextScene = false;
				}
			}
		} else {
			if (FadeInDone.isFadeInDone == true) {
				if (nextScene == true) {
					if (animDone == true) {
						StartCoroutine (waitForLoiThoai ());
						StartCoroutine (waitForCutscene ());
					}
					nextScene = false;
				}
			}
		}
	}

	public void SkipButton(){
		StartCoroutine (DoneCutscene ());
	}

	IEnumerator waitForCutscene(){
		yield return new WaitForSeconds (3f);
		if (whatScene == 1) {
			if (currentCutscene < allCutscene1.Count - 1) {
				if (currentCutscene == 0) {
					allCutscene1 [0].SetActive (false);
					currentCutscene++;
				} else if (currentCutscene >= 2) {
					allCutscene1 [1].SetActive (false);
					allCutscene1 [2].SetActive (false);
					currentCutscene++;
				} else {
					currentCutscene++;
				}
				allCutscene1 [currentCutscene].SetActive (true);
				allCutscene1 [currentCutscene].GetComponent<Animator> ().enabled = true;
				nextScene = true;
			} else {
				StartCoroutine (DoneCutscene ());
			}
			animDone = false;
		} else {
			if (currentCutscene < allCutscene2.Count - 1) {
				if (currentCutscene == 0) {
					allCutscene2 [0].SetActive (false);
					currentCutscene++;
				} else if (currentCutscene >= 3) {
					allCutscene2 [1].SetActive (false);
					allCutscene2 [2].SetActive (false);
					allCutscene2 [3].SetActive (false);
					currentCutscene++;
				} else {
					currentCutscene++;
				}
				allCutscene2 [currentCutscene].SetActive (true);
				allCutscene2 [currentCutscene].GetComponent<Animator> ().enabled = true;
				nextScene = true;
			} else {
				StartCoroutine (DoneCutscene ());
			}
			animDone = false;
		}
	}

	IEnumerator waitForLoiThoai(){
		yield return new WaitForSeconds (0.5f);
		if (whatScene == 1) {
			allLoiThoai1 [currentCutscene].SetActive (true);
		} else {
			allLoiThoai2 [currentCutscene].SetActive (true);
		}
	}

	IEnumerator DoneCutscene(){
		yield return new WaitForSeconds (0.2f);
		if (whatScene == 1) {
			if (SaveManager.instance.state.cutScene1Saw == false)
				SaveManager.instance.state.cutScene1Saw = true;
		} else {
			if (SaveManager.instance.state.cutScene2Saw == false)
				SaveManager.instance.state.cutScene2Saw = true;
		}
		FadeInManager.instance.fadeOut.enabled = true;
		FadeInManager.instance.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (FadeOutDone.isFadeOutDone == true) {
			SoundManager.Intance.MuteSoundBG ();
			SoundManager.Intance.StopSoundGamePlay ();
			SoundManager.Intance.StopSpaceship ();
			UnityEngine.SceneManagement.SceneManager.LoadScene (nameScene);
			FadeOutDone.isFadeOutDone = false;
		}
		SaveManager.instance.Save ();
		StartCoroutine (DoneCutscene ());
	}
}
