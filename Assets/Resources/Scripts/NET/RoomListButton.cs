using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomListButton : MonoBehaviour {
	private UILabel r;

	// Use this for initialization
	void Start () {
		r = transform.Find("Label").GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnClick(){
		NetworkManager.singleton.networkAddress = r.text;
        NetworkManager.singleton.StartClient();
	}
}
