using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool.Mesh
{
    public class DynamicMeshPointController : ControllerBaseModel
    {
        public MeshModel MeshModel;
        public List<MeshPointGroups> PointGroups;
        List<Vector3> vertices;
        List<int> triangles;

        public void ClearMesh()
        {
            if (MeshModel != null)
            {
                MeshModel.Clear();
            }
        }

        public void AddPointGroups(Vector3 center, Quaternion rotation, Vector3 scale, List<Vector3> points)
        {
            PointGroups.Add(new MeshPointGroups(center, rotation, scale, points));
        }

        public void AddPointGroups(Vector3 center, Quaternion rotation, Vector3 scale, Vector2 randomize, List<Vector3> points)
        {
            PointGroups.Add(new MeshPointGroups(center, rotation, scale, randomize, points));
        }

        public void CreateMesh()
        {
            vertices = new List<Vector3>();
            triangles = new List<int>();

            for (int i = 0; i < PointGroups.Count; i++)
            {
                MeshPointGroups pointGroup = PointGroups[i];
                vertices.AddRange(pointGroup.GetVertices());

                if (i == 0)
                {
                    triangles.AddRange(pointGroup.GetBottomCapTriangles(i));
                }
                else
                {
                    triangles.AddRange(pointGroup.GetSideTriangles(i));

                    if (i == PointGroups.Count - 1)
                        triangles.AddRange(pointGroup.GetTopCapTriangles(i));
                }
            }

            MeshModel.CreateMesh(vertices.ToArray(), triangles.ToArray());
        }
    }

    [System.Serializable]
    public class MeshPointGroups
    {
        public Vector3 Center;
        public Quaternion Rotation;
        public Vector3 Scale;
        public List<MeshPointModel> Points;

        public MeshPointGroups(Vector3 center, Quaternion rotation, Vector3 scale, List<Vector3> points)
        {
            Center = center;
            Rotation = rotation;
            Points = new List<MeshPointModel>();
            Scale = scale;

            for (int i = 0; i < points.Count; i++)
            {
                Points.Add(new MeshPointModel(rotation * ((center + points[i]) - center) + center));
            }
        }

        public MeshPointGroups(Vector3 center, Vector3 localEulerAngles, Vector3 scale, List<Vector3> points)
        {
            Center = center;
            Rotation = Quaternion.Euler(localEulerAngles);
            Points = new List<MeshPointModel>();
            Scale = scale;

            for (int i = 0; i < points.Count; i++)
            {
                Points.Add(new MeshPointModel(Rotation * ((center + points[i]) - center)));
            }
        }

        public MeshPointGroups(Vector3 center, Quaternion rotation, Vector3 scale, Vector2 randomizeValue, List<Vector3> points)
        {
            Center = center;
            Rotation = rotation;
            Points = new List<MeshPointModel>();
            Scale = scale;

            for (int i = 0; i < points.Count; i++)
            {
                Points.Add(new MeshPointModel(rotation * ((center + points[i]) - center), Random.Range(randomizeValue.x, randomizeValue.y)));
            }
        }

        public MeshPointGroups(Vector3 center, Vector3 localEulerAngles, Vector3 scale, Vector2 randomizeValue, List<Vector3> points)
        {
            Center = center;
            Rotation = Quaternion.Euler(localEulerAngles);
            Points = new List<MeshPointModel>();
            Scale = scale;

            for (int i = 0; i < points.Count; i++)
            {
                Points.Add(new MeshPointModel(Rotation * ((center + points[i]) - center), Random.Range(randomizeValue.x, randomizeValue.y)));
            }
        }

        public MeshPointGroups(Vector3 center, Quaternion rotation, MeshPointGroups sample)
        {
            Center = center;
            Rotation = rotation;
            Scale = sample.Scale;
            Points = new List<MeshPointModel>();

            for (int i = 0; i < sample.Points.Count; i++)
            {
                Points.Add(new MeshPointModel(rotation * ((Center + sample.Points[i].LocalPosition) - Center)));
            }
        }

        public MeshPointGroups(Vector3 center, Vector3 rotation, MeshPointGroups sample)
        {
            Center = center;
            Rotation = Quaternion.Euler(rotation);
            Points = new List<MeshPointModel>();
            Scale = sample.Scale;

            for (int i = 0; i < sample.Points.Count; i++)
            {
                Points.Add(new MeshPointModel(Rotation * ((Center + sample.Points[i].LocalPosition) - Center)));
            }
        }

        public MeshPointGroups(Vector3 center, MeshPointGroups sample)
        {
            Center = center;
            Rotation = sample.Rotation;
            Points = new List<MeshPointModel>();
            Scale = sample.Scale;

            for (int i = 0; i < sample.Points.Count; i++)
            {
                Points.Add(new MeshPointModel(Rotation * ((Center + sample.Points[i].LocalPosition) - Center)));
            }
        }

        public MeshPointGroups(MeshPointGroups sample)
        {
            Center = sample.Center;
            Rotation = sample.Rotation;
            Points = new List<MeshPointModel>();
            Scale = sample.Scale;

            for (int i = 0; i < sample.Points.Count; i++)
            {
                Points.Add(new MeshPointModel(Rotation * ((Center + sample.Points[i].LocalPosition) - Center)));
            }
        }

        public List<int> GetTopCapTriangles(int layerIndex)
        {
            int diff = Points.Count * layerIndex;
            List<int> triangles = new List<int>();

            if (Points.Count == 3)
            {
                triangles.Add(diff);
                triangles.Add(diff + 1);
                triangles.Add(diff + 2);
            }
            else if (Points.Count == 4)
            {
                triangles.Add(diff);
                triangles.Add(diff + 2);
                triangles.Add(diff + 3);

                triangles.Add(diff);
                triangles.Add(diff + 1);
                triangles.Add(diff + 2);
            }
            else if (Points.Count > 4)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    if (i < Points.Count - 1)
                    {
                        triangles.Add(diff + i);
                        triangles.Add(diff + (i + 1));
                        triangles.Add(diff + (Points.Count - 1));
                    }
                    else
                    {
                        triangles.Add(diff + (i - 1));
                        triangles.Add(diff);
                        triangles.Add(diff + (Points.Count - 1));
                    }
                }
            }

            return triangles;
        }

        public List<int> GetBottomCapTriangles(int layerIndex)
        {
            int diff = Points.Count * layerIndex;
            List<int> triangles = new List<int>();

            if (Points.Count == 3)
            {
                triangles.Add(diff);
                triangles.Add(diff + 1);
                triangles.Add(diff + 2);
            }
            else if (Points.Count == 4)
            {
                triangles.Add(diff);
                triangles.Add(diff + 2);
                triangles.Add(diff + 3);

                triangles.Add(diff);
                triangles.Add(diff + 1);
                triangles.Add(diff + 2);
            }
            else if (Points.Count > 4)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    if (i < Points.Count - 1)
                    {
                        triangles.Add(diff + (Points.Count - 1));
                        triangles.Add(diff + (i + 1));
                        triangles.Add(diff + i);
                    }
                    else
                    {
                        triangles.Add(diff + (Points.Count - 1));
                        triangles.Add(diff);
                        triangles.Add(diff + (i - 1));
                    }
                }
            }

            return triangles;
        }

        public List<int> GetSideTriangles(int layerIndex)
        {
            int diff = Points.Count * layerIndex;
            int prevDiff = Points.Count * (layerIndex - 1);
            List<int> triangles = new List<int>();

            if (prevDiff >= 0)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    if (i < Points.Count - 1)
                    {
                        triangles.Add(prevDiff + i);
                        triangles.Add(diff + i + 1);
                        triangles.Add(diff + i);

                        triangles.Add(prevDiff + i);
                        triangles.Add(prevDiff + i + 1);
                        triangles.Add(diff + i + 1);
                    }
                    else
                    {
                        triangles.Add(diff + Points.Count - 1);
                        triangles.Add(prevDiff);
                        triangles.Add(diff);

                        triangles.Add(diff + Points.Count - 1);
                        triangles.Add(prevDiff + Points.Count - 1);
                        triangles.Add(prevDiff);
                    }
                }
            }

            return triangles;
        }

        public Vector3[] GetVertices()
        {
            Vector3[] vertices = new Vector3[Points.Count];

            for (int i = 0; i < Points.Count; i++)
            {
                //vertices[i] = (Rotation * (Points[i].GetPosition(Center, Scale) - Center) + Center);

                vertices[i] = Points[i].GetPosition(Center, Scale);
            }

            return vertices;
        }

    }
}