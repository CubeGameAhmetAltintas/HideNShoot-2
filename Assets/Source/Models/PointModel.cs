using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointModel : ObjectModel
{
    public bool IsActive;
    public Vector3 StartPosition;

    public override void SetActive()
    {
        base.SetActive();
        IsActive = true;
    }

    public override void SetDeactive()
    {
        base.SetDeactive();
        IsActive = false;
    }
}
