using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject[] objects;

	void Update () {
		int objIndex = Random.Range (0, objects.Length);
		var obj = objects [objIndex];
		GameObject.Instantiate (obj, transform);
	}
}
