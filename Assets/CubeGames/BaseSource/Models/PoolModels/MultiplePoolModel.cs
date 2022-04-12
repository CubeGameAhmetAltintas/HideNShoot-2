using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePoolModel : ObjectModel
{
    public List<PoolModel> Pools;
    [HideInInspector] public int Index;

    public override void Initialize()
    {
        base.Initialize();

        foreach (var item in Pools)
        {
            item.Initialize();
        }
    }

    public ObjectModel GetRandomDeactiveItem()
    {
        int index = Random.Range(0, Pools.Count);
        return Pools[index].GetDeactiveItem();
    }

    public T GetDeactiveItemByIndex<T>(int index)
    {
        return (T)(object)Pools[index].GetDeactiveItem();
    }

    public ObjectModel GetLinearDeactiveItem()
    {
        ObjectModel model = Pools[Index].GetDeactiveItem();
        Index = (Index + 1 < Pools.Count) ? Index + 1 : 0;
        return model;
    }

    public ObjectModel GetDeactiveItemByLuck(params float[] lucks)
    {
        int index = (int)Helpers.Maths.GetValueByLuck(lucks);
        return Pools[index];
    }

    [EditorButton]
    public void GetPools()
    {
        Pools.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            PoolModel pool = transform.GetChild(i).GetComponent<PoolModel>();
            if (pool != null)
            {
                Pools.Add(pool);
            }
        }
    }

    public void ResetPool()
    {
        foreach (var item in Pools)
        {
            item.SetDeactiveItems();
        }
    }

}
