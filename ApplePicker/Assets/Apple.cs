using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {
	public GameObject point;
    public AudioClip landSound;
    public AudioClip collectSound;
    public int pointsToGive;
	public bool canCollect = true;

    void Start()
    {
        if (GameOver.gameOver.isGameOver)
            Destroy(gameObject);
    }

    public void Collect() {
		if (canCollect) {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(collectSound, 0.5f);
            Score.score.AddScore(pointsToGive);

			var newPoint = GameObject.Instantiate (point);
            newPoint.transform.position = transform.position;
			newPoint.transform.Find("PointDisplay").GetComponent<TextMesh> ().text = "+" + pointsToGive;
			Die ();
		}
	}

	public void Die() {
		if (canCollect) {
            canCollect = false;
			GetComponent<Animator>().Play ("AppleDeath");
            GetComponent<Rigidbody>().isKinematic = true;
			Invoke ("Destroy", 0.4f);
		}
	}

	void Destroy() {
		Destroy (gameObject);
	}
		
	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "DatGround") {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(landSound);
            Bucket.curBucket.NextBucket();
		}
	}
}
