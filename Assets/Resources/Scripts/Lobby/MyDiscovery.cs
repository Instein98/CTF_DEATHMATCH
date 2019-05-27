using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyDiscovery : NetworkDiscovery {
	public Dictionary<string, string> lanDic = new Dictionary<string, string>();
	public ScrollViewControl svc;

	private bool listen;

	// Use this for initialization
	public void Start (){
		NetworkManager.singleton.StopClient();
		NetworkManager.singleton.StopHost();
		NetworkManager.singleton.StopMatchMaker();
		Network.Disconnect();
		base.showGUI = false;
		this.showGUI = false;
		// StopBroadcast();
		Initialize();
		listen = StartAsClient();
	}

	// void Update(){
	// 	// if (!listen)
	// 	// 	listen = StartAsClient();
	// }

	public override void OnReceivedBroadcast(string fromAddress, string data){
		Debug.Log("Receieve:" + fromAddress+data);
		// lanDic.Clear();
		svc = GameObject.Find("UI Root/Anchor_R/Scroll View").GetComponent<ScrollViewControl>();
		string ipv4Add = fromAddress.Substring(7);
		if (lanDic.ContainsKey(ipv4Add))
			return;
		lanDic.Add(ipv4Add, data);
		svc.addRoom(fromAddress, data);
    }
}
