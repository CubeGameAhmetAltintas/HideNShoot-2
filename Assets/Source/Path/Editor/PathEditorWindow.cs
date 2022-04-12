using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PathEditorWindow : EditorWindow
{
    Level activeLevel;
    Color lineColor;
    Color bezierColor;
    Color pointColor;
    float pointRadius;

    [MenuItem("Editors/Path Editor")]
    static void init()
    {
        PathEditorWindow window = GetWindow<PathEditorWindow>();
        window.Show();
    }

    void OnFocus()
    {
        SceneView.duringSceneGui -= this.OnSceneGUI;
        SceneView.duringSceneGui += this.OnSceneGUI;

    }

    private void OnEnable()
    {
        pointRadius = EditorPrefs.GetFloat("Path_PointRadius", 1);
        ColorUtility.TryParseHtmlString(EditorPrefs.GetString("Path_LineColor", ColorUtility.ToHtmlStringRGB(Color.white)), out lineColor);
        ColorUtility.TryParseHtmlString(EditorPrefs.GetString("Path_PointColor", ColorUtility.ToHtmlStringRGB(Color.red)), out pointColor);
    }

    void OnDestroy()
    {
        EditorPrefs.SetFloat("Path_PointRadius", pointRadius);
        EditorPrefs.SetString("Path_LineColor", ColorUtility.ToHtmlStringRGB(lineColor));
        EditorPrefs.SetString("Path_PointColor", ColorUtility.ToHtmlStringRGB(pointColor));
        SceneView.duringSceneGui -= this.OnSceneGUI;
    }

    private void OnSceneGUI(SceneView obj)
    {
        if (activeLevel != null)
        {
            drawPathScene(activeLevel.Path);
        }
    }

    private void drawPathScene(PathModel path)
    {
        for (int i = 0; i < path.Points.Count; i++)
        {
            if (i + 1 < path.Points.Count)
            {
                drawLine(path.Points[i], path.Points[i + 1], 1, lineColor);
            }

            drawSphere(path.Points[i], pointRadius, pointColor);
        }

        if (path.E_EditWithBezier)
        {
            path.E_StartPos = drawTransformHandle(path.E_StartPos);
            path.E_ControlPos = drawTransformHandle(path.E_ControlPos);
            path.E_ControlPos1 = drawTransformHandle(path.E_ControlPos1);
            path.E_EndPos = drawTransformHandle(path.E_EndPos);

            drawLine(path.E_StartPos, path.E_ControlPos, 1, bezierColor);
            drawLine(path.E_ControlPos, path.E_EndPos, 1, bezierColor);

            for (int i = 0; i < path.Points.Count; i++)
            {
                path.Points[i] = Helpers.Maths.CalculateCubicBezierPoint((float)i / (float)path.Points.Count, path.E_StartPos, path.E_ControlPos, path.E_ControlPos1, path.E_EndPos);
            }
        }
        else
        {
            for (int i = 0; i < path.Points.Count; i++)
            {
                if (i + 1 < path.Points.Count)
                {
                    Vector3 diff = path.Points[i + 1] - path.Points[i];
                    path.Points[i] = drawTransformHandle(path.Points[i], Quaternion.LookRotation(diff));
                }
                else
                {
                    if (path.Points.Count > 1)
                    {
                        Vector3 diff = path.Points[i] - path.Points[i - 1];
                        path.Points[i] = drawTransformHandle(path.Points[i], Quaternion.LookRotation(diff));
                    }
                    else
                    {
                        path.Points[i] = drawTransformHandle(path.Points[i], Quaternion.identity);
                    }
                }

            }
        }

    }

    private void drawLine(Vector3 aPoint, Vector3 bPoint, float thickness, Color color)
    {
        Color defColor = Handles.color;
        Handles.color = color;
        Handles.DrawLine(aPoint, bPoint, thickness);
        Handles.color = defColor;

    }

    private void drawSphere(Vector3 pos, float r, Color color)
    {
        Color defColor = Handles.color;
        Handles.color = color;
        Handles.SphereHandleCap(0, pos, Quaternion.identity, r, EventType.Repaint);
        Handles.color = defColor;
    }

    private Vector3 drawTransformHandle(Vector3 pos)
    {
        if (Tools.current == UnityEditor.Tool.Move)
        {
            EditorGUI.BeginChangeCheck();

            Handles.SetCamera(SceneView.lastActiveSceneView.camera);
            Vector3 position = Vector3.zero;

            position = Handles.PositionHandle(pos, Quaternion.identity);


            if (EditorGUI.EndChangeCheck())
            {
                pos = position;
            }
        }

        return pos;
    }

    private Vector3 drawTransformHandle(Vector3 pos, Quaternion rotation)
    {
        if (Tools.current == UnityEditor.Tool.Move)
        {
            EditorGUI.BeginChangeCheck();

            Handles.SetCamera(SceneView.lastActiveSceneView.camera);
            Vector3 position = Vector3.zero;

            position = Handles.PositionHandle(pos, rotation);


            if (EditorGUI.EndChangeCheck())
            {
                pos = position;
            }
        }

        return pos;
    }

    private void OnGUI()
    {
        if (activeLevel == null)
        {
            activeLevel = (Level)EditorGUILayout.ObjectField("Level", activeLevel, typeof(Level), true);

            return;
        }

        bezierColor = EditorGUILayout.ColorField("Bezier Color", bezierColor);
        lineColor = EditorGUILayout.ColorField("Line Color", lineColor);
        pointColor = EditorGUILayout.ColorField("Point Color", pointColor);
        pointRadius = EditorGUILayout.FloatField("Point Radius", pointRadius);

        drawPathsGUI();
    }

    private void drawPathsGUI()
    {
        drawPathGUI(activeLevel.Path, 0);
    }

    private void drawPathGUI(PathModel path, int index)
    {
        path.E_IsSelected = EditorGUILayout.Foldout(path.E_IsSelected, "Path-" + index);

        if (path.E_IsSelected)
        {
            path.E_StartPos = EditorGUILayout.Vector3Field("Start Pos", path.E_StartPos);
            path.E_ControlPos = EditorGUILayout.Vector3Field("Control1 Pos", path.E_ControlPos);
            path.E_ControlPos1 = EditorGUILayout.Vector3Field("Control2 Pos", path.E_ControlPos1);
            path.E_EndPos = EditorGUILayout.Vector3Field("End Pos", path.E_EndPos);
            
            if (GUILayout.Button(path.E_EditWithBezier ? "Edit With Single" : "Edit With Bezier"))
            {
                path.E_EditWithBezier = !path.E_EditWithBezier;
            }

            if (GUILayout.Button("Add Point"))
            {
                if (path.Points.Count > 0)
                {
                    path.Points.Add(path.Points.GetLastItem() + new Vector3(0, 0, 1));
                }
                else
                {
                    path.Points.Add(Vector3.zero);
                }
            }
        }

       
    }

}

