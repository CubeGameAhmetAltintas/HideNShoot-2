using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : ControllerBaseModel
{
    [SerializeField] MultiplePoolModel RoadPools;
    [SerializeField] MultiplePoolModel enemyPools;

    public void OnLevelCompleted()
    {
    }

    public void LoadLevel(Level level)
    {
        for (int i = 0; i < level.RoadDatas.Length; i++)
        {
            List<EnemyModel> enemies = new List<EnemyModel>();
            for (int j = 0; j < level.RoadDatas[i].EnemyDatas.Length; j++)
            {
                EnemyDataModel enemyData = level.RoadDatas[i].EnemyDatas[j];
                EnemyModel enemy = enemyPools.Pools[enemyData.Id].GetDeactiveItem() as EnemyModel;
                enemies.Add(enemy);
                (RoadPools.Pools[0].GetDeactiveItem() as RoadModel).SetEnemy(enemy, enemyData);
            }
            (RoadPools.Pools[0].GetDeactiveItem() as RoadModel).Initialize(level.RoadDatas[i], enemies);
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
        //for (int i = 0; i < level.RoadDatas.Length; i++)
        //{
        //    WorldItemDataModel roadData = level.RoadDatas[i];

        //    RoadModel roadModel = RoadPools.Pools[roadData.Id].Items.Find(x => x.gameObject.activeInHierarchy == false) as RoadModel;
        //    roadModel.transform.position = roadData.Position;
        //    roadModel.transform.rotation = roadData.Rotation;
        //    roadModel.gameObject.SetActive(true);
        //}
    }
}
