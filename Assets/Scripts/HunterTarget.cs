using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterTarget : MonoBehaviour {

	void Update () {
        transform.position = Hunter.instance.target.position;
        transform.localScale = Hunter.instance.target.localScale;
    }
}
