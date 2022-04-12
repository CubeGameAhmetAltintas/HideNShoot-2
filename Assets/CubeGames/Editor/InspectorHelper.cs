using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class InspectorHelper : MonoBehaviour
{
    public GameObject Sample;

    public void ReplaceWithSample()
    {
#if UNITY_EDITOR
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            Vector3 localPos = transform.GetChild(0).localPosition;
            Quaternion localRotation = transform.GetChild(0).localRotation;
            DestroyImmediate(transform.GetChild(0).gameObject);

            GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Sample, transform);
            obj.transform.SetAsLastSibling();
            obj.transform.localPosition = localPos;
            obj.transform.localRotation = localRotation;
        }
#endif
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(InspectorHelper))]
public class EditorHelperInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("ReplaceWithSample"))
        {
            ((InspectorHelper)target).ReplaceWithSample();
        }
    }
}
#endif

