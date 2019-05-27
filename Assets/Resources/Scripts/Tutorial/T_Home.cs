using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Home : MonoBehaviour {

	public GameObject car;
	public int carID = -1;

	void OnTriggerEnter(Collider coll){
		if (car == null){
			return;
		}
		if (coll.gameObject.name == "Car" && car.GetComponent<T_CarFlag>().carryFlag){
			GameObject.Find("Global").GetComponent<T_Global>().backHome();
			car.GetComponent<T_CarFlag>().getPoint();
		}
	}


	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
