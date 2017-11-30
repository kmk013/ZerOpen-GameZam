using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	public float speed;

	void Move() {
		float dx = Input.GetAxis ("Horizontal");
		float dy = Input.GetAxis ("Vertical");

		Vector3 dir = new Vector3 (dx,dy,0)*speed*Time.deltaTime;

        transform.localPosition += dir;
    }
    
	void Update () {
		Move ();
	}
}
