﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_CarSkill : MonoBehaviour {

	public float dashCoolTime = 5;
	public float dashDurTime = 0.3f;
	public float missileCoolTime = 3;
	public int missileNum = 3;
	private float dashLeftTime;  
	public bool dash = false;
	private bool missile = false;
	private GameObject UIRoot;
	private UISprite spriteDash;
	private UISprite spriteMissile;
	private UISprite spriteScope;
	private UILabel labelMissile; 
	public Rigidbody carBody;
	public Transform FirstT;  // first - second = the car's orientation.
	public Transform SecondT;
	// public Camera mainCamera;
	public GameObject carCamera;
	public float speedUpForce;
	public float bulletSpeed;
	public float mouseSensitive = 0;
	public GameObject bullet;
	public Transform carCamPoint;
	public GameObject carCamCenterPoint;
	private Transform OutCarCamCenter;
	// private Transform 
	private bool speedUp = false;
	private float mouseX = 0;
	private float mouseY = 0;
	private Vector3 orientation;
	private Vector3 iniPos;  // initial 
	private Quaternion iniRot;

	void Start () {
		iniPos = transform.position;
		iniPos = new Vector3(iniPos.x, 2.06f, iniPos.z);
		iniRot = transform.rotation;
		UIRoot = GameObject.Find("UI Root");
		if (UIRoot != null){
			UIRoot.SetActive(false);
			UIRoot.SetActive(true);
			spriteDash = UIRoot.transform.Find("Anchor_RB/Dash").GetComponent<UISprite>();
			spriteMissile = UIRoot.transform.Find("Anchor_RB/Missile").GetComponent<UISprite>();
			labelMissile = spriteMissile.transform.Find("LabelMissile").GetComponent<UILabel>();
			spriteScope = UIRoot.transform.Find("scope").GetComponent<UISprite>();
			spriteScope.gameObject.SetActive(false);
		}

		OutCarCamCenter = GameObject.Find("OutCarCamCenter").transform;
		OutCarCamCenter.transform.rotation = carCamCenterPoint.transform.rotation;
		OutCarCamCenter.transform.position = carCamCenterPoint.transform.position;
		carCamera = GameObject.Find("Camera");
		// Debug.Log("carCamera: "+carCamera);
		// Debug.Log("CameraControl: "+carCamera.GetComponent<CameraControl>());
		carCamera.GetComponent<T_Camera>().car = gameObject;
		carCamera.GetComponent<T_Camera>().initCam(carCamPoint.position, carCamPoint.rotation);

		CmdStartGame(); 

		
	}

	 void Update(){
		OutCarCamCenter.transform.position = carCamCenterPoint.transform.position;
		if (!GameObject.Find("Global").GetComponent<T_Global>().hasStart){
			transform.position = iniPos;
			// transform.rotation = iniRot;
			return;
		}
		float axisX = Input.GetAxis("Mouse X");
		float axisY = Input.GetAxis("Mouse Y");
		mouseX += axisX;
		mouseY += axisY;
		Quaternion q = Quaternion.Euler(-mouseY * mouseSensitive, mouseX * mouseSensitive, 0);
		OutCarCamCenter.transform.localRotation = q;
		float t = Time.deltaTime;
		callSkills(t);
		skillsCoolDown(t);
	}

	private void CmdFire(Vector3 pos, Quaternion rot){
		GameObject b = Instantiate(bullet, pos, rot);
		b.GetComponent<Rigidbody>().velocity = b.transform.forward * bulletSpeed;
		b.transform.Rotate(90, 0, 0, Space.Self);
		// NetworkServer.Spawn(b);
		Destroy(b, 5);
	}

	// [Command]
	void CmdStartGame(){
		GameObject g = GameObject.Find("Global");
		RpcStartGame(g);
	}

	// [ClientRpc]
	public void RpcStartGame(GameObject global){
		// GameObject nd = GameObject.Find("NetworkDiscovery");
		// if (nd != null){
		// 	nd.GetComponent<MyDiscovery>().StopBroadcast();
		// }
		global.GetComponent<T_Global>().startGame();
		// GameObject.Find("UI Root/wait").SetActive(false);
	}

	// mouse right button to speed up, left and right to fire.
	private void callSkills(float deltaT){
		orientation = (FirstT.position - SecondT.position);
		orientation = new Vector3(orientation.x, 0, orientation.z).normalized;

		if (Input.GetMouseButtonDown(1)){
			missile = true;
			spriteScope.gameObject.SetActive(true);
			carCamera.GetComponent<Camera>().fieldOfView /= 2;
		}else if (Input.GetMouseButtonUp(1)){
			missile = false;
			spriteScope.gameObject.SetActive(false);
			carCamera.GetComponent<Camera>().fieldOfView *= 2;
		}

		if (Input.GetMouseButtonDown(0)){
			if (missile && missileNum >= 1){
				missileNum -= 1;
				labelMissile.text = "" + missileNum;
				if (spriteMissile.fillAmount >= 1){
					spriteMissile.fillAmount = 0;
				}
				GameObject.Find("Global").GetComponent<T_Global>().finishMissile();
				CmdFire(carCamera.transform.position, carCamera.transform.rotation);
			}else if (!missile && spriteDash.fillAmount >= 1){
				// vBeforeDash = GetComponent<Rigidbody>().velocity.magnitude;
				dash = true;
				GameObject.Find("Global").GetComponent<T_Global>().finishDash();
				dashLeftTime = dashDurTime;
				spriteDash.fillAmount = 0;
			}
		}

		if (dash){
			carBody.AddForce(speedUpForce * orientation, ForceMode.VelocityChange);
			dashLeftTime -= deltaT;
			if (dashLeftTime <= 0){
				dash = false;
			}
		}
	}

	private void skillsCoolDown(float deltaT){
		if (spriteDash.fillAmount < 1){
			spriteDash.fillAmount += deltaT/dashCoolTime;
		}
		if (int.Parse(labelMissile.text) < 3){
			spriteMissile.fillAmount += deltaT/missileCoolTime;
			if (spriteMissile.fillAmount >= 1){
				labelMissile.text = "" + (int.Parse(labelMissile.text) + 1);
				missileNum += 1;
				if (int.Parse(labelMissile.text) < 3){
					spriteMissile.fillAmount = 0;
				}
			} 
		}
	}

	public void resetCarPosition(){
		if (GetComponent<T_CarFlag>().carryFlag){
			GetComponent<T_CarFlag>().carryFlag = false;
			GameObject.Find("Global").GetComponent<T_Global>().flagFall(true);
			GetComponent<T_CarFlag>().dropFlag(true);
		}
		GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		transform.position = new Vector3(iniPos.x, iniPos.y + 30, iniPos.z);
		transform.rotation = iniRot;
	}
}
