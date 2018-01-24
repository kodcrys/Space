using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

	/*
	 * Script 2
	 */

	/* 
	 * Cach 1: tao list pool object
	 * Cach nay kho kiem soat Pool object hon, vi khi lay doi tuong PoolObject don le ra thi phai lay bang index (so thu tu) kho nho'
	 */
	public List<PoolObject> lstPool = new List<PoolObject>();

	/* 
	 * Singleton pattern
	 * Detail: https://viblo.asia/p/huong-dan-su-dung-singleton-de-quan-ly-game-trong-unity3d-l0rvmxwxGyqA
	 */
	public static PoolManager Intance { get; private set; }

	// Use this for initialization
	void Start () {
		if (Intance == null)
			Intance = this;
	}
}
