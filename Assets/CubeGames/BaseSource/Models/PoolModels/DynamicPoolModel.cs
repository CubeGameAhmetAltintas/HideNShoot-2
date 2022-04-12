using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPoolModel : PoolModel
{
    public ObjectModel Sample;
    public int MaxInstantiateCount;
    public int InstantiateCount;

    public override ObjectModel GetDeactiveItem()
    {
        ObjectModel model = base.GetDeactiveItem();
        if (model == null)
        {
            if (InstantiateCount < MaxInstantiateCount)
            {
                model = Instantiate(Sample, transform);
                Items.Add(model);
            }
        }

        return model;
    }
}
