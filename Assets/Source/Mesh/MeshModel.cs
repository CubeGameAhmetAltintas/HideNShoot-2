using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool.Mesh
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshModel : ObjectModel
    {
        public UnityEngine.Mesh CreatedMesh;
        [SerializeField] MeshFilter meshFilter;
        [SerializeField] MeshRenderer meshRenderer;
        Vector3[] vertices;

        public void Clear()
        {
            if (CreatedMesh != null)
                CreatedMesh.Clear();

            meshFilter.mesh = null;
        }

        public void CreateMesh(MeshData mesh)
        {
            CreateMesh(mesh.Vertices, mesh.Triangles, uvs: mesh.UVS);
        }


        public void CreateMesh(Vector3[] vertices, int[] triangles, Vector3[] normals = null, Vector2[] uvs = null)
        {
            this.vertices = vertices;
            CreatedMesh = new UnityEngine.Mesh();
            CreatedMesh.vertices = vertices;
            CreatedMesh.triangles = triangles;
            if (normals != null)
                CreatedMesh.normals = normals;
            else
                CreatedMesh.RecalculateNormals();

            if (uvs != null)
                CreatedMesh.uv = uvs;

            CreatedMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000);
            meshFilter.sharedMesh = CreatedMesh;
        }

        public void UpdateVertices(Vector3[] vertices)
        {
            CreatedMesh.vertices = vertices;
        }

        public void SetPositionToVertices(BoxCollider boxCollider, Vector3 position)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                if (boxCollider.bounds.Contains(vertices[i]))
                {
                    vertices[i] = position;
                }
            }
            CreatedMesh.vertices = vertices;
        }

        public void TranslateToVertices(BoxCollider boxCollider, Vector3 dir, float speed)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                if (boxCollider.bounds.Contains(transform.TransformPoint(vertices[i])))
                {
                    vertices[i] += dir * speed * Time.deltaTime;
                }
            }
            CreatedMesh.vertices = vertices;
        }

        private void Reset()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }
    }
}