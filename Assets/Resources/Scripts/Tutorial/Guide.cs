using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || 
		Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.UpArrow) ||
		Input.GetKeyDown(KeyCode.W)){
			gameObject.SetActive(false);
			GameObject.Find("Global").GetComponent<T_Global>().tutorialStart();
		}
	}
}
