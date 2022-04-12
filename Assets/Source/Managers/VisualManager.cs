using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : ManagerBaseModel
{
    public static VisualManager Manager;
    [SerializeField] MultiplePoolModel fxPool;
    [SerializeField] Sprite[] upgradeIcons;

    public override void Initialize()
    {
        base.Initialize();
        Manager = this;
        fxPool.Initialize();
    }

    public T GetFx<T>(int poolIndex)
    {
        return fxPool.GetDeactiveItemByIndex<T>(poolIndex);
    }
}