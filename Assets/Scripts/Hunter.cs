using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public static Hunter instance = null;
    public List<GameObject> objs_mob = new List<GameObject>();
    public Transform target;
    public float speed;

    private float targetRetime = 5.0f;
    private float targetChktime = 0.0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        if (!target || targetChktime >= targetRetime)
            HuntTarget();

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        transform.localScale = new Vector3(2.5f * target.GetComponent<Animal>().size,
            2.5f * target.GetComponent<Animal>().size,
            2.5f * target.GetComponent<Animal>().size);

        LookAtTarget();
    }

    void LookAtTarget()
    {
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg * -1;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Mob")
        {
            targetChktime += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.name == "Mouse")
        {
            objs_mob.Remove(collider.gameObject);
            MobSpawn.instance.spawnCount++;
        } else if(collider.gameObject.tag == "Mob")
        {
            objs_mob.Remove(collider.gameObject);
            Destroy(collider.gameObject.transform.parent.gameObject);
            MobSpawn.instance.spawnCount++;
        }
    }

    void HuntTarget()
    {
        if (objs_mob.Count == 0)
        {
            return;
        }

        objs_mob.Sort(delegate (GameObject a, GameObject b)
        {
            if (a.GetComponent<Animal>().size > b.GetComponent<Animal>().size)
                return -1;
            else if (a.GetComponent<Animal>().size < b.GetComponent<Animal>().size)
                return 1;
            return 0;
        });

        target = objs_mob[0].transform;
    }
}