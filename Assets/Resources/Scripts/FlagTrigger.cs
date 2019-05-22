using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlagTrigger : NetworkBehaviour {

	public bool disabled = false;
	public float waitTime = 5;
	public Transform trigger;
	public GameObject flagPrefab;
	public Transform collCar;
	// public Transform lastDropCar = null;
	

	// Use this for initialization
	void Start () {
		trigger = transform.Find("Trigger");
		if (disabled){
			disable();
		}else{
			enable();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll){
		// if (isServer)
		// 	return;

    	if (coll != null && !disabled){
			if (coll.gameObject.name == "Car(Clone)"){
				collCar = coll.transform;
				// Vector3 flagPos = collCar.position + new Vector3(1f, 1.2f, -1.5f);
				// Quaternion flagRot = collCar.rotation;
				collCar.GetComponent<CarFlagControl>().getFlag();
				// CmdCarry(flagPos, flagRot);
			}
		}  
	}

	public void enable(){
		Renderer rend = trigger.GetComponent<Renderer>();
		rend.material.color = new Color(0, 1, 0, 0.8f);
		disabled = false;
		GetComponent<CapsuleCollider>().enabled = true;
	}

	public void disable(){
		Renderer rend = trigger.GetComponent<Renderer>();
		rend.material.color = new Color(1, 0, 0, 0.8f);
		disabled = true;
		Invoke("enable", waitTime);
		GetComponent<CapsuleCollider>().enabled = false;
	}

	// [Command]
	// public void CmdCarry(Vector3 flagPos, Quaternion flagRot){
	// 		GameObject flag = Instantiate(flagPrefab, flagPos, flagRot);
	// 		flag.transform.parent = collCar;
	// 		NetworkServer.SpawnWithClientAuthority(flag, connectionToClient);;
	// 		// flag.transform.parent = collCar;
	// 		Destroy(transform.gameObject);
	// }
}
