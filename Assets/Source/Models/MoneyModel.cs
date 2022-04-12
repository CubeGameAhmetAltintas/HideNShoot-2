using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyModel : ObjectModel
{
    public int Value;
    [SerializeField] Transform collectFx;

    public void OnCharacterCollect()
    {
        collectFx.transform.SetParent(GameController.Controller.FxGarbage);
        collectFx.SetActive(true);
        SetDeactive();
    }

    public WorldItemDataModel GetDataModel()
    {
        WorldItemDataModel dataModel = new WorldItemDataModel();
        dataModel.Position = transform.position;
        dataModel.Rotation = transform.rotation;

        return dataModel;
    }
}
