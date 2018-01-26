using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppAdvisory.social;

public class Revive : MonoBehaviour {
	float time;

	[SerializeField]
	private UnityEngine.UI.Text CountdownText, Score, HighScore;
	[SerializeField]
	private GameObject GameOver;
	// Use this for initialization
	void OnEnable () {
		time = 5f;
		StartCoroutine (Countdown ());
	}

	IEnumerator Countdown()
	{
		// đếm thời gian cứ mỗi giây sẽ trả về giá trị text
		yield return new WaitForSeconds (1f);
		time--;
		CountdownText.text = time.ToString ();

		// nếu giá trị time vẫn còn thời gian đếm thì vẫn cứ tiếp tục đếm để bấm hồi , nếu hết thời gian thì sẽ hiện panel game over.
		if (time >= 0)
			StartCoroutine (Countdown ());
		else 
		{
			// khi mà game over thì save lại coin kiếm được.
			SaveManager.instance.state.money += PlayerController.coin;
			SaveManager.instance.Save ();

			// Nếu như không remove ad thì sẽ chạy interstitial mỗi 3 lần chết.
			if (SaveManager.instance.state.isPurchaseRemoveAds == false) 
			{
				LoadScene.countDead++;
				if (LoadScene.countDead >= 3) 
				{
					GGAmob.Intance.ShowInterstitial ();
					GGAmob.Intance.RequestInterstitial ();
					LoadScene.countDead = 0;		
				}
			}

			// Lấy điểm của mỗi loại chơi, gồm có endless và level.
			int highscore;
			if (SpawnerPool.endless) 
			{
				highscore = PlayerPrefs.GetInt ("highscoreEndless", 0);
				if (Mathf.RoundToInt (PlayerController.timelive) > highscore)
					PlayerPrefs.SetInt ("highscoreEndless", Mathf.RoundToInt (PlayerController.timelive));
				Score.text = "SCORE: " + Mathf.RoundToInt (PlayerController.timelive).ToString ();
				HighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt ("highscoreEndless", 0).ToString ();
				LeaderboardManager.ReportScore (LoadScene.leaderboardIdEndlessStr, highscore);
			} 
			else 
			{
				highscore = PlayerPrefs.GetInt ("highscoreLevel", 0);
				if (Mathf.RoundToInt (PlayerController.timelive) > highscore)
					PlayerPrefs.SetInt ("highscoreLevel", Mathf.RoundToInt (PlayerController.timelive));
				Score.text = "SCORE: " + Mathf.RoundToInt (PlayerController.timelive).ToString ();
				HighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt ("highscoreLevel", 0).ToString ();
				LeaderboardManager.ReportScore (LoadScene.leaderboardIdLevelStr, highscore);
			}

			// Nếu như time trở về 0 thì sẽ tắt cái text đi và cho hiện cái panel game over.
			CountdownText.text = "";
			GameOver.SetActive (true);

			// Tắt âm thanh khi game over.
			SoundManager.Intance.MuteSoundCutScene1BG ();
			SoundManager.Intance.MuteSoundCutScene2BG ();
			SoundManager.Intance.StopSoundGamePlay ();
			SoundManager.Intance.StopSpaceship ();
			// Tắt panel revive đi.
			transform.gameObject.SetActive (false);
		}
	}
}
