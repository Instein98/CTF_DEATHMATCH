using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Home : NetworkBehaviour {

	public GameObject car;
	public int carID = -1;

	void OnTriggerEnter(Collider coll){
		if (car == null){
			return;
		}
		if (coll.gameObject.name == "Car"+carID && car.GetComponent<CarFlagControl>().carryFlag){
			car.GetComponent<CarFlagControl>().getPoint();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
