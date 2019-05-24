using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	public GameObject camCenter;  // for see the ocean
	public GameObject OutCarCamCenter;
	// public Vector3 carCamPos;
	// public Quaternion carCamRot;
	public GameObject car;
	private bool gameStart = false;
	private bool transToCar = false;
	private Vector3 position;
	// public GlobalControl gc;
	// public GameObject carCamPoint;

	// Use this for initialization
	void Start () {
		// gc = GameObject.Find("Global").GetComponent<GlobalControl>();
		OutCarCamCenter = GameObject.Find("OutCarCamCenter");
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStart){
			camCenter.transform.Rotate(Vector3.up * 50 * Time.deltaTime, Space.World);
			if (camCenter.transform.eulerAngles.y >= 160){
				gameStart = false;
				transToCar = true;
				position = transform.position;
			}
		}
		if (transToCar){
			Transform carCP = car.GetComponent<CarSkills>().carCamPoint;
			float t = 1;
			position.x = Mathf.Lerp(position.x, carCP.position.x, Time.deltaTime*t);
			position.y = Mathf.Lerp(position.y, carCP.position.y, Time.deltaTime*t);
			position.z = Mathf.Lerp(position.z, carCP.position.z, Time.deltaTime*t);
        	transform.position = position;
			if ((int)position.x == (int)carCP.position.x){
				transToCar = false;
				transform.rotation = carCP.rotation;
				transform.parent = OutCarCamCenter.transform;
			}
		}
	}

	public void initCam(Vector3 pos, Quaternion rot){
		// carCamPos = pos;
		// carCamRot = rot;
		transform.position = pos;
		transform.rotation = rot;
		transform.parent = OutCarCamCenter.transform;
	}

	public void gameStartCam(){
		gameStart = true;
		transform.rotation = camCenter.transform.rotation;
		transform.parent = camCenter.transform;
		transform.localPosition = new Vector3(-30, -240, -270);
	}
}
