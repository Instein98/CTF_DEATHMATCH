using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateRoomButton : MonoBehaviour {
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
		m.StartAsServer();
		// DontDestroyOnLoad(m.gameObject);
		NetworkManager.singleton.networkPort = 7777;
		NetworkManager.singleton.StartHost();
	}
}
