using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool.Mesh
{
    public class FlowMeshController : ControllerBaseModel
    {
        public List<FlowMeshPointModel> MeshPoints;
        public FlowPoint FlowPoint;
        public MeshModel MeshModel;
        public bool IsActive;
        public BoxCollider boxCollider;
        float t;
                

        public override void Initialize()
        {
            base.Initialize();
            IsActive = true;
            FlowPoint.Initialize();
        }

        public void ClearMesh()
        {
            MeshModel.Clear();
        }

        private void Update()
        {
            if (IsActive)
            {
                if (Input.GetMouseButton(0))
                {
                    FlowPoint.FlowUpdate();
                    if (Time.time >= t + 0.075f)
                    {
                        AddPoint();
                        CreateMesh();
                    }
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                MoveVertices(boxCollider, Vector3.right, 10);
            }
        }

        public void OnCompletePath()
        {
            IsActive = false;
        }

        public void AddPoint()
        {
            FlowMeshPointModel pointModel = new FlowMeshPointModel(FlowPoint.Points);
            MeshPoints.Add(pointModel);
        }

        public void MoveVertices(BoxCollider collider, Vector3 dir, float speed)
        {
            for (int i = 0; i < MeshPoints.Count; i++)
            {
                for (int j = 0; j < MeshPoints[i].Points.Length; j++)
                {
                    if (collider.bounds.Contains(MeshModel.transform.TransformPoint(MeshPoints[i].Points[j])))
                    {
                        MeshPoints[i].Points[j] += dir * speed * Time.deltaTime;
                    }
                }
            }

            CreateMesh();
        }

        public void CreateMesh()
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            for (int i = 0; i < MeshPoints.Count; i++)
            {
                FlowMeshPointModel pointGroup = MeshPoints[i];
                vertices.AddRange(pointGroup.Points);

                if (i == 0)
                {
                    triangles.AddRange(pointGroup.GetBottomCapTriangles(i));
                }
                else
                {
                    triangles.AddRange(pointGroup.GetSideTriangles(i));

                    if (i == MeshPoints.Count - 1)
                        triangles.AddRange(pointGroup.GetTopCapTriangles(i));
                }
            }

            MeshModel.CreateMesh(vertices.ToArray(), triangles.ToArray());
        }
    }
}