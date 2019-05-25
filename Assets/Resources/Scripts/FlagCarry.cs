using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class FlagCarry : NetworkBehaviour {
	[SyncVar]
	public bool onCar = true;
	public Transform car;
	public GameObject TriggerPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// transform.localPosition = new Vector3(1f, 1.2f, -1.5f);
	}

	void OnCollisionEnter(Collision collisionInfo){
		if (!onCar  && collisionInfo.collider.gameObject.name == "Platform"){
			car.GetComponent<CarFlagControl>().dropFlag(false);
		}
	}

	
}
