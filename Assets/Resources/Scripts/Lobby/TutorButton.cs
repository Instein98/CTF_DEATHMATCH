using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class TutorButton : MonoBehaviour {
	public MyDiscovery m;

	// Use this for initialization
	void Start () {
		m = GameObject.Find("NetworkDiscovery").GetComponent<MyDiscovery>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick(){
		m.StopBroadcast();
		m.Initialize();
		SceneManager.LoadScene("Tutorial");
	}
	
}
