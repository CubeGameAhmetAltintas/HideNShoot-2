using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBaseModel : ObjectModel
{
    public Transform Target;
    public UpdateTypes UpdateType;

    public override void Initialize()
    {
        base.Initialize();
    }

    public virtual void CameraUpdate()
    {

    }

    private void Update()
    {
        if (UpdateType == UpdateTypes.Update)
        {
            CameraUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (UpdateType == UpdateTypes.FixedUpdate)
        {
            CameraUpdate();
        }
    }

    private void LateUpdate()
    {
        if (UpdateType == UpdateTypes.LateUpdate)
        {
            CameraUpdate();
        }
    }

}