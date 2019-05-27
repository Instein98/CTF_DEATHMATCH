using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_FlagTrigger : MonoBehaviour {

	public bool disabled = false;
	public float waitTime = 5;
	public Transform trigger;
	public GameObject flagPrefab;
	public Transform collCar;  // The car enter the trigger
	
	void Start () {
		// trigger = transform.Find("Trigger");
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
			if (coll.gameObject.name.Substring(0, 3) == "Car"){
				collCar = coll.transform;
				// Vector3 flagPos = collCar.position + new Vector3(1f, 1.2f, -1.5f);
				// Quaternion flagRot = collCar.rotation;
				if (collCar.GetComponent<T_CarFlag>() == null){
					return;
				}
				GameObject.Find("Global").GetComponent<T_Global>().getFlag();
				collCar.GetComponent<T_CarFlag>().getFlag();
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
}
