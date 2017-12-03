using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
    
    [HideInInspector]
    public int size;

    public void ScalingBot()
    {
        transform.parent.transform.localScale = new Vector3(size * 0.75f, size * 0.75f, size * 0.75f);
    }

    public void ScalingPlayer()
    {
        transform.localScale = new Vector3(size, size, size);
        Camera.main.GetComponent<Camera>().orthographicSize = size * 100;
    }
}
