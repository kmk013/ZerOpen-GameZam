using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour {

    private GameObject player;
    public float speed;

	void Start () {
        player = GameObject.Find("Mouse");
        transform.rotation = player.transform.rotation;
        Destroy(this.gameObject, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.up * -1 * Time.deltaTime * speed;
	}
}
