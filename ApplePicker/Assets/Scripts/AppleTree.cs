using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour {
	public GameObject apple;

	// Speed of the tree
	public float speed;

	// Max distance apple tree can move
	public float leftAndRightEdge; 

	// Chance for the tree to change directions
	public float chanceToChangeDir;

	// Rate in which the tree drops apples
	public float appleDropRate;

	bool canJuke = true;

	int direction = 1;

	Transform appleSpawnPoint;

	float startX;

	void Start () {
		Invoke ("DropApple", appleDropRate);
		appleSpawnPoint = transform.Find ("AppleSpawnPoint");
		startX = transform.position.x;
	}

	void DropApple() {
		Invoke ("DropApple", appleDropRate);

		var newApple = GameObject.Instantiate (apple);
		newApple.transform.position = appleSpawnPoint.position;
	}


	void FixedUpdate() {
		if(canJuke && Random.value < chanceToChangeDir) {
			direction *= -1;
			canJuke = false;

			Invoke ("CanJuke", 0.3f);
		}
	}

	void CanJuke() {
		canJuke = true;
	}

	void Update () {
		var pos = transform.position;
		pos.x += direction * Time.deltaTime * speed;

		if (startX - transform.position.x < -leftAndRightEdge) {
			direction = -1;
		}

		if (startX - transform.position.x > leftAndRightEdge) {
			direction = 1;
		}

		transform.position = pos;
	}
}
