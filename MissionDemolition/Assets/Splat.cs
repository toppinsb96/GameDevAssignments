using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour {
    public float splat_dampen;
    public Color splat_color;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        // create new colors array where the colors will be created.
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
            colors[i] = new Color(0, 0, 0, 0.8f);

        // assign the array of colors to the Mesh.
        mesh.colors = colors;
    }

    void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        if (other.tag != "Splattable")
            return;

        float splat_dist = Vector3.Magnitude(collision.relativeVelocity) / splat_dampen;
        var contactPoints = collision.contacts;
        foreach (var contactPoint in contactPoints) {
            var mesh = other.GetComponent<MeshFilter>().mesh;
            var mesh_verts = mesh.vertices;

            var mesh_colors = new Color[mesh_verts.Length];
            for (int i = 0; i < mesh_verts.Length; i++) {
                var vert = mesh_verts[i];
                vert = other.transform.TransformPoint(vert);

                mesh_colors[i] = mesh.colors[i];

                float dist = Vector3.Distance(vert, contactPoint.point);
                if (dist > splat_dist)
                    continue;

                Color splat = Color.Lerp(splat_color, new Color(splat_color.r, splat_color.g, splat_color.b), dist / splat_dist);
                if (mesh_colors[i].r == splat_color.r && mesh_colors[i].g == splat_color.g && mesh_colors[i].b == splat_color.b)
                { 
                    mesh_colors[i].a += splat.a;
                }
                else
                {
                    if (mesh_colors[i].a <= splat.a)
                        mesh_colors[i] = splat;
                }    
            }
            mesh.colors = mesh_colors;
        }
    }
}
