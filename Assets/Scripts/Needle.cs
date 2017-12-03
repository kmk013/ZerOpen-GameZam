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
	
	void Update () {
        transform.position += transform.up * -1 * Time.deltaTime * speed;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Mob")
        {
            if(collision.gameObject.GetComponent<Animal>().size > 1)
                collision.gameObject.GetComponent<Animal>().size--;
        }
        Destroy(this.gameObject);
    }
}
