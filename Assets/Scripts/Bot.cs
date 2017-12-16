using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Animal {

    public Vector3 target;

    public float speed;
    
	void Start ()
    {
        size = (int)Random.Range(GameObject.Find("Mouse").GetComponent<Animal>().size - 1,
            GameObject.Find("Mouse").GetComponent<Animal>().size + 2);
        GameManager.Instance.obj_list.Add(this.gameObject);

        speed = Random.Range(120f, 240f);

        TargetSetting();
    }
	
	void Update ()
    {
        ScalingBot();
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        LookAtTarget();
    }

    void LookAtTarget()
    {
        Vector3 dir = target - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void TargetSetting()
    {
        float TargetPosX = Random.Range(-(GameManager.Instance.mapSizeX / 2 + 250), (GameManager.Instance.mapSizeX / 2 - 250));
        float TargetPosY = Random.Range(-(GameManager.Instance.mapSizeY / 2 + 250), (GameManager.Instance.mapSizeY / 2 - 250));
        target = new Vector3(TargetPosX, TargetPosY, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mob" && collision.gameObject.GetComponent<Animal>().size <= size)
        {
            if (collision.gameObject.name == "Mouse")
            {
                GameManager.Instance.obj_list.Remove(collision.gameObject);
                Destroy(collision.gameObject);
            } else
            {
                GameManager.Instance.obj_list.Remove(collision.gameObject);
                Destroy(collision.gameObject);
                MobSpawn.Instance.spawnCount++;
            }
        }

        if (collision.gameObject.tag == "Gas")
        {
            StartCoroutine(Gas());
        }
    }

    IEnumerator Gas()
    {
        float minusSpeed = speed * 0.3f;
        speed -= minusSpeed;
        yield return new WaitForSeconds(5.0f);
        speed += minusSpeed;
        yield return null;
    }
}
