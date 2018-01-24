using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollowObject : MonoBehaviour {

	[HideInInspector]
	public Transform objectFollow;

	[SerializeField]
	LoadDataUpgradeScene loadDataUpgradeScene;

	public static LightFollowObject Intance;

	void Awake() {
		if (Intance == null)
			Intance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (loadDataUpgradeScene.isPlayGamePlane && objectFollow != null) {
			transform.position = objectFollow.position;
		}
	}
}
