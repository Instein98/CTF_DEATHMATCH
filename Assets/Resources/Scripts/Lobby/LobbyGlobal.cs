using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class LobbyGlobal : MonoBehaviour {
	public MyDiscovery m;

	// Use this for initialization
	void Start () {
		m = GameObject.Find("NetworkDiscovery").GetComponent<MyDiscovery>();
		// NetworkManager.singleton.StopClient();
		// NetworkManager.singleton.StopHost();
		// NetworkManager.singleton.StopMatchMaker();
		// Network.Disconnect();
		// m.showGUI = false;
		// m.showGUI = false;
		// m.Initialize();
		// m.StartAsClient();
		m.Start();
		m.lanDic.Clear();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
