using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour {

	// Player
	[SerializeField]
	private Transform[] Planes;

	// List of the distance of levels
	[SerializeField]
	float[] ListRangeLv;

	// List of planets
	[SerializeField]
	Sprite[] Planets;

	[SerializeField]
	float speed;

	[SerializeField] 
	Transform target;

	[SerializeField]
	GameObject panelWin, spawner;
	int currentPlane, currentLv;

	[SerializeField]
	private GameObject BG, PlaneRotate;

	[SerializeField]
	private GameObject startTouch;

	float temp;
	bool isPlaySoundOnce;
	// Use this for initialization
	void Start () {
		startTouch.SetActive (true);
		currentPlane = PlaneData.index;
		currentLv = PlayerPrefs.GetInt ("Level", 0);
		temp = 0;
		isPlaySoundOnce = false;
		BG.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Planes [currentPlane].position.y >= ListRangeLv [currentLv]) 
		{
			BG.SetActive (true);
			Planes [currentPlane].GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			Planes [currentPlane].GetComponent<Rigidbody2D> ().gravityScale = 0f;
				transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);

			startTouch.SetActive (false);

				spawner.SetActive (false);

				var Enermytarget = GameObject.FindGameObjectsWithTag("Enermy");
				foreach (var i in Enermytarget)
				{
					// Set active for the affect here.
					PoolManager.Intance.lstPool [9].getindex ();
					PoolManager.Intance.lstPool [9].GetPoolObject ().transform.position = i.transform.position;
					PoolManager.Intance.lstPool [9].GetPoolObject ().transform.rotation = i.transform.rotation;
					PoolManager.Intance.lstPool [9].GetPoolObject ().SetActive (true);

					i.transform.gameObject.SetActive(false);
				}
			if (transform.position == target.position) 
			{
				temp += Time.deltaTime;
				if (temp <= 1f)
					BG.transform.GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, temp);
				else 
				{
					panelWin.SetActive (true);
					if (isPlaySoundOnce == false) {
						SoundManager.Intance.PlayWinGame ();
						isPlaySoundOnce = true;
					}
					SoundManager.Intance.MuteSoundCutScene1BG ();
					SoundManager.Intance.MuteSoundCutScene2BG ();
					SoundManager.Intance.StopSoundGamePlay ();
					SoundManager.Intance.StopSpaceship ();
					PlaneRotate.SetActive (true);
				}
			}
		}
	}
}
