using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float power = 10;
	public float radius = 5;
	public float upforce = 1;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider GetObj){
    	if (GetObj != null){
			Boom();
		}  
	}

	private void Boom(){
		Vector3 explosionPos = gameObject.transform.position;

		//  Notice!!! It only finds the colliders' rigidbody!!
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders){
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			// Debug.Log(" " + hit.gameObject.name+"'s Rigidbody: "+rb);
			if (rb != null && rb.gameObject.name != gameObject.name){
				// Debug.Log(hit.gameObject.name+" should FLY!!");
				rb.AddExplosionForce(power, explosionPos, radius, upforce, ForceMode.Impulse);
			}
		}
		// Debug.Log("BOOM!!!!");
		Destroy(gameObject);
	}
}
