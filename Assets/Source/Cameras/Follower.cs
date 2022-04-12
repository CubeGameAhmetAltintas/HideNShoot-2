using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{

    public Transform Target;
    public Vector2 ClampValues;
    public float Speed;
    [SerializeField] Vector3 offset;
    public float CollectedHumanOffset;
    float value;

    private void Update()
    {
        value = Mathf.Lerp(value, CollectedHumanOffset, 0.1f);

        Vector3 pos = new Vector3(transform.position.x, Target.position.y + value, Target.position.z) + offset;
        pos.x = Mathf.Lerp(pos.x, Target.position.x + offset.x, Speed);
        pos.x = Mathf.Clamp(pos.x, ClampValues.x, ClampValues.y);
        transform.position = pos;
    }


}
