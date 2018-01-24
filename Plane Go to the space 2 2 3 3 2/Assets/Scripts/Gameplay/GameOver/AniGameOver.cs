using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniGameOver : MonoBehaviour {

	[SerializeField]
	Transform target;
	[SerializeField] 
	float rotation, speed, scaledown, scaleup;

	int modeScale;
	// Use this for initialization

	void OnEnable()
	{
		modeScale = 0;
	}

	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime * 2;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		if (transform.position == target.position) 
		{
			rotation = rotation + speed * Time.deltaTime * 2;
			if (rotation < 0)
				transform.eulerAngles = new Vector3 (0f, 0f, rotation);
			if (modeScale == 0)
				transform.localScale -= new Vector3 (0, speed * Time.deltaTime * 0.2f, 0);
			else 
				if (modeScale == 1)
					transform.localScale += new Vector3 (0, speed * Time.deltaTime * 0.2f, 0);
			else 
			{
				transform.eulerAngles = new Vector3 (0f, 0f, 0f);
				if (transform.localScale.y > 1f)
					transform.localScale -= new Vector3 (0, speed * Time.deltaTime * 0.2f, 0);
				else
					transform.localScale = new Vector3 (1f, 1f, 1f);
			}
			if (transform.localScale.y <= scaledown)
				modeScale++;
			else 
				if (transform.localScale.y >= scaleup)
					modeScale++;
			
		}
	}
}
