using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public float speed;
    public int size;

    void Update()
    {
        size = HunterManager.instance.maxSize;
        transform.position = Vector3.MoveTowards(transform.position, HunterManager.instance.HuntTarget().position, speed * Time.deltaTime);

        transform.localScale = new Vector3(2.5f * size, 2.5f * size, 2.5f * size);

        LookAtTarget();
    }

    void LookAtTarget()
    {
        Vector3 dir = HunterManager.instance.HuntTarget().position - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg * -1;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * speed);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.name == "Mouse")
        {
            HunterManager.instance.objs_mob.Remove(collider.gameObject);
            MobSpawn.instance.spawnCount++;
        } else if(collider.gameObject.tag == "Mob")
        {
            HunterManager.instance.objs_mob.Remove(collider.gameObject);
            Destroy(collider.gameObject.transform.parent.gameObject);
            MobSpawn.instance.spawnCount++;
        }
    }
}