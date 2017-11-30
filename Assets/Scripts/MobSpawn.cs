using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour {
	public static MobSpawn instance = null;

	public int spawnCount;
	public float spawnIntervalTime;
    public List<GameObject> botList = new List<GameObject>();

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
	}

	void Start() {
		StartCoroutine(Spawn());
	}

	void SpawnBot() {
		Instantiate (botList[Random.Range(1,3)], new Vector3 (Random.Range((-CameraMove.instance.mapSizeX/2 + 8),(CameraMove.instance.mapSizeX/2 - 8)),Random.Range((-CameraMove.instance.mapSizeY/2 + 8),(CameraMove.instance.mapSizeY/2 - 8)),0),Quaternion.identity);
		spawnCount--;
	}

	IEnumerator Spawn() {
		while (true) {
			if(spawnCount>0)
				SpawnBot ();
			yield return new WaitForSeconds (spawnIntervalTime);
		}
	}
}
