using UnityEngine;

public class ChooseModeSceneManager : MonoBehaviour {

	[SerializeField]
	UnityEngine.UI.Button modeAdBtn, modeEndlessBtn, btnClose;

	[SerializeField]
	string nameModeAd, nameModeEndless;

	public static string mode = "";


	int chooseMode = 0;

	// Use this for initialization
	void Start () {

		btnClose.onClick.AddListener (GotoBackScene);

		modeAdBtn.onClick.AddListener (GotoSceneAd);

		modeEndlessBtn.onClick.AddListener (GotoSceneEndless);

		chooseMode = 0;
	}

	void Update(){
		if (chooseMode == 1)
			ClickSceneAd (nameModeAd);

		if (chooseMode == 2)
			ClickSceneEnd (nameModeEndless);

		if(chooseMode == 3)
			BackScene ();
	}

	void GotoSceneAd(){
		SoundManager.Intance.PlayClickBtn ();

		FadeInManager.instance.fadeOut.enabled = true;
		FadeInManager.instance.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		chooseMode = 1;
	}

	void ClickSceneAd(string nameScene){
		if (FadeOutDone.isFadeOutDone == true) {
			UIManager.Instance.BackHome (nameScene);
			mode = "ad";
			FadeOutDone.isFadeOutDone = false;
		}
	}

	void GotoSceneEndless(){
		SoundManager.Intance.PlayClickBtn ();

		FadeInManager.instance.fadeOut.enabled = true;
		FadeInManager.instance.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		chooseMode = 2;
	}

	void ClickSceneEnd(string nameScene){
		if (FadeOutDone.isFadeOutDone == true) {
			UIManager.Instance.BackHome (nameScene);
			mode = "endless";
			FadeOutDone.isFadeOutDone = false;
		}
	}

	void GotoBackScene(){
		SoundManager.Intance.PlayClickBtn ();

		FadeInManager.instance.fadeOut.enabled = true;
		FadeInManager.instance.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		chooseMode = 3;
	}

	public void BackScene(){
		if (FadeOutDone.isFadeOutDone == true) {
			UIManager.Instance.BackHome ("Bitcoin");
			FadeOutDone.isFadeOutDone = false;
		}
	}
}
