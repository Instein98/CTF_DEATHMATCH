using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CarSkills : NetworkBehaviour {

	public float dashCoolTime = 5;
	public float dashDurTime = 0.3f;
	public float missileCoolTime = 3;
	public int missileNum = 3;
	private float dashLeftTime;  
	private bool dash = false;
	private bool missile = false;
	private GameObject UIRoot;
	private UISprite spriteDash;
	private UISprite spriteMissile;
	private UILabel labelMissile; 
	public Rigidbody carBody;
	public Transform FirstT;  // first - second = the car's orientation.
	public Transform SecondT;
	// public Camera mainCamera;
	public Camera carCamera;
	public float speedUpForce;
	public float bulletSpeed;
	public float mouseSensitive = 0;
	public GameObject bullet;
	private bool speedUp = false;
	private float mouseX = 0;
	private float mouseY = 0;
	private Vector3 orientation;

	public override void OnStartLocalPlayer(){
		carCamera = transform.Find("CarCamera").GetComponent<Camera>();
		carCamera.gameObject.SetActive(true);
	}

	void Start () {
		UIRoot = GameObject.Find("UI Root");
		if (UIRoot != null){
			UIRoot.SetActive(false);
			UIRoot.SetActive(true);
			spriteDash = UIRoot.transform.Find("Anchor_RB/Dash").GetComponent<UISprite>();
			spriteMissile = UIRoot.transform.Find("Anchor_RB/Missile").GetComponent<UISprite>();
			labelMissile = spriteMissile.transform.Find("LabelMissile").GetComponent<UILabel>();
			Debug.Log("spriteDash = "+spriteDash);
			Debug.Log("spriteMissile = "+spriteMissile);
			Debug.Log("labelMissile = "+labelMissile);
		}
	}

	 void Update(){
		if (!isLocalPlayer)
			return;
		float axisX = Input.GetAxis("Mouse X");
		float axisY = Input.GetAxis("Mouse Y");
		mouseX += axisX;
		mouseY += axisY;
		Quaternion q = Quaternion.Euler(-mouseY * mouseSensitive, mouseX * mouseSensitive, 0);
		carCamera.transform.localRotation = q;
		float t = Time.deltaTime;
		callSkills(t);
		skillsCoolDown(t);
	}

	[Command]
	private void CmdFire(Vector3 pos, Quaternion rot){
		// Debug.Log("Fire!!!!!!!!!!!");
		GameObject b = Instantiate(bullet, pos, rot);
		// b.transform.rotation = carCamera.transform.rotation;
		b.GetComponent<Rigidbody>().velocity = b.transform.forward * bulletSpeed;
		NetworkServer.Spawn(b);
		Destroy(b, 5);
	}

	// mouse right button to speed up, left and right to fire.
	private void callSkills(float deltaT){
		orientation = (FirstT.position - SecondT.position);
		orientation = new Vector3(orientation.x, 0, orientation.z).normalized;

		if (Input.GetMouseButtonDown(1)){
			missile = true;
			carCamera.fieldOfView /= 2;
		}else if (Input.GetMouseButtonUp(1)){
			missile = false;
			carCamera.fieldOfView *= 2;
		}

		if (Input.GetMouseButtonDown(0)){
			if (missile && missileNum >= 1){
				missileNum -= 1;
				labelMissile.text = "" + missileNum;
				if (spriteMissile.fillAmount >= 1){
					spriteMissile.fillAmount = 0;
				}
				CmdFire(carCamera.transform.position, carCamera.transform.rotation);
			}else if (!missile && spriteDash.fillAmount >= 1){
				// vBeforeDash = GetComponent<Rigidbody>().velocity.magnitude;
				dash = true;
				dashLeftTime = dashDurTime;
				spriteDash.fillAmount = 0;
			}
		}
		// else if (Input.GetMouseButtonUp(0)){
		// 	dash = false;
		// 	if (!missile){
		// 		Vector3 nowDir = GetComponent<Rigidbody>().velocity.normalized;
		// 		GetComponent<Rigidbody>().velocity = nowDir * vBeforeDash;
		// 	}
		// }

		if (dash){
			Debug.Log("Dashing!");
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
}
