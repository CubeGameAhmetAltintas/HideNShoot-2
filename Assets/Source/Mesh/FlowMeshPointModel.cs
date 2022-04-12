using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tool.Mesh
{
    [System.Serializable]
    public class FlowMeshPointModel 
    {
        public Vector3[] Points;

        public FlowMeshPointModel(Transform[] points)
        {
            Points = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                Points[i] = points[i].position;
            }
        }

        public List<int> GetTopCapTriangles(int layerIndex)
        {
            int diff = Points.Length * layerIndex;
            List<int> triangles = new List<int>();

            if (Points.Length == 3)
            {
                triangles.Add(diff);
                triangles.Add(diff + 1);
                triangles.Add(diff + 2);
            }
            else if (Points.Length == 4)
            {
                triangles.Add(diff);
                triangles.Add(diff + 2);
                triangles.Add(diff + 3);

                triangles.Add(diff);
                triangles.Add(diff + 1);
                triangles.Add(diff + 2);
            }
            else if (Points.Length > 4)
            {
                for (int i = 0; i < Points.Length; i++)
                {
                    if (i < Points.Length - 1)
                    {
                        triangles.Add(diff + i);
                        triangles.Add(diff + (i + 1));
                        triangles.Add(diff + (Points.Length - 1));
                    }
                    else
                    {
                        triangles.Add(diff + (i - 1));
                        triangles.Add(diff);
                        triangles.Add(diff + (Points.Length - 1));
                    }
                }
            }

            return triangles;
        }

        public List<int> GetBottomCapTriangles(int layerIndex)
        {
            int diff = Points.Length * layerIndex;
            List<int> triangles = new List<int>();

            if (Points.Length == 3)
            {
                triangles.Add(diff);
                triangles.Add(diff + 1);
                triangles.Add(diff + 2);
            }
            else if (Points.Length == 4)
            {
                triangles.Add(diff);
                triangles.Add(diff + 2);
                triangles.Add(diff + 3);

                triangles.Add(diff);
                triangles.Add(diff + 1);
                triangles.Add(diff + 2);
            }
            else if (Points.Length > 4)
            {
                for (int i = 0; i < Points.Length; i++)
                {
                    if (i < Points.Length - 1)
                    {
                        triangles.Add(diff + (Points.Length - 1));
                        triangles.Add(diff + (i + 1));
                        triangles.Add(diff + i);
                    }
                    else
                    {
                        triangles.Add(diff + (Points.Length - 1));
                        triangles.Add(diff);
                        triangles.Add(diff + (i - 1));
                    }
                }
            }

            return triangles;
        }

        public List<int> GetSideTriangles(int layerIndex)
        {
            int diff = Points.Length * layerIndex;
            int prevDiff = Points.Length * (layerIndex - 1);
            List<int> triangles = new List<int>();

            if (prevDiff >= 0)
            {
                for (int i = 0; i < Points.Length; i++)
                {
                    if (i < Points.Length - 1)
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
                        triangles.Add(diff + Points.Length - 1);
                        triangles.Add(prevDiff);
                        triangles.Add(diff);

                        triangles.Add(diff + Points.Length - 1);
                        triangles.Add(prevDiff + Points.Length - 1);
                        triangles.Add(prevDiff);
                    }
                }
            }

            return triangles;
        }

    }
}