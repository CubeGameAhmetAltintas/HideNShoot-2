using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastController : ControllerBaseModel
{
    [System.Serializable]
    public class TrigEvent : UnityEvent { }

    public TrigEvent OnHit;
    [SerializeField] Camera camera;
    [SerializeField] LayerMask layer;
    [SerializeField] float maxDistance;
    [SerializeField] Transform center;
    [SerializeField] Vector3 pos;
    [SerializeField] Vector3 Offset;
    [SerializeField] RectTransform pointView;
    [SerializeField] Canvas canvas;
    [SerializeField] Vector2 viewOffset, scaledViewOffset;
    public bool lookTarget;
    public bool IsActive;
    public bool ClampPosition;
    [SerializeField] Vector3 clampMin, clampMax;


    public void ControllerUpdate()
    {
        if (IsActive)
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                Vector3 screenPos = Input.mousePosition + Offset;
                var ray = camera.ScreenPointToRay(screenPos);

                if (Physics.Raycast(ray, out hit, maxDistance, layer))
                {
                    pos = hit.point;
                    //pos.y = pos.y < -0.8f ? -0.8f : pos.y;
                    OnHit?.Invoke();
                }
            }

            if (ClampPosition)
                pos = clampUpdate(pos);

            if (ScreenManager.IsReScaled)
            {
                pointView.anchoredPosition = Helpers.Vectors.WorldToScreenPoint(camera, canvas, pos) + scaledViewOffset;
            }
            else
            {
                pointView.anchoredPosition = Helpers.Vectors.WorldToScreenPoint(camera, canvas, pos) + viewOffset;
            }

            transform.position = Vector3.Lerp(transform.position, pos, 0.1f);
            if (lookTarget)
                transform.LookAt(center);
        }
    }

    private Vector3 clampUpdate(Vector3 target)
    {
        if (clampMin.x != clampMax.x)
            target.x = Mathf.Clamp(target.x, clampMin.x, clampMax.x);

        if (clampMin.y != clampMax.y)
            target.y = Mathf.Clamp(target.y, clampMin.y, clampMax.y);

        if (clampMin.z != clampMax.z)
            target.z = Mathf.Clamp(target.z, clampMin.z, clampMax.z);

        return target;
    }

}


