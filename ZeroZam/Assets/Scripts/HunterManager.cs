using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterManager : MonoBehaviour
{
    public static HunterManager instance = null;
    public List<GameObject> objs_mob;
    public int maxSize;
    public int maxIndex;

    void Awake()
    {
        objs_mob = new List<GameObject>();
        maxSize = 0;
        maxIndex = 0;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Transform HuntTarget()
    {
        if (objs_mob.Count == 0)
        {
            return null;
        }
        for (int i = 0; i < objs_mob.Count; i++)
        {
            if (maxSize < objs_mob[i].transform.GetComponent<Animal>().size)
            {
                maxSize = objs_mob[i].transform.GetComponent<Animal>().size;
                maxIndex = i;
            }
        }
        return objs_mob[maxIndex].transform;
    }


}