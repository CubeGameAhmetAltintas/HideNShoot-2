using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoadDataModel
{
    public int ColorId;
    public Vector3 Position;
    public Quaternion Rotation;
    public EnemyDataModel[] EnemyDatas;
    public WorldItemDataModel[] EnvironmentDatas;
}