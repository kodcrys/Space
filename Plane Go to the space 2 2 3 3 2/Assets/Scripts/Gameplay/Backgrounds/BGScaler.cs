using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScaler : MonoBehaviour {

	// Use this for initialization
	void Update () {
		// Get the height resolution
		var worldHeight = Camera.main.orthographicSize * 6;
		// Get the width resolution
		var worldWidth = worldHeight * Screen.width / Screen.height;

		// Scale background fit with the resolution
		transform.localScale = new Vector3 (worldWidth, worldHeight, 0f);
		Vector3 temp = Camera.main.transform.position;
		temp.z = 0f;
		transform.position = temp;
		transform.rotation = Camera.main.transform.rotation;
	}
}
