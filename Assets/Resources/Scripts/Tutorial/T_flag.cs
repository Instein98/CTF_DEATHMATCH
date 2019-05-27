using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_flag : MonoBehaviour {

	public bool onCar = true;
	public Transform car;
	public GameObject TriggerPrefab;

	void Start () {		
	}

	void Update () {
	}

	void OnCollisionEnter(Collision collisionInfo){
		if (!onCar  && collisionInfo.collider.gameObject.name == "Platform"){
			GameObject.Find("Global").GetComponent<T_Global>().flagFall(false);
			car.GetComponent<T_CarFlag>().dropFlag(false);
		}
	}
}
