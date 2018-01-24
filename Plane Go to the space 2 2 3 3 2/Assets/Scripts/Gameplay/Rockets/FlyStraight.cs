using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyStraight : BaseCharacter {

	void Start ()
	{
		Move ();
	}

	public override void Move()
	{
		StartCoroutine(WaitSecond(0.5f));
	}

	IEnumerator WaitSecond(float time)
	{
		var direction = (transform.position - PlayerController.Instance.transform.position).normalized;
		var rotation = Quaternion.LookRotation(direction, Vector3.forward);
		rotation.x = 0;
		rotation.y = 0;
		transform.rotation = rotation;

		yield return new WaitForSeconds(time);
		transform.GetComponent<Rigidbody2D>().velocity = Speed * transform.up;
	}
}
