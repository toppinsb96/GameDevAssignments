using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
    public float speed;
    public float killX;
    public float blinkDelay;

    void Start () {
        Blink();
	}

    void Blink() {
        Invoke("Blink", blinkDelay + Random.Range(0, 3));
        GetComponent<Animator>().Play("CloudBlink");
    }
	
	void Update () {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if (transform.position.x > killX) {
            Destroy(gameObject);
        }
	}
}
