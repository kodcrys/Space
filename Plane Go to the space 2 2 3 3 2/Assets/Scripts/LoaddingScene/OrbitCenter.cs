using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gameobject: 1

public class OrbitCenter : MonoBehaviour {

	[SerializeField]
	private float speed, rotate;

	[SerializeField]
	private GameObject target;

	private Vector3 zAxis;

	void Start()
	{
		zAxis = new Vector3(0, 0, rotate );
	}
	void FixedUpdate () {
		transform.RotateAround (target.transform.position, zAxis, speed); 
	}	
}
