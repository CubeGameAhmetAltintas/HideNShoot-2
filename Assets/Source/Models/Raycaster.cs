using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public Vector3 HitPoint(Ray ray, string tag, int layerMask)
    {
        int mask = 1 << layerMask;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, mask))
        {
            if (string.IsNullOrEmpty(tag))
            {
                return hit.point;
            }
            else
            {
                if (hit.transform.CompareTag(tag))
                    return hit.point;
            }
        }

        return Vector3.zero;
    }
}