using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class PointerController
{
    public Vector2 PointerDownPosition;
    public Vector2 PointerPosition;
    public Vector2 PointerUpPosition;
    public Vector2 DragPosition;
    public Vector2 DeltaPosition
    {
        get
        {
            return new Vector2((PointerPosition.x - PointerDownPosition.x) / Screen.width, (PointerPosition.y - PointerDownPosition.y) / Screen.height);
        }
    }

    public float DragDetectSensitive;
    public float DragSensitive;
    public UnityEvent OnPointerDownEvent;
    public UnityEvent OnPointerEvent;
    public UnityEvent OnPointerUpEvent;
    public UnityEvent OnDragStart;
    public DragEvent OnPointerDragEvent;
    public UnityEvent OnDragEnd;
    bool isDraging;

    public void ControllerUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }

        if (Input.GetMouseButton(0))
        {
            OnPointer();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnPointerUp();
        }
    }

    public void OnPointerDown()
    {
        PointerDownPosition = Input.mousePosition;
        if (OnPointerDownEvent != null)
            OnPointerDownEvent.Invoke();
    }

    public void OnPointer()
    {
        PointerPosition = Input.mousePosition;
        if (OnPointerEvent != null)
            OnPointerEvent.Invoke();

        if (isDraging == false)
        {
            if (Vector2.Distance(PointerDownPosition, PointerPosition) * DragDetectSensitive >= 1)
            {
                OnPointerBeginDrag();
            }
        }
        else
        {
            OnPointerDrag();
        }
    }

    public void OnPointerUp()
    {
        PointerUpPosition = Input.mousePosition;
        if (OnPointerUpEvent != null)
            OnPointerUpEvent.Invoke();

        if (isDraging)
            OnPointerEndDrag();
    }

    public void OnPointerBeginDrag()
    {
        isDraging = true;
        if (OnDragStart != null)
            OnDragStart.Invoke();
    }

    public void OnPointerDrag()
    {
        DragPosition = (PointerPosition - PointerDownPosition).normalized * DragSensitive;
        if (OnPointerDragEvent != null)
            OnPointerDragEvent.Invoke(DragPosition);
    }

    public void OnPointerEndDrag()
    {
        isDraging = false;
        DragPosition = Vector2.zero;
        if (OnDragEnd != null)
            OnDragEnd.Invoke();
    }

    [System.Serializable]
    public class DragEvent : UnityEvent<Vector2> { }

}
