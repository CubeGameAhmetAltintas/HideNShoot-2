using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : CameraBaseModel
{
    public Vector3 PositionOffset;


    public override void CameraUpdate()
    {
        Vector3 pos = Target.position + PositionOffset;
        transform.position = Vector3.Lerp(transform.position, pos, 0.1f);
        base.CameraUpdate();
    }
}
