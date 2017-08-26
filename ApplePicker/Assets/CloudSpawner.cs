using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {
    public float yRange;
    public float spawnDelay;
    public GameObject cloud;

	void Start () {
        SpawnCloud();   
	}

    void SpawnCloud() {
        Invoke("SpawnCloud", spawnDelay + Random.Range(2, 5));
        var newCloud = GameObject.Instantiate(cloud);
        var cloudPos = transform.position;
        cloudPos.y += Random.Range(-yRange, yRange);
        newCloud.transform.position = cloudPos;
    }
}
