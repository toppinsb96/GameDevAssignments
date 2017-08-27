using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sling : MonoBehaviour
{
    public float stretchResistance;
    public float restingWidth;

    LineRenderer lr;
    Transform startPoint;
    Transform endPoint;
    GameObject curProjectile;


    void Start()
    {
        lr = GetComponent<LineRenderer>();
        startPoint = transform.Find("StartPoint");
        endPoint = transform.Find("EndPoint");
    }

    public void SlingProjectile(GameObject projectile)
    {
        curProjectile = projectile;
    }

    public void FireProjectile() {
        Invoke("UnslingProjectile", 0.12f);
    }

    void UnslingProjectile() {
        curProjectile = null;
    }

    void Update()
    {
        if (!curProjectile)
        {
            lr.positionCount = 0;
            return;
        }

        var slingPoints = new List<Vector3>();

        slingPoints.Add(startPoint.position);
        for (int i = 0; i < curProjectile.transform.childCount; i++)
        {
            var slingPoint = curProjectile.transform.GetChild(i);
            if (slingPoint.gameObject.tag == "SlingPoint")
            {
                slingPoints.Add(slingPoint.position);
            }
        }
        slingPoints.Add(endPoint.position);

        lr.positionCount = slingPoints.Count;
        lr.SetPositions(slingPoints.ToArray());

        float shrinkage = Vector3.Distance(transform.position, curProjectile.transform.position) / stretchResistance;
        shrinkage = Mathf.Max(1, shrinkage);
        lr.startWidth = restingWidth / shrinkage;
        lr.endWidth = restingWidth / shrinkage;
    }
}
