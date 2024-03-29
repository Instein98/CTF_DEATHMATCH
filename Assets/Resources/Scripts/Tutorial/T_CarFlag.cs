﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_CarFlag : MonoBehaviour {

	public float flagFallDeltaV = 3;
	private Vector3 lastV;
	private float deltaV;
	public bool carryFlag = false;
	public bool speedUp = false;
	public GameObject flagPrefab;
	public GameObject triggerPrefab;
	public GameObject homePrefab;
	public GameObject home;
	Transform flag = null;
	public int netID;
	public int score = 0;

	private GameObject UIRoot;
	private UILabel Mscore;
	private UILabel Escore;
	
	void Start(){
		UIRoot = GameObject.Find("UI Root");
		if (UIRoot != null){
			Mscore = UIRoot.transform.Find("Anchor_T/ScoreBoard/MyScore").GetComponent<UILabel>();
			Escore = UIRoot.transform.Find("Anchor_T/ScoreBoard/EnemyScore").GetComponent<UILabel>();
		}
		// netID = int.Parse(GetComponent<NetworkIdentity>().netId.ToString());
		// gameObject.name = "Car" + netID;
		// if (isLocalPlayer)
		// CmdCreateHome();
	}

	void Update () {
		updateScoreBoard();
		// if (!isLocalPlayer)
        //     return;
		if (GetComponent<T_CarSkill>().dash){
			deltaV = 0;
		}else{
			deltaV = (lastV - GetComponent<Rigidbody>().velocity).magnitude;
		}
		flyFlag();
		lastV = GetComponent<Rigidbody>().velocity;
	}

	public void getFlag(){
		if (carryFlag)
			return;
		carryFlag = true;
		CmdGetFlag();
	}

	public void flyFlag(){
		if (deltaV > flagFallDeltaV && carryFlag){
			// flag = transform.Find("flag_carry(Clone)");
			CmdFlyFlag(flag.gameObject, lastV);
		}
	}

	public void dropFlag(bool inOcean){
		// if (!isLocalPlayer){
		// 	return;
		// }
		CmdFlagDrop(flag.gameObject, inOcean);
	}

	public void getPoint(){
		// if (!isLocalPlayer)
		// 	return;
		CmdGetPoint();
	}

	public void losePoint(){
		// if (!isLocalPlayer)
		// 	return;
		CmdLosePoint();
	}

	void updateScoreBoard(){
		Mscore.text = "" + score;
	}

	void CmdGetPoint(){
		score += 1;
		carryFlag = false;
		Destroy(flag.gameObject);
		Vector3 ftPos = new Vector3(0, 5.16f, 0);
		Quaternion rot = Quaternion.Euler(0, 0, 0);
		GameObject ft = Instantiate(triggerPrefab, ftPos, rot);
		ft.transform.GetComponent<T_FlagTrigger>().disabled = true;
		// NetworkServer.Spawn(ft);
		// RpcFlagDrop(ft);
		ft.transform.GetComponent<T_FlagTrigger>().disable();
	}

	// [Command]
	void CmdLosePoint(){
		score -= 1;
	}

	// [Command]
	public void CmdGetFlag(){
		carryFlag = true;
		Vector3 flagPos = transform.position + new Vector3(1f, 1.2f, -1.5f);
		Quaternion flagRot = transform.rotation;
		GameObject newflag = Instantiate(flagPrefab, flagPos, flagRot);
		newflag.transform.parent = transform;
		newflag.GetComponent<T_flag>().car = transform;
		newflag.transform.localPosition = new Vector3(1f, 1.2f, -1.5f);
		newflag.transform.rotation = transform.rotation;
		// NetworkServer.Spawn(newflag);
		flag = newflag.transform;
		GameObject ft = GameObject.Find("FlagTrigger_T");
		if (ft != null)
			Destroy(ft);
		ft = GameObject.Find("FlagTrigger_T(Clone)");
		if (ft != null)
			Destroy(ft);
		// RpcGetFlag(newflag, gameObject);
	}


	public void CmdFlyFlag(GameObject flag, Vector3 v){
		// flag = transform.Find("flag_carry(Clone)");
		// flag.GetComponent<NetworkTransform>().enabled = true;
		//  Notice !! to change the Input.GetMouseButton !!!
		if (Input.GetMouseButton(0) && !Input.GetMouseButton(1)){
			return;
		}
		// Rigidbody rb = flag.gameObject.AddComponent<Rigidbody>() as Rigidbody;
		// flag.transform.parent = null;
		// if (rb == null)
		// 	return;
		// rb.velocity = v * 3;
		// rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
		flag.GetComponent<T_flag>().onCar = false;
		carryFlag = false;
		RpcFlyFlag(v);
	}

	public void RpcFlyFlag(Vector3 v){
		//  Notice !! to change the Input.GetMouseButton !!!
		if (Input.GetMouseButton(0) && !Input.GetMouseButton(1)){
			return;
		}
		Rigidbody rb = flag.gameObject.AddComponent<Rigidbody>() as Rigidbody;
		flag.parent = null;
		rb.velocity = v * 3;
		rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
		flag.GetComponent<T_flag>().onCar = false;
		carryFlag = false;
		// flag.GetComponent<NetworkTransform>().enabled = true;
	}

	void CmdFlagDrop(GameObject flag, bool inOcean){
		GameObject temp = GameObject.Find("FlagTrigger_T");
		if (temp != null)
			return;
		temp = GameObject.Find("FlagTrigger_T(Clone)");
		if (temp != null)
			return;	
		if (flag == null )
			return;
		Vector3 ftPos;
		if (inOcean)
			ftPos = new Vector3(0, 5.16f, 0);
		else
			ftPos = new Vector3(flag.transform.position.x, 5.16f, flag.transform.position.z);
		Quaternion rot = Quaternion.Euler(0, 0, 0);
		GameObject ft = Instantiate(triggerPrefab, ftPos, rot);
		ft.GetComponent<T_FlagTrigger>().disabled = true;
		ft.GetComponent<T_FlagTrigger>().disable();
		// ft.transform.GetComponent<T_FlagTrigger>().lastDropCar = transform;
		// NetworkServer.Spawn(ft);
		Destroy(flag.gameObject);
		// RpcFlagDrop(ft);
	}

	// void CmdCreateHome(){
	// 	Vector3 pos = new Vector3(transform.position.x, 2.16f, transform.position.z) + transform.forward * -10;
	// 	Quaternion rot = Quaternion.Euler(0, 0, 0);
	// 	GameObject home = Instantiate(homePrefab, pos, rot);
	// 	home.name = "Home" + netID;
	// 	home.GetComponent<Home>().car = gameObject;
	// 	home.GetComponent<Home>().carID = netID;
	// 	NetworkServer.Spawn(home);
	// 	RpcCreateHome(home, gameObject);
	// }

	// [ClientRpc]
	// void RpcCreateHome(GameObject home, GameObject car){
	// 	car.GetComponent<CarFlagControl>().home = home;
	// 	int carID = car.GetComponent<CarFlagControl>().netID;
	// 	home.name = "Home" + carID;
	// 	home.GetComponent<Home>().car = car;
	// 	home.GetComponent<Home>().carID = carID;
	// 	if (isLocalPlayer){
	// 		home.GetComponent<Renderer>().material.color = Color.blue;
	// 	}
	// }
}
