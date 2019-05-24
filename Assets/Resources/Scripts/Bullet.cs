using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float power = 10;
	public float radius = 5;
	public float upforce = 1;
	private bool boom = false;

	public GameObject explosion;
	public ParticleSystem[] p;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 5);
		ParticleSystem[] p = explosion.GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem ps in p){
			ps.Stop();
		}
	}
	
	// Update is called once per frame
	void Update () {
		// if (boom){
		// 	bool stoped = true;
		// 	foreach(ParticleSystem ps in p){
		// 		if (!ps.isStopped){
		// 			stoped = false;
		// 			return;
		// 		}
		// 	}
		// 	if (stoped)
		// 		Destroy(gameObject);
		// }
	}

	void OnTriggerEnter(Collider GetObj){
    	if (GetObj != null && !boom){
			Boom();
		}  
	}

	private void Boom(){
		boom = true;
		GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		p = explosion.GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem ps in p){
			ps.Play();
		}
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
		GetComponent<BoxCollider>().enabled = false;
		Destroy(gameObject, 0.8f);
	}
}
