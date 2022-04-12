using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class ModelLineRenderer : ObjectModel
{
    public LineRenderer LineRenderer;
    public Vector3 Center;
    public Mesh Model;
    public Direction Direction;

    public void GetModelLine()
    {
        List<Vector3> list = new List<Vector3>();

        switch (Direction)
        {
            case Direction.X:
                for (int i = 0; i < Model.triangles.Length; i += 3)
                {
                    Vector2 interPoint = Vector2.zero;
                    Vector3[] vertices = new Vector3[]
                    {
                        Model.vertices[Model.triangles[i]],
                        Model.vertices[Model.triangles[i+1]],
                        Model.vertices[Model.triangles[i +2]]
                    };

                    if (Helpers.Vectors.GetIntersectionPoint(vertices[0], vertices[1], vertices[2], vertices[0], out interPoint))
                    {
                        if (list.Contains(new Vector3(Center.x, interPoint.y, interPoint.x)) == false)
                            list.Add(new Vector3(Center.x, interPoint.y, interPoint.x));
                    }
                }
                break;
            case Direction.Y:
                for (int i = 0; i < Model.triangles.Length; i += 3)
                {
                    Vector2 interPoint = Vector2.zero;
                    Vector3[] vertices = new Vector3[]
                    {
                        Model.vertices[Model.triangles[i]],
                        Model.vertices[Model.triangles[i+1]],
                        Model.vertices[Model.triangles[i +2]]
                    };

                    if (Helpers.Vectors.GetIntersectionPoint(vertices[0], vertices[1], vertices[2], vertices[0], out interPoint))
                    {
                        if (list.Contains(new Vector3(interPoint.x, Center.y, interPoint.y)) == false)
                            list.Add(new Vector3(interPoint.x, Center.y, interPoint.y));
                    }
                }
                break;
            case Direction.Z:
                for (int i = 0; i < Model.triangles.Length; i += 3)
                {
                    Vector2 interPoint = Vector2.zero;
                    Vector3[] vertices = new Vector3[]
                    {
                        Model.vertices[Model.triangles[i]],
                        Model.vertices[Model.triangles[i+1]],
                        Model.vertices[Model.triangles[i +2]]
                    };

                    if (Helpers.Vectors.GetIntersectionPoint(vertices[0], vertices[1], vertices[2], vertices[0], out interPoint))
                    {
                        if (list.Contains(new Vector3(interPoint.x, interPoint.y, Center.z)) == false)
                            list.Add(new Vector3(interPoint.x, interPoint.y, Center.z));
                    }
                }
                break;
            default:
                break;
        }

        LineRenderer.positionCount = list.Count;
        for (int i = 0; i < list.Count; i++)
        {
            LineRenderer.SetPosition(i, list[i]);
        }
    }


}

#if UNITY_EDITOR
[CustomEditor(typeof(ModelLineRenderer))]
public class ModelLineRendererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Get Line"))
        {
            ((ModelLineRenderer)target).GetModelLine();
        }
    }
}

#endif