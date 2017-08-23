using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	void Start () {
		
	}

	void OnTriggerEnter(Collider c) {
		if (c.tag == "DatApple") {
			var apple = c.gameObject.GetComponent<Apple> ();
			apple.Collect ();
		}
	}

	void Update () {
		var mousePos = Input.mousePosition;

		var pos = transform.position;
		pos = Camera.main.ScreenToWorldPoint (mousePos);
		pos.z = transform.position.z;

		transform.position = pos;
	}
}
