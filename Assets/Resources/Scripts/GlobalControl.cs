﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class GlobalControl : NetworkBehaviour {
	public int gameMinutes = 5;
	public float gameLeftSeconds;
	private GameObject UIRoot;
	private UILabel labelMin;
	private UILabel labelSec;
	private UILabel labelLose;
	private UILabel labelWin;
	private UILabel labelDraw;
	
	private UILabel Mscore;
	private UILabel Escore;
	private int Min;
	private int Sec;
	[SyncVar]
	public bool hasStart = false;

	// Use this for initialization
	void Start () {
		gameLeftSeconds = gameMinutes * 60;
		UIRoot = GameObject.Find("UI Root");
		if (UIRoot != null){
			labelMin = UIRoot.transform.Find("Anchor_T/TimeBoard/min").GetComponent<UILabel>();
			labelSec = UIRoot.transform.Find("Anchor_T/TimeBoard/sec").GetComponent<UILabel>();
			labelLose = UIRoot.transform.Find("lose").GetComponent<UILabel>();
			labelWin = UIRoot.transform.Find("win").GetComponent<UILabel>();
			labelDraw = UIRoot.transform.Find("draw").GetComponent<UILabel>();
			Mscore = UIRoot.transform.Find("Anchor_T/ScoreBoard/MyScore").GetComponent<UILabel>();
			Escore = UIRoot.transform.Find("Anchor_T/ScoreBoard/EnemyScore").GetComponent<UILabel>();
		}
		showTime();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStart)
			return;
		CmdGameTime(Time.deltaTime);
	}

	void showTime(){
		if (gameLeftSeconds <= 0){
			GameOver();
		}
		string s = "" + ((int)gameLeftSeconds % 60);
		string m = "" + ((int)gameLeftSeconds / 60);
		if (s.Length > 1)
			labelSec.text = s;
		else
			labelSec.text = "0" + s;
		if (m.Length > 1)
			labelMin.text = m + ":";
		else	
			labelMin.text = "0" + m + ":";
	}

	void GameOver(){
		hasStart = false;
		if(int.Parse(Mscore.text) > int.Parse(Escore.text)){
			labelWin.gameObject.SetActive(true);
		}else if (int.Parse(Mscore.text) < int.Parse(Escore.text)){
			labelLose.gameObject.SetActive(true);
		}else{
			labelDraw.gameObject.SetActive(true);
		}
		Time.timeScale = 0;

	}
	void CmdGameTime(float t){
		gameLeftSeconds -= t;
		showTime();
	}

}