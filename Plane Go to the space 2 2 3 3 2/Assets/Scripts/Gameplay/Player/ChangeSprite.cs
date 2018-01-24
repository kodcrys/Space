using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour {

	[SerializeField]
	private Sprite[] listSpritePlanes;
	// Use this for initialization
	void Start () {
		int currentPlane = PlaneData.index;
		transform.GetComponent<SpriteRenderer> ().sprite = listSpritePlanes [currentPlane];
	}

}
