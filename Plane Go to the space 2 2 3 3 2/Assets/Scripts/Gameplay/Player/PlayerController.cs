using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using AppAdvisory.social;
/*
 * p - Parameter
 * This script is on the GameObject: 
 */

// List items for gameplay
//public enum Items { none, shield, shockwave, speedforce }

public class PlayerController : BaseCharacter {

	[Header ("Character")]
	// The bar show the curren fuel
	public UnityEngine.UI.Slider sliderEnergy;

	// The fuel of the plane.
	public float Fuel;

	// 1-p Rigid body of the plane
	Rigidbody2D rigid;
	// 1-p Current fuel of the plane
	float currentFuel;
	// 1-p Check when you touch on the screen.
	bool touchOnScreen = false;
	// 1-p The angle of the plane
	float angle;
	float time;
	bool dead;
	[SerializeField]
	private int numberExplosion;

	//public Items activeItem;
	[SerializeField]
	private GameObject Shield, Effects, Magnet, ShockwareEffect, spawner, poolManager, GameOver, RevivePanel;
	bool revive;
	string currentPlane;

	[SerializeField]
	private UnityEngine.UI.Text CountdownText;

	// The nitro bar when the plane get the nitro item.
	[SerializeField]
	private UnityEngine.UI.Slider sliderNitro;
	bool enableNitroBar;
	float timeNitro, currentNitro;

	// Coin you get
	public static int coin;

	// Game Over
	[SerializeField]
	private UnityEngine.UI.Text highScore, score;

	float timeWaitWarningFuel;
	UnityEngine.UI.Image fillFuel;
	[SerializeField]
	Sprite normalFuel, warningFuel;
	[SerializeField]
	GameObject warningSprite;

	// Time life
	[SerializeField]
	Text scoreTimeliveTxt;
	public static float timelive;

	[SerializeField]
	private int[] ListLevel;
	int currentLv;

	[Header("Upgrade spaceship")]
	[SerializeField]
	int indexSpaceship;
	[SerializeField]
	GameObject[] upgrade;
	[SerializeField]
	GameObject[] effectUpgrade;

	private Vector3 zAxis;

	string levelScene;

	float StartSpeed, timeCountdownDead, timeSpeedforce;

	bool checkGameOver, useSpeedforce;

	VitualJoyStick joystick;

	[SerializeField]
	private GameObject joystickObj;

	// Make the instance for the player
	private static PlayerController _player;
	public static PlayerController Instance
	{
		get
		{
			if (_player == null)
				_player = GameObject.FindObjectOfType<PlayerController>();
			return _player;
		}
	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		// Get the rigid body to move the character
		rigid = transform.GetComponent<Rigidbody2D> (); 
	}

	// Use this for initialization
	void Start () {
		// 1. When start the fuel of the plane is full.
		Fuel = PlaneData.fuelchoosePlane * 2 + 7f;
		Speed = PlaneData.speedchoosePlane * 0.5f + 5f;
		Rotation = PlaneData.rotationchoosePlane + 3;
		coin = 0;
		StartSpeed = Speed;
		currentFuel = Fuel;
		isUseItem = true;
		time = 3f;

		revive = false;
		dead = false;

		// time live
		timelive = 0f;

		timeWaitWarningFuel = 0;
		warningSprite.SetActive (false);
		fillFuel = sliderEnergy.transform.GetChild (1).GetChild (0).gameObject.GetComponent<UnityEngine.UI.Image> ();

		if (SpawnerPool.endless == false)
			currentLv = PlayerPrefs.GetInt ("Level", 0);

		SoundManager.Intance.PlaySpaceship ();

		zAxis = new Vector3(0, 0, 1);
		checkGameOver = false;
		timeCountdownDead = 1f;

		timeSpeedforce = 3f;
		useSpeedforce = false;
		//touchOnScreen = true;
		//isUseItem = false;

		ReadUpgrade ();

		StartCoroutine (Countdown());
	}	
		
	// Update is called once per frame
	void Update () {

		if (joystickObj.activeSelf == true)
		{
			joystick = GameObject.Find ("VirtualJoyStickContainerMove").GetComponent<VitualJoyStick> ();
		}

		if (time <= 0) 
		{
			if (!dead)
			// The fuel of the plane will go down every second 
			currentFuel -= Time.deltaTime * 0.5f;
		}
			
		if (!SpawnerPool.endless) 
		{
			if (transform.position.y >= ListLevel [currentLv])
				dead = true;
		}

		if (dead)
			CameraShake.shakeCamera = true;

			Move ();
		if (enableNitroBar)
			NitroBar ();

		LoadSceneActive (levelScene);
		if (useSpeedforce)
			ActiveSpeedforce ();
		if (dead && !checkGameOver) 
		{
			timeCountdownDead -= Time.deltaTime;
			if (timeCountdownDead <= 0)
				GameOverDelay ();
		}
	}

	/// 1.3
	/// <summary>
	/// Move this instance.
	/// </summary>
	public override void Move()
	{
		// Call the function PlayerMove, it will help the plane follow the the finger.
		if (!isUseItem || VitualJoyStick.useJoyStick)
			PlayerMove ();
	
		if (!dead) 
		{
			// Di chuyen may bay
			rigid.velocity = transform.up * Speed;
			rigid.gravityScale = 0.2f;
		}
		else 
		{
			rigid.gravityScale = 0f;
			rigid.velocity = Vector2.zero;
		}


		if (currentFuel > 0 && !dead) 
		{
			// time live go up
			timelive = transform.position.y;
			scoreTimeliveTxt.text = Mathf.RoundToInt (timelive).ToString ();
		}

		if (currentFuel < 0)
		{
			angle = 270;
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (Vector3.forward * (angle - 90)), Rotation * Time.deltaTime * 0.5f);
			Effects.SetActive (false);
			rigid.velocity = -Vector2.up * Speed * Time.deltaTime * 6;

			SoundManager.Intance.StopSpaceship ();
		}
		// Show the energy on the energy bar
		sliderEnergy.value = currentFuel / Fuel;  
		if (sliderEnergy.value <= 0.15f && !dead) {
			timeWaitWarningFuel += Time.deltaTime;
			if (timeWaitWarningFuel < 0.25f && timeWaitWarningFuel > 0) {
				fillFuel.sprite = normalFuel;
				warningSprite.SetActive (true);
			}
			if (timeWaitWarningFuel >= 0.25f && timeWaitWarningFuel < 0.5f) {
				fillFuel.sprite = warningFuel;
				warningSprite.SetActive (false);
			}
			if (timeWaitWarningFuel >= 0.5f) {
				timeWaitWarningFuel = 0;
				fillFuel.sprite = normalFuel;
				warningSprite.SetActive (false);
			}
		} else {
			timeWaitWarningFuel = 0;
			fillFuel.sprite = normalFuel;
			warningSprite.SetActive (false);
		}
	}
		
	public void LoaddingScene(string sceneName){
		FadeInManager.instance.fadeOut.enabled = true;
		FadeInManager.instance.fadeOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		levelScene = sceneName;
	}

	void LoadSceneActive(string sceneName){
		if (FadeOutDone.isFadeOutDone == true) {
			if (transform.gameObject.activeSelf == true) {
				UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName);
				if (sceneName == "Cutscene1") {
					CutsceneManager.whatScene = 1;
				}
			}
			FadeOutDone.isFadeOutDone = false;
		}
	}
		
	public void StartTouch(){
		if (transform.gameObject.activeSelf == true) {
			isUseItem = false;
			// 1.1 If you never touch on the screen, the plane will auto go up. 
			touchOnScreen = true;
			//joystickObj.SetActive (true);
		}

	}

	public void StartTouchUp()
	{
		touchOnScreen = false;
	}

	/// 1.4
	/// <summary>
	/// Players move.
	/// </summary>
	private void PlayerMove()
	{
		// If the fuel still have, the plane still fly.
		if (currentFuel >= 0) 
		{
			// Get the mouse position
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 0f;

			// Get the plane postion
			Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);

			// The vector between mouse and plane position
			mousePos.x = mousePos.x - objectPos.x;
			mousePos.y = mousePos.y - objectPos.y;
			// the plane will follow the mouse position 

			if (VitualJoyStick.useJoyStick) {
				angle = Mathf.Atan2 (-joystick.Horizontal (), joystick.Vertical ()) * Mathf.Rad2Deg;
			} else if (touchOnScreen) {
				angle = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;
			} 
			
			// Rotate of the plane, get the rotation of each plane.
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (Vector3.forward * (angle)), Rotation * Time.deltaTime);
			/*
				Note: Nen dung dong bo ca rocket zs ten lua cung 1 cach di chuyen
			*/
		} 
	}

	public bool isUseItem;

	/*
	 *  hàm xác nhận sự kiện active cái shield để bảo vệ plane.
	 */
	/// <summary>
	/// Pickups the shield button.
	/// </summary>
	public void PickupShield (Button btn)
	{
		if (transform.gameObject.activeSelf == true)
		if (btn.interactable) {
			Shield.SetActive (true);
			PlayerPrefs.SetInt ("statusItem" + 2, 0);
			isUseItem = true;
			StartCoroutine (ActiveShield (5f));
		}
	}
		
	/// <summary>
	/// Unpick the shield button.
	/// </summary>
	/// <param name="btn">Button.</param>
	public void UnPickItem (Button btn){
		if (transform.gameObject.activeSelf == true)
		if (btn.interactable) {
			btn.interactable = false;
			StartCoroutine (wait (0.2f));
		}
	}

	/// <summary>
	/// Wait the specified time.
	/// </summary>
	/// <param name="time">Time.</param>
	IEnumerator wait(float time){
		yield return new WaitForSeconds (time);
		isUseItem = false;
		ShockwareEffect.SetActive (false);
	}

	/*
	 * gọi hàm active cái shield để bảo vệ plane.
	 */
	// wait time to deactive the shield
	public IEnumerator ActiveShield(float timeShieldActive)
	{
		yield return new WaitForSeconds(timeShieldActive);
		Shield.SetActive (false);
	}

	/// <summary>
	/// Picks the shockware button.
	/// </summary>
	/// <param name="btn">Button.</param>
	public void PickShockware (Button btn)
	{
		if (transform.gameObject.activeSelf == true)
		if (btn.interactable) 
		{
			isUseItem = true;
			ShockwareEffect.SetActive (true);
			PlayerPrefs.SetInt ("statusItem" + 0, 0);
			ActiveShockware ();
		}
	}

	/// <summary>
	/// Unpick the shockware button.
	/// </summary>
	/// <param name="btn">Button.</param>
	public void UnPickShockware (Button btn){
		if (transform.gameObject.activeSelf == true)
		if (btn.interactable) {
			btn.interactable = false;
			StartCoroutine (wait (0.2f));
		}
	}

	/// <summary>
	/// Actives the shockware.
	/// </summary>
	private void ActiveShockware()
	{
		var target = GameObject.FindGameObjectsWithTag("Enermy");
		foreach (var i in target)
		{
			// Set active for the affect here.
			PoolManager.Intance.lstPool [numberExplosion].getindex ();
			PoolManager.Intance.lstPool [numberExplosion].GetPoolObject ().transform.position = i.transform.position;
			PoolManager.Intance.lstPool [numberExplosion].GetPoolObject ().transform.rotation = i.transform.rotation;
			PoolManager.Intance.lstPool [numberExplosion].GetPoolObject ().SetActive (true);

			i.transform.gameObject.SetActive(false);
		}
	}

	/*
	 * Hàm sử lý sự kiện click để active speedforce trong 1 khoảng thời gian nhất định
	 */
	/// <summary>
	/// Picks the speedforce.
	/// </summary>
	/// <param name="btn">Button.</param>
	public void PickSpeedforce (Button btn)
	{
		if (transform.gameObject.activeSelf == true)
		if (btn.interactable) {
			isUseItem = true;
			useSpeedforce = true;
			PlayerPrefs.SetInt ("statusItem" + 1, 0);
		}
	}

	/// <summary>
	/// Unpick speedforce.
	/// </summary>
	/// <param name="btn">Button.</param>
	public void UnPickSpeedforce (Button btn){
		if (transform.gameObject.activeSelf == true)
		if (btn.interactable) {
			btn.interactable = false;
			StartCoroutine (wait (0.2f));
		}
	}

	/*
	 * Hàm để active speedforce trong 1 khoảng thời gian nhất định
	 */
	// Wait time to use speedforce
	public void ActiveSpeedforce()
	{
		Speed = StartSpeed * 2;
		Debug.Log (timeSpeedforce);
		timeSpeedforce -= Time.deltaTime;
		if (timeSpeedforce <= 0) 
		{
			Speed = StartSpeed;
			useSpeedforce = false;
		}
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		var go = other.gameObject;
		switch (go.tag) 
		{
		case "ItemFuel":
			SoundManager.Intance.PlayEatItem ();
			PickupItemFuel ();
			go.SetActive (false);
			break;

			/*
			 * Khi máy bay ăn được item nitro rớt xuống sẽ được xử lý ở dưới đây 
	 		 */
			case "ItemNitro":
			SoundManager.Intance.PlayEatItem ();
			timeNitro = 1.5f;
			currentNitro = timeNitro;
			sliderNitro.gameObject.SetActive (true);
			enableNitroBar = true;
			go.SetActive (false);
			// cộng thêm xăng cho plane.
			currentFuel += 2f;
			if (currentFuel > Fuel)
				currentFuel = Fuel;
			break;

			/*
			 * Khi máy bay ăn được item nam châm rớt xuống sẽ được xử lý ở dưới đây 
	 		 */
			case "ItemMagnet":
			SoundManager.Intance.PlayEatItem ();
			Magnet.SetActive (true);
			StartCoroutine (PickupItemMagnet (3f));
			go.SetActive (false);
			break;

		case "Bitcoin":
			SoundManager.Intance.PlayEatItem ();
			coin++;
			go.SetActive (false);
			break;

		case "Enermy":
			// Set active for the affect here.
			PoolManager.Intance.lstPool [9].getindex ();
			PoolManager.Intance.lstPool [9].GetPoolObject ().transform.position = transform.position;
			PoolManager.Intance.lstPool [9].GetPoolObject ().SetActive (true);
			Handheld.Vibrate();
			transform.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			dead = true;
			break;
		}
	}

	void PickupItemFuel ()
	{
		currentFuel += 2.5f;
		if (currentFuel > Fuel)
			currentFuel = Fuel;
	}

	// Hàm để cho thanh nitro giảm dần theo thời gian mỗi lần xuất hiện, khi thanh nitro mất thì tốc độ trả về bình thường. 
	void NitroBar()
	{
		currentNitro -= Time.deltaTime;
		sliderNitro.value = currentNitro / timeNitro;
		Speed = StartSpeed * 1.5f;
		if (currentNitro <= 0)
		{
			Speed = StartSpeed;
			enableNitroBar = false;
			sliderNitro.gameObject.SetActive (false);
		}
	}

	/*
	 * Hàm để active item magnet trong 1 khoảng thời gian nhất định
	 */
	public IEnumerator PickupItemMagnet (float timeMagnetActive)
	{
		yield return new WaitForSeconds (timeMagnetActive);
		Magnet.SetActive (false);
	}

	IEnumerator Countdown()
	{
		yield return new WaitForSeconds (1f);
		time--;
		CountdownText.text = time.ToString ();
		if (time > 0)
			StartCoroutine (Countdown ());
		else 
		{
			CountdownText.text = "";
			spawner.SetActive (true);
			poolManager.SetActive (true);
			checkGameOver = false;
			timeCountdownDead = 1f;
		}
	}

	// Hàm để khi mà máy bay đụng phải rocket thì sẽ cho delay 1s rồi mới hiện lên revive hoặc gameover panel.
	void GameOverDelay()
	{
		checkGameOver = true;
		spawner.SetActive (false);
		poolManager.SetActive (false);
		// Kiểm tra mạng có hay không? nếu không thì cho hiện game over luôn.
		if (Application.internetReachability != NetworkReachability.NotReachable) 
		{
			// Nếu như chết lần đầu tiên thì sẽ hiện panel revive 
			if (!revive) 
			{
				RevivePanel.SetActive (true);
			} 
			else 
			{
				// không thì hiện panel game over và cộng tiền vào tài khoản.
				SaveManager.instance.state.money += coin;
				SaveManager.instance.Save ();
				// hiện quảng cáo nếu như đủ 3 lần. 
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
				// cho tắt cái revive panel và hiện cái game over panel.
				RevivePanel.SetActive (false);
				GameOver.SetActive (true);

				SoundManager.Intance.MuteSoundBG ();
				SoundManager.Intance.StopSoundGamePlay ();
				SoundManager.Intance.StopSpaceship ();

				SoundManager.Intance.PlayLoseGame ();

				int highscore;
				// cho hiện điểm của màn 
				if (SpawnerPool.endless) 
				{
					highscore = PlayerPrefs.GetInt ("highscoreEndless", 0);
					if (Mathf.RoundToInt (timelive) > highscore)
						PlayerPrefs.SetInt ("highscoreEndless", Mathf.RoundToInt (timelive));
					score.text = "SCORE: " + Mathf.RoundToInt (timelive).ToString ();
					highScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt ("highscoreEndless", 0).ToString ();
					LeaderboardManager.ReportScore (LoadScene.leaderboardIdEndlessStr, highscore);
				}
				else
				{
					highscore = PlayerPrefs.GetInt ("highscoreLevel", 0);
					if (Mathf.RoundToInt (timelive) > highscore)
						PlayerPrefs.SetInt ("highscoreLevel", Mathf.RoundToInt (timelive));
					score.text = "SCORE: " + Mathf.RoundToInt (timelive).ToString ();
					highScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt ("highscoreLevel", 0).ToString ();
					LeaderboardManager.ReportScore (LoadScene.leaderboardIdLevelStr, highscore);
				}
			}
		} 
		else 
		{
			// gửi tiền đã ăn được vào tài khoản.
			SaveManager.instance.state.money += coin;
			SaveManager.instance.Save ();

			// cho tắt cái revive panel và hiện cái game over panel.
			RevivePanel.SetActive (false);
			GameOver.SetActive (true);

			SoundManager.Intance.MuteSoundBG ();
			SoundManager.Intance.StopSoundGamePlay ();
			SoundManager.Intance.StopSpaceship ();

			SoundManager.Intance.PlayLoseGame ();

			// cho hiện điểm của màn 
			int highscore;
			// cho hiện điểm của màn 
			if (SpawnerPool.endless) 
			{
				highscore = PlayerPrefs.GetInt ("highscoreEndless", 0);
				if (Mathf.RoundToInt (timelive) > highscore)
					PlayerPrefs.SetInt ("highscoreEndless", Mathf.RoundToInt (timelive));
				score.text = "SCORE: " + Mathf.RoundToInt (timelive).ToString ();
				highScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt ("highscoreEndless", 0).ToString ();
			}
			else
			{
				highscore = PlayerPrefs.GetInt ("highscoreLevel", 0);
				if (Mathf.RoundToInt (timelive) > highscore)
					PlayerPrefs.SetInt ("highscoreLevel", Mathf.RoundToInt (timelive));
				score.text = "SCORE: " + Mathf.RoundToInt (timelive).ToString ();
				highScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt ("highscoreLevel", 0).ToString ();
				LeaderboardManager.ReportScore (highscore);
			}

		}
	}

	/*
	 * Hàm xử lý button revive máy bay sau khi chết. 
	 */
	public void Revive()
	{
		if (transform.gameObject.activeSelf == true)
		{
			GGAmob.Intance.ShowRewardBasedVideo ();
			// tắt revive panel 
			RevivePanel.SetActive (false);
			// bật lại effect 
			Effects.SetActive (true);
			revive = true;
			dead = false;
			// hồi phục lại full xăng.
			currentFuel = Fuel;
			// bật lại sprite renderer cho nhân vật
			transform.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			// bật lại hàm countdown để chạy lại game.
			time = 3f;
			CameraShake.shakeCamera = false;
			CountdownText.text = time.ToString ();
			StartCoroutine (Countdown ());
			angle = 0f;
		}
	}

	void ReadUpgrade() {
		if (indexSpaceship == 0) {
			Debug.Log (SaveManager.instance.state.indexEvolWater);
			if (SaveManager.instance.state.indexEvolWater <= 0) {
				for (int i = 0; i < upgrade.Length; i++)
					upgrade [i].SetActive (false);
			} else {
				for (int i = 0; i < upgrade.Length; i++)
					if (i == SaveManager.instance.state.indexEvolWater - 1)
						upgrade [i].SetActive (true);
					else
						upgrade [i].SetActive (false);

				for (int i = 0; i < upgrade.Length; i++)
					effectUpgrade [i].SetActive (false);
				effectUpgrade [SaveManager.instance.state.indexEvolWater].SetActive (true);
			}
		}
		if (indexSpaceship == 1) {
			if (SaveManager.instance.state.indexEvolFire <= 0) {
				for (int i = 0; i < upgrade.Length; i++)
					upgrade [i].SetActive (false);
			} else {
				for (int i = 0; i < upgrade.Length; i++)
					if (i == SaveManager.instance.state.indexEvolFire - 1)
						upgrade [i].SetActive (true);
					else
						upgrade [i].SetActive (false);

				for (int i = 0; i < upgrade.Length; i++)
					effectUpgrade [i].SetActive (false);
				effectUpgrade [SaveManager.instance.state.indexEvolFire].SetActive (true);
			}
		}
		if (indexSpaceship == 2) {
			if (SaveManager.instance.state.indexEvolWood <= 0) {
				for (int i = 0; i < upgrade.Length; i++)
					upgrade [i].SetActive (false);
			} else {
				for (int i = 0; i < upgrade.Length; i++)
					if (i == SaveManager.instance.state.indexEvolWood - 1)
						upgrade [i].SetActive (true);
					else
						upgrade [i].SetActive (false);

				for (int i = 0; i < upgrade.Length; i++)
					effectUpgrade [i].SetActive (false);
				effectUpgrade [SaveManager.instance.state.indexEvolWood].SetActive (true);
			}
		}
		if (indexSpaceship == 3) {
			if (SaveManager.instance.state.indexEvolEarth <= 0) {
				for (int i = 0; i < upgrade.Length; i++)
					upgrade [i].SetActive (false);
			} else {
				for (int i = 0; i < upgrade.Length; i++)
					if (i == SaveManager.instance.state.indexEvolEarth - 1)
						upgrade [i].SetActive (true);
					else
						upgrade [i].SetActive (false);
				
				for (int i = 0; i < upgrade.Length; i++)
					effectUpgrade [i].SetActive (false);
				effectUpgrade [SaveManager.instance.state.indexEvolEarth].SetActive (true);
			}
		}
		if (indexSpaceship == 4) {
			if (SaveManager.instance.state.indexEvolMetal <= 0) {
				for (int i = 0; i < upgrade.Length; i++)
					upgrade [i].SetActive (false);
			} else {
				for (int i = 0; i < upgrade.Length; i++)
					if (i == SaveManager.instance.state.indexEvolMetal - 1)
						upgrade [i].SetActive (true);
					else
						upgrade [i].SetActive (false);
				
				for (int i = 0; i < upgrade.Length; i++)
					effectUpgrade [i].SetActive (false);
				effectUpgrade [SaveManager.instance.state.indexEvolMetal].SetActive (true);
			}
		}
	}
}
