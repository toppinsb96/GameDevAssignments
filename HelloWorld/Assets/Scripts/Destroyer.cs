using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	public float destroyTime;
	public float lerpSpeed;

	bool destroying = false;
	Vector3 startingScale;
	float lerp = 0;

	void Start () {
		Invoke ("Destroy", destroyTime);	
	}

	void Destroy() {
		destroying = true;
		startingScale = transform.localScale;
	}
	
	void Update () {
		if (destroying) {
			lerp = Mathf.Min (1, lerp + lerpSpeed * Time.deltaTime);

			if (lerp > 0.99f) {
				Destroy (gameObject);
			}
			transform.localScale = Vector3.Lerp (startingScale, Vector3.zero, lerp);
		}
	}
}
