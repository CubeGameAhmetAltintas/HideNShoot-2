using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : ObjectModel
{
    public Vector3 StartPoint;
    public float MaxHeight;
    [SerializeField] float speed;
    float value;
    float targetValue;

    private void Update()
    {
        transform.localPosition = StartPoint + new Vector3(0, value, 0);
        value = Mathf.Lerp(value, targetValue, 0.1f);
    }

    public void IncreaseValue()
    {
        targetValue += speed;
        targetValue = targetValue > MaxHeight ? MaxHeight : targetValue;
    }

    public void DecreaseValue()
    {
        targetValue -= speed;
        targetValue = targetValue < 0 ? 0 : targetValue;
    }
}
