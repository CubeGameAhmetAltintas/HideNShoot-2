using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EditableMeshCopy : ObjectModel
{
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] Mesh targetMesh;
    [SerializeField] MeshCollider meshCollider;
    [SerializeField] int count;
    Mesh createdMesh;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    float passed;
    bool isClear;

    public void Hit(BoxCollider collider)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if (collider.bounds.Contains(transform.TransformPoint(vertices[i])))
            {
                if (vertices[i].y == 0.9007069f)
                {
                    count++;
                    if (Time.time > passed + 1f)
                    {
                        passed = Time.time;
                    }
                }

                Vector3 diff = (transform.TransformPoint(vertices[i]) - collider.transform.position).normalized;
                vertices[i] = Vector3.Lerp(vertices[i], new Vector3(vertices[i].x, -2, vertices[i].z), 0.1f);
            }
        }

        UpdateMesh();
    }

    public void UpdateMesh()
    {
        createdMesh.vertices = vertices;
    }

    public void ResetMesh()
    {
        isClear = false;
        vertices = targetMesh.vertices.ToArray();
        triangles = targetMesh.triangles.ToArray();
        uvs = targetMesh.uv.ToArray();
        count = 0;
        Create();
    }

    public void Create()
    {
        createdMesh = new Mesh();
        createdMesh.vertices = vertices;
        createdMesh.triangles = triangles;
        createdMesh.RecalculateNormals();

        createdMesh.uv = uvs;
        meshFilter.mesh = createdMesh;
        if (isClear == false)
            meshCollider.sharedMesh = createdMesh;
    }

}
