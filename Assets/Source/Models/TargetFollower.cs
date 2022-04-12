using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : ObjectModel
{
    [SerializeField] Transform target;
    [SerializeField] float speed;

    public void CameraUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, target.position.x, speed);
        pos.x = Mathf.Clamp(pos.x, -1, 1);

        transform.position = pos;

    }
}
