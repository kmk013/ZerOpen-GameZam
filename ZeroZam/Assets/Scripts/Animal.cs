using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
    
    public int size;
    Vector3 startScale;

    private void Start()
    {
        startScale = transform.localScale;
    }

    public void ScalingBot()
    {
        transform.parent.transform.localScale = new Vector3(size * 0.75f, size * 0.75f, size * 0.75f);
    }

    public void ScalingPlayer()
    {
        transform.localScale = new Vector3(size, size, size);
    }
}
