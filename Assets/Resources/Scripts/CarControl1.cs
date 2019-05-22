using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CarControl1 : MonoBehaviour {

	public WheelCollider frontLeftCol, forntRightCol;
	public WheelCollider backLeftCol, backRightCol;
	public Transform frontLeft, frontRight;
	public Transform backLeft, backRight;

	public float _steerAngle = 25.0f;
	public float _motorforce = 1500f;
	public float steerAngle;

	float h, v;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	// void Update () {
		
	// }

	public void FixedUpdate(){
		Debug.Log("Hello, World!");
		Inputs();
		Drive();
		SteerCar();

		UpdateWheelPos(frontLeftCol, frontLeft);
		UpdateWheelPos(forntRightCol, frontRight);
		UpdateWheelPos(backLeftCol, backLeft);
		UpdateWheelPos(backRightCol, backRight);
	}

	void Inputs(){
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		Debug.Log("v: "+v);
		Debug.Log("h: "+h);
	}

	void Drive(){
		backLeftCol.motorTorque = v * _motorforce;
		backRightCol.motorTorque = v * _motorforce;
	}

	void SteerCar(){
		steerAngle = h * _steerAngle;
		frontLeftCol.steerAngle = steerAngle;
		forntRightCol.steerAngle = steerAngle;
	}

	void UpdateWheelPos(WheelCollider col, Transform t){
		Vector3 pos = t.position;
		Quaternion rot = t.rotation;

		col.GetWorldPose(out pos, out rot);
		t.position = pos;
		t.rotation = rot;
	}
}
