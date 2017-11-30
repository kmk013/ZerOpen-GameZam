using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterTarget : MonoBehaviour {

	void Update () {
        transform.position = HunterManager.instance.HuntTarget().position;
    }
}
