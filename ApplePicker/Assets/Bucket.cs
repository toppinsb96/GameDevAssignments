using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {
    public Bucket nextBucket;
    public static Bucket curBucket;

    void Start() {
        curBucket = this;
    }

    public void NextBucket() {
        if (nextBucket.name == "DeadBucket")
        {
            GameOver.gameOver.BeginGameOver();
        }
        nextBucket.gameObject.SetActive(true);
        gameObject.SetActive(false);

        var nextBucketPos = nextBucket.transform.position;
        nextBucketPos.x = transform.position.x;
        nextBucket.transform.position = nextBucketPos;
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
        pos.y = transform.position.y;
		pos.z = transform.position.z;

		transform.position = pos;
	}
}
