using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Animal {

    public GameObject okPanel;
    public GameObject player;

    public float speed;
    
	void Start ()
    {
        okPanel.SetActive(false);
        size = (int)Random.Range(1, 4);
        Hunter.instance.objs_mob.Add(this.gameObject);
        player = GameObject.Find("Mouse");

        speed = transform.parent.GetComponent<botMove>().speed;
    }
	
	void Update ()
    {
        if (player != null)
        {
            if (size < player.GetComponent<Animal>().size)
                okPanel.SetActive(true);
            else
                okPanel.SetActive(false);
        }

        ScalingBot();
	}

    private void LateUpdate()
    {
        LookAtTarget();
    }

    void LookAtTarget()
    {
        Vector3 dir = transform.parent.GetComponent<botMove>().target - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mob" && collision.gameObject.GetComponent<Animal>().size <= size)
        {
            if (collision.gameObject.name == "Mouse")
            {
                Hunter.instance.objs_mob.Remove(collision.gameObject);
                Destroy(collision.gameObject);
            } else
            {
                Hunter.instance.objs_mob.Remove(collision.gameObject);
                Destroy(collision.gameObject.transform.parent.gameObject);
                MobSpawn.instance.spawnCount++;
            }
        }

        if (collision.gameObject.tag == "Gas")
        {
            StartCoroutine(Gas());
        }
    }

    IEnumerator Gas()
    {
        transform.parent.GetComponent<botMove>().speed -= transform.parent.GetComponent<botMove>().speed * 0.3f;
        yield return new WaitForSeconds(5.0f);
        transform.parent.GetComponent<botMove>().speed = speed;
        yield return null;
    }
}
