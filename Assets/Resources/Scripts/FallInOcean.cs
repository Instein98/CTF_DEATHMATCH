using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallInOcean : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.name.Substring(0, 3) == "Car"){
			coll.gameObject.GetComponent<CarSkills>().resetCarPosition();
			coll.gameObject.GetComponent<CarFlagControl>().losePoint();
		}else if(coll.gameObject.name == "flag_carry(Clone)"){
			coll.gameObject.GetComponent<FlagCarry>().car.GetComponent<CarFlagControl>().dropFlag(true);
		}
	}
}
