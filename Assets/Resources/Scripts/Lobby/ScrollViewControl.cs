using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewControl : MonoBehaviour {

	public GameObject roomPrefab;
	public int roomNum = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addRoom(string addr, string data){
		GameObject room = Instantiate(roomPrefab, transform);
		room.transform.localPosition = new Vector3(-6.24f, 130-roomNum*80, 0);
		room.transform.Find("Label").GetComponent<UILabel>().text = addr.Substring(7);
		roomNum += 1;
	}

}
