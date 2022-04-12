using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLineModel : ObjectModel
{
    public bool IsActive;
    public float Speed;
    public float TargetZ;
    public float Range;
    public float Min, Max;
    float target;

    private void Awake()
    {
        target = TargetZ - Range;
    }

    private void Update()
    {
        if (IsActive)
        {
            movementUpdate();
        }
    }

    private void movementUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = Mathf.MoveTowards(pos.z, target, Speed);
        transform.position = pos;
        if (pos.z == target)
        {
            setTarget();
        }
    }

    private void setTarget()
    {
        if (target == TargetZ + Max)
        {
            target = TargetZ + Min;
        }
        else
        {
            target = TargetZ + Max;
        }
    }

}
