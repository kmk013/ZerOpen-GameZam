using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	public static CameraMove instance=null;

	public Transform player;

	public float cameraSpeed; 

	public int mapSizeX;
	public int mapSizeY;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
	}

	void Move() {
		transform.position = Vector3.Lerp (player.position, transform.position, cameraSpeed*Time.deltaTime);
		transform.position = new Vector3 (transform.position.x, transform.position.y, -10);

		if (transform.position.x > ((mapSizeX / 2) - GetComponent<Camera>().orthographicSize*2))
			transform.position = new Vector3(((mapSizeX / 2) - GetComponent<Camera>().orthographicSize*2),transform.position.y,transform.position.z);
		
		if (transform.position.x < ((-mapSizeX / 2) + GetComponent<Camera>().orthographicSize*2))
			transform.position = new Vector3(((-mapSizeX / 2) + GetComponent<Camera>().orthographicSize*2),transform.position.y,transform.position.z);

		if (transform.position.y > ((mapSizeY / 2) - GetComponent<Camera>().orthographicSize))
			transform.position = new Vector3(transform.position.x,((mapSizeY / 2) - GetComponent<Camera>().orthographicSize),transform.position.z);

		if (transform.position.y < ((-mapSizeY / 2) + GetComponent<Camera>().orthographicSize))
			transform.position = new Vector3(transform.position.x,((-mapSizeY / 2) + GetComponent<Camera>().orthographicSize),transform.position.z);
		}
    
	void Update () {
		Move ();	
	}
}
