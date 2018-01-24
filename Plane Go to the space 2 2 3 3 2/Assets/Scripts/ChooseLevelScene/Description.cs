using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class Description : MonoBehaviour {

	[SerializeField]
	HorizontalScrollSnap horizontalScrollSnapPlanet;

	[SerializeField]
	UnityEngine.UI.Text nameTxt, contentTxt;

	[SerializeField]
	string[] nameText, contentText;

	[SerializeField]
	UIAnimation nameAni, contentAni;

	string levelScene;

	[SerializeField]
	string commingsoon;

	[SerializeField]
	GameObject scroll;

	void Start(){
		if (MultiResolution.device == "iphonex")
			scroll.GetComponent<RectTransform> ().localScale = new Vector3 (0.85f, 0.85f, 1);
		else
			scroll.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1);
	}

	void Update(){
		ClickChooseLevel (levelScene);
	}

	public void StartChange() {
		nameAni.ani.isChangeAni = false;
		contentAni.ani.isChangeAni = false;
	}

	public void EndChange(){
		if (contentTxt.text == commingsoon)
			contentTxt.fontSize = 100;
		else
			contentTxt.fontSize = 60;

		nameTxt.text = nameText [horizontalScrollSnapPlanet.CurrentPage];
		contentTxt.text = contentText [horizontalScrollSnapPlanet.CurrentPage];

		nameAni.ani.isChangeAni = true;
		contentAni.ani.isChangeAni = true;
	}

	public void GotoGameScene(string nameScene){
		FadeInManager.instance.fadeOut.enabled = true;
		FadeInManager.instance.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		SoundManager.Intance.PlayClickBtn ();
		levelScene = nameScene;
	}

	void ClickChooseLevel(string nameScene){
		if (FadeOutDone.isFadeOutDone == true) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (nameScene);
			FadeOutDone.isFadeOutDone = false;
		}
	}

	public void BackScene(string nameSceneBack){
		SoundManager.Intance.PlayClickBtn ();
		UIManager.Instance.BackHome (nameSceneBack);
	}
}
