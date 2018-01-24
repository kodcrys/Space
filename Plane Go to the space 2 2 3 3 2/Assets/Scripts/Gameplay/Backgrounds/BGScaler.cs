using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScaler : MonoBehaviour {

	// 1-p Get the position of the plane
	[SerializeField]
	private GameObject player, BG;
	// 1-p The number you want to div the position of the plane with it.
	[SerializeField]
	private float div;
	// 1-p alpha of the background color
	float alphaColor = 0;

	// Use this for initialization
	void Start () {
		GGAmob.Intance.isRunx2Money = false;
		// Get the height resolution
		var worldHeight = Camera.main.orthographicSize / 4;
		// Get the width resolution
		var worldWidth = worldHeight * Screen.width / Screen.height;

		// Scale background fit with the resolution
		transform.localScale = new Vector3 (worldWidth, worldHeight, 0f);

		// Inscrease the alpha for the background when the plane go highly. 
		StartCoroutine (changeColor ());
	}

	/// 1.
	/// <summary>
	/// Changes the color.
	/// </summary>
	/// <returns>The color.</returns>
	IEnumerator changeColor()
	{
		// The plane go up, the alpha change. 
		if (player.transform.position.y >= 0)
			alphaColor = player.transform.position.y / div;
		BG.transform.GetComponent<Image> ().color = new Color (0f, 0f, 0f, alphaColor);

		yield return new WaitForSeconds (0.01f);

		// if the alpha = 1, stop run this code any more.
		if (alphaColor <= 1f)
			StartCoroutine (changeColor ());
	}
}
