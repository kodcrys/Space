using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public static UIManager Instance;

	void Start(){
		if (Instance == null)
			Instance = this;
	}

	public void BackHome(string sceneName){
		SceneManager.LoadScene (sceneName);
	}


}
