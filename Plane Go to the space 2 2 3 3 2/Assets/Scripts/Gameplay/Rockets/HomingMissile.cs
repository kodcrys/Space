using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * p - Parameter
 * This cripts is on the GameObjects: Rocket4 Prefabs, Rocket2 Prefabs, Rocket3 Prefabs
 */

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour {

	// 1-p Get the plane transform
	private Transform target;
	// 1-p Rigidbody of the rocket.
	private Rigidbody2D rb;

	// 2-p The speed and rotation of the rocket
	[SerializeField]
	private float speed, rotateSpeed, div;
	[SerializeField]
	private int ExplosionPrefab;

	// Use this for initialization
	void Start () {
		// Get the rigid bory of the rocket
		rb = GetComponent<Rigidbody2D>();
		// Find the plane on the hierarchy
		target = GameObject.FindGameObjectWithTag("Player").transform;

		// speed of rockets always > speed of plane
		speed = PlaneData.speedchoosePlane * 0.5f + 6;
	}

	void OnEnable ()
	{
		if (SpawnerPool.endless)
			speed += Mathf.RoundToInt (transform.position.y / div);
		else 
		{
			if (Mathf.RoundToInt (transform.position.y / (div + 100f)) >= 5f)
				speed += 5f;
			else
				speed += Mathf.RoundToInt (transform.position.y / (div + 100f));
		}
	}

	void FixedUpdate ()
	{
		Flying ();
	}

	/// <summary>
	/// 1.1 Flying the specified target.
	/// </summary>
	/// <param name="temp">Temp.</param>
	void Flying()
	{
		Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;

		point2Target.Normalize ();

		float value = Vector3.Cross (point2Target, transform.up).z;
		rb.angularVelocity = 200 * value;

		rb.velocity = transform.up * speed;

	}

	// 2. When the rocket hit something, it will explosion.
	void OnTriggerEnter2D (Collider2D other)
	{
		var go = other.gameObject;

		if (go.tag == "Player" || go.tag == "Enermy" || go.tag == "Shield") 
		{
			// Set active for the affect here.
			PoolManager.Intance.lstPool [ExplosionPrefab].getindex ();
			PoolManager.Intance.lstPool [ExplosionPrefab].GetPoolObject ().transform.position = transform.position;
			PoolManager.Intance.lstPool [ExplosionPrefab].GetPoolObject ().transform.rotation = transform.rotation;
			PoolManager.Intance.lstPool [ExplosionPrefab].GetPoolObject ().SetActive (true);

			// Hide the rocket on the screen.
			transform.gameObject.SetActive (false);

			SoundManager.Intance.PlayExpSound ();

			if (go.tag == "Player")
				SoundManager.Intance.StopSpaceship ();
		}
	}
}
