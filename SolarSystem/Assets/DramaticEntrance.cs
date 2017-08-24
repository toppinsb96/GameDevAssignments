using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DramaticEntrance : MonoBehaviour {
	public float scaleSpeed;
	float scaleLerp;
	bool enter;

	// Use this for initialization
	void Start () {
		Invoke ("Enter", 11.0f);
	}

	void Enter() {
		enter = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (enter) {
			scaleLerp += Time.deltaTime * scaleSpeed;
			Vector3 one = new Vector3 (1, 1, 1);
			transform.localScale = Vector3.Lerp (one * 0.7f, one * 4.0f, scaleLerp);
			Camera.main.transform.position = Vector3.Lerp (new Vector3(0, 64.9f, 0), new Vector3(0, 35.69f, 0), scaleLerp);
		}
	}
}
