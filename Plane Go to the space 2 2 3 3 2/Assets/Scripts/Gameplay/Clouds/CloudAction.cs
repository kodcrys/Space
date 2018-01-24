using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * p - Parameter
 * This cripts is on the GameObjects: Cloud Prefabs
 */

public class CloudAction : MonoBehaviour {
	// 1-p The speed of cloud
	[SerializeField] 
	private float cloudSpeed;

	Rigidbody2D rb;

	// 2-p The rate, if the cloud pass over it, cloud will disappear
	[SerializeField]
	private float Rate;


	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update ()
	{
		// 1. The cloud will fly away to the left
		MoveCloud ();

		// 2. The cloud will fly with the plane, until it go far away the plane.
		HideCloud ();
	}

	/// 1.1
	/// <summary>
	/// The cloud moves.
	/// </summary>
	void MoveCloud()
	{
		// Move the cloud to left
		//transform.position -= Vector3.right * Time.deltaTime * cloudSpeed;
		rb.velocity = -Vector3.right * cloudSpeed;
	}

	/// 2.1
	/// <summary>
	/// Hides the cloud.
	/// </summary>
	void HideCloud()
	{
		// Get the max heigt and width of the camera
		Vector3 sizeCamera = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		// Get the position of the camera
		Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width/2, Screen.height/2, 0));

		// The size between max heigh and current position
		Vector3 tempCam = sizeCamera - cam;

		// The cloud fly over the left side or the top side or the down side, the cloud will disappear
		if (transform.position.x <= (cam.x - tempCam.x - Rate) || transform.position.y >= (sizeCamera.y + Rate) || transform.position.y <= (cam.y - tempCam.y - Rate))
			gameObject.SetActive (false);
	}
}
