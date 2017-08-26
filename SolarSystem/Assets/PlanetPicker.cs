using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPicker : MonoBehaviour {
    public float moveSpeed;
    public Transform restingTransform;

    Transform desiredTransform;
    float lerp;

	void Start () {
        MoveTo(restingTransform);
	}

    void MoveTo(Transform t) {
        desiredTransform = t;
        lerp = 0;
    }
	
	void Update () {
        lerp = Mathf.Min(1, lerp + moveSpeed * Time.deltaTime);
       
        
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + transform.forward);
        var dir = mousePos - transform.position;
        dir.Normalize();
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;
        Transform raycastPlanet = null;
        int planetMask = LayerMask.GetMask("Planet");
        if (Physics.Raycast(ray, out hit, 500000, planetMask))
        {
            raycastPlanet = hit.collider.gameObject.transform;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!raycastPlanet) {
                MoveTo(restingTransform);
            }
            else
            {
                MoveTo(raycastPlanet.Find("CamPos"));
            }
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredTransform.rotation, lerp);
        transform.position = Vector3.Lerp(transform.position, desiredTransform.position, lerp);
	}
}
