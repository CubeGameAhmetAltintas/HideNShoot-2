using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialValueUpdater : ObjectModel
{
    public string PropertyName;
    public float Speed;
    public bool IsActive;
    public Material Material;
    public float Min, Max;
    float target;
    float value;

    private void Update()
    {
        if (IsActive)
        {
            Material.SetFloat(PropertyName, value);
            value = Mathf.MoveTowards(value, target, Speed);

            if (value == target)
            {
                if (target == Min)
                {
                    target = Max;
                }
                else
                {
                    target = Min;
                }
            }
        }
    }

}
