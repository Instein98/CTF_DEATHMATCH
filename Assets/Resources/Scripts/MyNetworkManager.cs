using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {
	public NetworkDiscovery discovery;

	// Use this for initialization
	void Start () {
		discovery.Initialize();
		discovery.StartAsClient();
	}
}
