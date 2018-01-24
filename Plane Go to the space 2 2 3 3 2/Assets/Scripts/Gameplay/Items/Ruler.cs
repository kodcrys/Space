using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler : MonoBehaviour {

	// Player
	[SerializeField]
	private Transform[] plane;

	// List of the distance of levels
	[SerializeField]
	private float[] ListRangeLv;

	[SerializeField]
	private float maxSizeRuler = 4f;

	[SerializeField] 
	private Transform point;

	Vector3 startPos, temp, cam;
	int currentPlane, currentLv;

	//[SerializeField]
	//Vector3 posIphone, posIpad, posIphoneX;

	[SerializeField]
	Vector3 offset;

	[SerializeField]
	GameObject des;

	Rigidbody2D rigid;
	// Use this for initialization
	void Start () {
		rigid = transform.GetComponent<Rigidbody2D> ();
		currentPlane = PlaneData.index;
		currentLv = PlayerPrefs.GetInt ("Level", 0);
		cam = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width /2, Screen.height, 0));
		cam.x = cam.x - Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0)).x;
		cam = cam + offset; // new Vector3 (2.7f, -2f, 0);
		cam.z = 0f;
		transform.position = cam;
		/*Debug.Log (MultiResolution.device);
		if (MultiResolution.device == "iphonex")
			transform.position = posIphoneX;
		else if (MultiResolution.device == "ipad")
			transform.position = posIpad;
		else 
			transform.position = posIphone;*/
	
		startPos = point.position;
	}
	
	// Update is called once per frame
	void Update () {
		ShowRuler ();
	}
	void ShowRuler()
	{
		if (plane [currentPlane].transform.position.y <= ListRangeLv [currentLv]) 
		{
			startPos.y = point.position.y;
		
			Vector3 t = transform.position;
			t.y = plane [currentPlane].transform.position.y + cam.y;
			t.x = plane [currentPlane].transform.position.x + cam.x;
			transform.position = t;
			//rigid.velocity = transform.up * plane [currentPlane].GetComponent<PlayerController> ().Speed;
			float percentGo = (plane [currentPlane].transform.position.y / ListRangeLv [currentLv]) * maxSizeRuler;
			float percent = (plane [currentPlane].transform.position.y / ListRangeLv [currentLv]);
			temp = startPos;
			temp.x = startPos.x + plane [currentPlane].transform.position.x  + percentGo;
			temp.y = plane [currentPlane].transform.position.y + cam.y;
			point.position = Vector3.MoveTowards (point.position, temp, Time.deltaTime);

			if (percent >= 0.98f)
				transform.gameObject.SetActive (false);
		} 
		//else
		//	transform.gameObject.SetActive (false);
	}
}
