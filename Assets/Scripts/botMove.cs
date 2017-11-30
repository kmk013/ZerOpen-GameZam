using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botMove : MonoBehaviour {

	public float sleepTime;

	public float speed;

	private bool SleepOn;
	public bool mobTargeting; 

	public GameObject targetMob;
    
	public Vector3 target;

	void Awake() {
		SleepOn = false;
		mobTargeting = false;
		speed = Random.Range (2f, 4f);
	}

	void Start() {
		TargetSetting ();
	}

	void TargetSetting() {
		float TargetPosX = Random.Range (-(CameraMove.instance.mapSizeX / 2 + 8), (CameraMove.instance.mapSizeX / 2 - 8));
		float TargetPosY = Random.Range (-(CameraMove.instance.mapSizeY / 2 + 8), (CameraMove.instance.mapSizeY / 2 - 8));
		target = new Vector3 (TargetPosX, TargetPosY, 0);
	}

    void Update () {
		transform.position = Vector3.MoveTowards (transform.position, target, speed*Time.deltaTime);
		if (mobTargeting)
			return; 
		if ((target - transform.position).magnitude < 0.5f && !SleepOn) {
			StartCoroutine (SleepMove ());
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "Mob" && col.gameObject.GetComponent<Animal>().size < GetComponent<Animal>().size ) { // if(col.gameObject.tag == "Mob" && col.gameObject.GetComponent<Animal>().size < GetComponent<Animal>().size ) {
			targetMob = col.gameObject;
			mobTargeting = true;
			target = col.gameObject.transform.position;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (targetMob == null)
			return;
		if (col.gameObject == targetMob) {
			mobTargeting = false;
			targetMob = null;
		}
	}

	IEnumerator SleepMove() {
		SleepOn = true;
		yield return new WaitForSeconds (sleepTime);
		TargetSetting ();
		SleepOn = false;
		yield return null;
	}
}
