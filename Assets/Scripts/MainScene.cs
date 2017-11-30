using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour {
    
    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }

	void Update () {
        if(Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(1);
	}
}
