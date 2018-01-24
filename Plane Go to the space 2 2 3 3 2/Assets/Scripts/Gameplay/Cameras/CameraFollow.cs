using UnityEngine;
using System.Collections;

/*
 * p - Parameter
 * This cripts is on the GameObjects: Main Camera
 */


public class CameraFollow : MonoBehaviour {

	// 1-p player: Transform
	private Transform player;

	void Start ()
	{
		SoundManager.Intance.PlaySoundGamePlay ();
		// Find the player
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		// 1. Call the function MoveCamera
		MoveCamera ();
	}
	/// 1.1
	/// <summary>
	/// Moves the camera follow the player.
	/// </summary>
	void MoveCamera()
	{
		// Get the possition of the player
		Vector3 temp = transform.position;
		temp.x = player.position.x;
		temp.y = player.position.y;

		// The camera will move follow the player if the position of camera = player
		transform.position = temp;
	}
}
