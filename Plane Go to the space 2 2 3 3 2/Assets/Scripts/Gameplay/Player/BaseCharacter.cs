using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {
	[Header ("BaseCharacter")]
	// Speed of the planes and rockets
	public float Speed;
	// The rotation of both planes and rockets
	public int Rotation;
	// Check planes and rockets are destroyed
	public bool IsDeath;

	public virtual void Move()
	{
	}

	public virtual void Death()
	{
		IsDeath = true;
	}
}
