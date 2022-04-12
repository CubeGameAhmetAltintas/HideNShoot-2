using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSetter : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Camera camera;

    private void Start()
    {
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                int index = lineRenderer.positionCount;
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(index, hit.point);
            }
        }
    }

}
