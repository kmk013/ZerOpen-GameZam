using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public List<GameObject> objs_mob = new List<GameObject>();
    public GameObject overwrite;
    public Transform target;
    public float speed;

    private void Start()
    {
        objs_mob = new List<GameObject>(GameManager.Instance.obj_list);
        StartCoroutine(TargetSetting());
    }

    void Update()
    {
        objs_mob = new List<GameObject>(GameManager.Instance.obj_list);

        HunterSetting();
        
        LookAtTarget();
        OverwriteSetting();
    }

    void HunterSetting()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.localScale = new Vector3(2.5f * target.GetComponent<Animal>().size,
            2.5f * target.GetComponent<Animal>().size,
            2.5f * target.GetComponent<Animal>().size);
    }

    void LookAtTarget()
    {
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg * -1;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * speed * Time.deltaTime);
    }

    void OverwriteSetting()
    {
        overwrite.transform.position = target.position;
        overwrite.transform.localScale = target.localScale;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {   
        if(collider.gameObject.tag.Equals("Mob"))
        {
            objs_mob.Remove(collider.gameObject);

            if(collider.gameObject.name.Equals("Mouse")){}
            else
            {
                Destroy(collider.gameObject);
            }
            MobSpawn.Instance.spawnCount++;
        }
    }

    IEnumerator TargetSetting()
    {
        while(true)
        {
            objs_mob.Sort(delegate (GameObject a, GameObject b)
            {
                if (a.GetComponent<Animal>().size > b.GetComponent<Animal>().size)
                    return -1;
                else if (a.GetComponent<Animal>().size < b.GetComponent<Animal>().size)
                    return 1;
                return 0;
            });
            target = objs_mob[0].transform;
            yield return null;
        }
    }
}