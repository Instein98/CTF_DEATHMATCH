using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyDiscovery : NetworkDiscovery {
	Dictionary<string, string> lanDic = new Dictionary<string, string>();
	public ScrollViewControl svc;

	// Use this for initialization
	void Start (){
		NetworkManager.singleton.StopClient();
		NetworkManager.singleton.StopHost();
		NetworkManager.singleton.StopMatchMaker();
		Network.Disconnect();
		svc = GameObject.Find("UI Root/Anchor_R/Scroll View").GetComponent<ScrollViewControl>();
		base.showGUI = false;
		this.showGUI = false;
		Initialize();
		StartAsClient();
	}

	public override void OnReceivedBroadcast(string fromAddress, string data){
		string ipv4Add = fromAddress.Substring(7);
		if (lanDic.ContainsKey(ipv4Add))
			return;
		lanDic.Add(ipv4Add, data);
		svc.addRoom(fromAddress, data);
    }
}
