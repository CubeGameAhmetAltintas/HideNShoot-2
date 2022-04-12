using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : ControllerBaseModel
{
    [SerializeField] MultiplePoolModel RoadPools;

    public void OnLevelCompleted()
    {
    }

    public void LoadLevel(Level level)
    {
        for (int i = 0; i < level.RoadDatas.Length; i++)
        {
            WorldItemDataModel roadData = level.RoadDatas[i];

            RoadModel roadModel = RoadPools.Pools[roadData.Id].GetDeactiveItem() as RoadModel;
            roadModel.transform.position = roadData.Position;
            roadModel.transform.rotation = roadData.Rotation;
            roadModel.SetActive();
        }
    }

    public void E_SetRoad(int roadCount)
    {
        RoadPools.ResetPool();

        for (int i = 0; i < roadCount; i++)
        {
            RoadModel roadModel = RoadPools.Pools[0].Items.Find(x => x.gameObject.activeInHierarchy == false) as RoadModel;
            roadModel.transform.position = new Vector3(0, 0, i * 20);
            roadModel.gameObject.SetActive(true);
        }
    }

    public void E_LoadLevel(Level level)
    {
        for (int i = 0; i < level.RoadDatas.Length; i++)
        {
            WorldItemDataModel roadData = level.RoadDatas[i];

            RoadModel roadModel = RoadPools.Pools[roadData.Id].Items.Find(x => x.gameObject.activeInHierarchy == false) as RoadModel;
            roadModel.transform.position = roadData.Position;
            roadModel.transform.rotation = roadData.Rotation;
            roadModel.gameObject.SetActive(true);
        }
    }
}
