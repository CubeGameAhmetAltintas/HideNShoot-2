using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleModel : ObjectModel
{
    public int Id;

    public WorldItemDataModel GetDataModel()
    {
        WorldItemDataModel dataModel = new WorldItemDataModel();
        dataModel.Id = Id;
        dataModel.Position = transform.position;
        dataModel.Rotation = transform.rotation;

        return dataModel;
    }

    public virtual void OnHitPlayer()
    {
        VibrateController.Controller.SetHaptic(VibrationTypes.Light);

    }

}
