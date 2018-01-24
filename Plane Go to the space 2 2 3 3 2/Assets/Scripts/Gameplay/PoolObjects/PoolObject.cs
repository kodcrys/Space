using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour {

	/*
	 * Script 1
	 */

	public GameObject pooledObject;
	public int pooledAmount = 20;
	private int poolIndex;
	private int poolIndexMax;

	List<GameObject> pooledObjects;

	void Awake() 
	{
		poolIndexMax = pooledAmount;
	}

	// Use this for initialization
	void Start()
	{
		poolIndex = 0;
		pooledObjects = new List<GameObject>();
		for (int i = 0; i < pooledAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.SetActive(false);
			obj.transform.parent = transform;
			pooledObjects.Add(obj);
		}
	}
	public int getindex()
	{
		poolIndex += 1;
		if (poolIndex >= poolIndexMax - 1)
			poolIndex = 0;
		if (pooledObjects [poolIndex].activeInHierarchy)
			pooledObjects [poolIndex].SetActive (false);
		return poolIndex;
	}

	public GameObject GetPoolObject()
	{
		if (!pooledObjects [poolIndex].activeInHierarchy)
			return pooledObjects [poolIndex];
		return null;
	}
}
