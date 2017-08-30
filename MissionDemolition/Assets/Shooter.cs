using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] projectiles;
    public GameObject spawnPoint;

    public float launchForce;
    public float minStretchDist;
    public float maxStretchDist;
    public float maxBotttomStretchAngle;
    public float maxTopStretchAngle;

    bool hover = false;
    GameObject curProjectile;
    Sling sling;

    void Start()
    {
        sling = transform.GetComponentInChildren<Sling>();
    }

    GameObject CreateProjectile() {
        int projectile_index = Random.Range(0, projectiles.Length);
        return GameObject.Instantiate(projectiles[projectile_index]);
    }

    void Update()
    {
        int shooterMask = LayerMask.GetMask("Shooter");
        var cam = Camera.main;
        var origin = cam.transform.position;
        var p = Input.mousePosition;
        p.z = cam.transform.position.x;
        var mousePos = cam.ScreenToWorldPoint(p);
        var direction = mousePos - cam.transform.position;
        direction.Normalize();
        hover = Physics.Raycast(new Ray(origin, direction), 20000, shooterMask);

        if (Input.GetMouseButtonDown(0) && hover)
        {
            curProjectile = CreateProjectile();
            sling.SlingProjectile(curProjectile);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (curProjectile)
            {
                var projectileToSpawn = spawnPoint.transform.position - curProjectile.transform.position;
                if (projectileToSpawn.magnitude > minStretchDist)
                {
                    var projectileRigidbody = curProjectile.GetComponent<Rigidbody>();
                    projectileRigidbody.isKinematic = false;
                    projectileRigidbody.AddForce(projectileToSpawn * launchForce);
                    curProjectile = null;
                    sling.FireProjectile();
                }
                else {
                    Destroy(curProjectile);
                    curProjectile = null;
                    sling.SlingProjectile(null);
                }
            }
        }

        if (curProjectile)
        {
            var projectilePos = mousePos;
            projectilePos.z = Mathf.Min(mousePos.z, spawnPoint.transform.position.z);
            
            var spawnToProjectile = projectilePos - spawnPoint.transform.position;
            if (spawnToProjectile.magnitude > maxStretchDist)
            {
                spawnToProjectile.Normalize();
                spawnToProjectile *= maxStretchDist;
            }
            float angle = Vector3.SignedAngle(spawnToProjectile, Vector3.forward, Vector3.right);
            float clampedAngle = angle;

            if (angle < 0 && Mathf.Abs(angle) < 180.0f - maxBotttomStretchAngle)
            {
                clampedAngle = -(180.0f - maxBotttomStretchAngle);
            }
            if (angle > 0 && Mathf.Abs(angle) < 180.0f - maxTopStretchAngle)
            {
                clampedAngle = (180.0f - maxTopStretchAngle);
            }
            spawnToProjectile = new Vector3(spawnToProjectile.x, Mathf.Sin(clampedAngle * Mathf.Deg2Rad), Mathf.Cos(clampedAngle * Mathf.Deg2Rad)) * spawnToProjectile.magnitude;
            projectilePos = spawnPoint.transform.position + spawnToProjectile;
            curProjectile.transform.position = projectilePos;
        }
    }
}
