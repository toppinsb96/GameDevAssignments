using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubble : MonoBehaviour {
    public float gravity;
    public float speed;
    public float scaleDownSpeed;
    public float timeToDie;
    Vector3 velocity;

    float scaleLerp = 0;

    void Start() {
        Invoke("PleaseDie", timeToDie);
    }

    void PleaseDie() {
        // okay...
        Destroy(gameObject);
    }


    public void SetDirection(Vector3 dir) {
        // Mess with the direction a bit
        float angle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.left);
        angle += Random.Range(-40, 40);
        angle *= Mathf.Deg2Rad;
        velocity = new Vector3(0, Mathf.Sin(angle), Mathf.Cos(angle));
    }
	
	void FixedUpdate () {
        velocity.y += gravity * Time.deltaTime;
        transform.position += speed * velocity * Time.deltaTime;
        scaleLerp = Mathf.Min(1, scaleLerp + scaleDownSpeed * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, scaleLerp);
	}
}
