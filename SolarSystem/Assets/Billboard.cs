using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
	void Update () {
        Vector3 euler = transform.localRotation.eulerAngles;
        transform.LookAt(Camera.main.transform);
        euler.y = transform.localRotation.eulerAngles.y;
        
        transform.localRotation = Quaternion.Euler(euler);
    }
}
