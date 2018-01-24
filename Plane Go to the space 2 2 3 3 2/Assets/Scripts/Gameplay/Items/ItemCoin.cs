using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour {

	[SerializeField]
	private GameObject magnet;
	[SerializeField]
	private float Speed;

	void Start ()
	{
		magnet = GameObject.Find("/Player/MagnetEffect");
		Speed = PlaneData.speedchoosePlane * 2 + 10f;
	}

	void Update ()
	{
		if (transform.tag == "Bitcoin")
			transform.Rotate (new Vector3 (0, Time.deltaTime * Speed * 100, 0));
		if (magnet != null)
		if (magnet.activeInHierarchy) 
		{
			var direction = (transform.position - PlayerController.Instance.transform.position).normalized;
			var rotation = Quaternion.LookRotation(direction, Vector3.forward);
			rotation.x = 0;
			rotation.y = 0;
			transform.rotation = rotation;
			transform.GetComponent<Rigidbody2D>().velocity = Speed * transform.up;
		}
	}
}
