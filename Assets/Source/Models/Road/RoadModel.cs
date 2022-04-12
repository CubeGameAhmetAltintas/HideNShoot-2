using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadModel : ObjectModel
{
    public int Id;
    public Transform EndPoint;

    public WorldItemDataModel GetDataModel()
    {
        WorldItemDataModel dataModel = new WorldItemDataModel();
        dataModel.Id = Id;
        dataModel.Position = transform.position;
        dataModel.Rotation = transform.rotation;

        return dataModel;
    }
}