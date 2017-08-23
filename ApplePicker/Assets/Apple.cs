using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {
	public GameObject point;
	public int pointsToGive;
	public bool canCollect = true;

	public void Collect() {
		if (canCollect) {
			var newPoint = GameObject.Instantiate (point);
			newPoint.transform.position = transform.position;
			newPoint.GetComponent<TextMesh> ().text = "+" + pointsToGive;
			Die ();
		}
	}

	public void Die() {
		if (canCollect) {
			canCollect = false;
			GetComponent<Animator> ().Play ("AppleDeath");
			Invoke ("Destroy", 0.3f);
		}
	}

	void Destroy() {
		Destroy (gameObject);
	}
		
	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "DatGround") {
			Die ();
		}
	}
}
