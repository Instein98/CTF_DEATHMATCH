using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Global : MonoBehaviour {
	private bool getTheFlag = false;
	private bool backToHome = false;  // tutorial target.
	private bool useDash = false;
	private bool useMissile = false;
	private bool intoOcean = false;
	public UILabel guideText;
	public int gameMinutes = 5;
	public float gameLeftSeconds;
	public T_Camera cc;
	// public GameObject Back;
	private GameObject UIRoot;
	private UILabel labelMin;
	private UILabel labelSec;
	private UILabel labelLose;
	private UILabel labelWin;
	private UILabel labelDraw;
	
	private UILabel Mscore;
	private UILabel Escore;
	private UIButton BackToLobby;
	private int Min;
	private int Sec;
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
			BackToLobby = UIRoot.transform.Find("lobbyButton").GetComponent<UIButton>();
		}
		// showTime();
	}
	
	// Update is called once per frame
	void Update () {
		// if (!hasStart)
		// 	return;
		// CmdGameTime(Time.deltaTime);
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
		BackToLobby.gameObject.SetActive(true);
		Time.timeScale = 0;
		

	}
	void CmdGameTime(float t){
		gameLeftSeconds -= t;
		showTime();
	}

	public void startGame(){
		cc.gameStartCam();
		hasStart = true;
	}

	public void tutorialStart (){
		getTheFlag = true;  // target is to get the flag
		guideText.text = "Welcome to CTF DEATHMATH!\nNow try to capture the flag in the center.";
	}

	public void getFlag(){
		if (!getTheFlag && !backToHome)
			return;
		getTheFlag = false;
		backToHome = true;
		guideText.text = "Carry you flag back to your HOME (blue one).\n You may drop your flag if you dash or bump into something.";
	}

	// He make it to get home.
	public void backHome(){
		if (!backToHome && !useDash)
			return;
		backToHome = false;
		useDash = true;
		guideText.text = "Left click your mouse to dash.\n(Speed up immediately may drop the flag carried)";
	}

	public void finishDash(){
		if (!useDash && !useMissile)
			return;
		useDash = false;
		useMissile = true;
		guideText.text = "Long press your right button of your mouse,\nthen Left click to launch a missile.";
	}

	public void finishMissile(){
		if (!useMissile && !intoOcean)
			return;
		useMissile = false;
		intoOcean = true;
		guideText.text = "Falling into the ocean, you will lose a point and respawn.\nNow try to fall into the ocean.";
	}

	public void finishIntoOcean(){
		if (!intoOcean)
			return;
		intoOcean = false;
		guideText.text = "congratulations! You have finish the tutorial.\n In real game who gets more points in a limit time will win.";
		// active the button to lobby.
		Destroy(guideText.gameObject, 11);
	}

	public void finishGuide(){
		guideText.text = "congratulations! You have finish the tutorial.\n In real game who gets more points in a limit time will win.";
	}

	public void flagFall(bool isOcean){
		if (guideText == null)
			return;
		int delayTime = 6;
		if (!isOcean){
			guideText.text = "When the flag fall into the ground, it will take few seconds for it to be available again.";
		}else{
			guideText.text = "When the flag fall into the Ocean, it will respawn at the center of the map.";
		}
		if (getTheFlag){
			Invoke("tutorialStart", delayTime);
		}else if(backToHome){
			Invoke("getFlag", delayTime);
		}else if(useDash){
			Invoke("backHome", delayTime);
		}else if (useMissile){
			Invoke("finishDash", delayTime);
		}else if (intoOcean){
			Invoke("finishMissile", delayTime);
		}else{
			Invoke("finishGuide", delayTime);
		}
	}
}
