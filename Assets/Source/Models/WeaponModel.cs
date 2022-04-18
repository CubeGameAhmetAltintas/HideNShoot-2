using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : ObjectModel
{
    public Transform Target => this .transform;
    [SerializeField] PoolModel bulletPool;

    public void WeaponUpdate()
    {
        (bulletPool.GetDeactiveItem() as BulletModel).SetActive();
    }
}
