using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Ocean : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.name.Substring(0, 3) == "Car"){
			// Debug.Log("T_CarFlag:" + coll.gameObject.GetComponent<T_CarFlag>());
			// Debug.Log("T_CarSkill:" + coll.gameObject.GetComponent<T_CarSkill>());
			coll.gameObject.GetComponent<T_CarFlag>().losePoint();
			coll.gameObject.GetComponent<T_CarSkill>().resetCarPosition();
			GameObject.Find("Global").GetComponent<T_Global>().finishIntoOcean();
		}else if(coll.gameObject.name.Length >= 10 && coll.gameObject.name.Substring(0, 10) == "flag_carry"){
			GameObject.Find("Global").GetComponent<T_Global>().flagFall(true);
			coll.gameObject.GetComponent<T_flag>().car.GetComponent<T_CarFlag>().dropFlag(true);
		}
	}
}
