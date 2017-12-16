using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
    
    [HideInInspector]
    public int size;

    public void ScalingBot()
    {
        transform.localScale = new Vector3(size * 2f, size * 2f, size * 2f);
    }

    public void ScalingPlayer()
    {
        transform.localScale = new Vector3(size, size, size);
        Camera.main.GetComponent<Camera>().orthographicSize = size * 100;
    }
}
