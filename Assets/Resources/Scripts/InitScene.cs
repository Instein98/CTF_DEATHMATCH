using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InitScene : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GameObject nd = GameObject.Find("NetworkDiscovery");
		GameObject nm = GameObject.Find("NetworkManager");
		DontDestroyOnLoad(nd);
		DontDestroyOnLoad(nm);
		SceneManager.LoadScene("Lobby");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
