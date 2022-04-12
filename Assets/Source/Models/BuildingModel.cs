using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingModel : ObjectModel
{
    public Vector2 MinMaxValue;

    private void OnEnable()
    {
        Vector3 pos = transform.position;
        pos.y = Random.Range(MinMaxValue.x, MinMaxValue.y);
        transform.position = pos;
    }
}
