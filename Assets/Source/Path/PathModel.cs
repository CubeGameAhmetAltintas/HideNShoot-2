using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathModel
{
    public int Id;
    public List<Vector3> Points;
#if UNITY_EDITOR
    public bool E_IsSelected, E_EditWithBezier;
    public Vector3 E_StartPos, E_EndPos, E_ControlPos, E_ControlPos1;
#endif

    public int MoveObjectToPath(int currentPathIndex, Transform target, float followSpeed, float rotationSpeed, Action onComplete)
    {
        target.position = Vector3.MoveTowards(target.position, Points[currentPathIndex], followSpeed * Time.deltaTime);
        lookNextPath(currentPathIndex, target, rotationSpeed);

        if (Vector3.Distance(target.position, Points[currentPathIndex]) < 0.2f)
        {
            if (currentPathIndex + 1 < Points.Count)
            {
                currentPathIndex++;
            }
            else
            {
                onComplete?.Invoke();
            }
        }

        return currentPathIndex;
    }

    private void lookNextPath(int currentPathIndex, Transform target, float rotationSpeed)
    {
        if (currentPathIndex < Points.Count)
        {
            Vector3 diff = Points[currentPathIndex] - target.position;
            if (Vector3.Distance(target.position, Points[currentPathIndex]) > 0.1f)
                target.rotation = Quaternion.RotateTowards(target.rotation, Quaternion.LookRotation(diff), rotationSpeed * Time.deltaTime);
        }
    }

}
