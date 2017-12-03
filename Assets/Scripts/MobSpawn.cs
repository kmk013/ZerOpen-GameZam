using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : SingleTon<MobSpawn> {
	public int spawnCount;
	public float spawnIntervalTime;
    public List<GameObject> botList = new List<GameObject>();

	void Start() {
		StartCoroutine(Spawn());
	}

	void SpawnBot() {
		Instantiate (botList[Random.Range(1,3)], new Vector3 (Random.Range((-GameManager.Instance.mapSizeX/2 + 8),(GameManager.Instance.mapSizeX / 2 - 8)),
            Random.Range((-GameManager.Instance.mapSizeY / 2 + 8),(GameManager.Instance.mapSizeY / 2 - 8)),
            0),
            Quaternion.identity);
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
