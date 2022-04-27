using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadModel : ObjectModel
{
    public int Id;
    public Transform EndPoint;

    [SerializeField] MeshRenderer roadRenderer;
    public Color TargetColor;
    private List<EnemyModel> spawnedEnemies;
    private PlayerController player;

    public void Initialize(RoadDataModel data, List<EnemyModel> enemies)
    {
        TargetColor = GameController.GetAreaColor(data.ColorId);
        transform.position = data.Position;
        transform.rotation = data.Rotation;

        spawnedEnemies = enemies;
        roadRenderer.material.color = TargetColor;
        SetActive();
    }

    public void SetEnemy(EnemyModel enemy, EnemyDataModel enemyData)
    {
        enemy.transform.position = enemyData.Position;
        enemy.transform.rotation = enemyData.Rotation;
        enemy.StateUpdate(EnemyStates.Idle);
        enemy.EnemyIdlePoint();
        enemy.SetActive();
    }

    public void SetEnvironment(EnvironmentModel environment, WorldItemDataModel enironmentData, RoadDataModel data)
    {
        environment.transform.position = enironmentData.Position;
        environment.transform.rotation = enironmentData.Rotation;
        Component[] meshRenderers = environment.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer item in meshRenderers)
            item.material.color = GameController.GetAreaColor(data.ColorId);
        environment.SetActive();
    }

    public void OnPlayerEnter(PlayerController playerController)
    {
        player = playerController;
        foreach (var item in spawnedEnemies)
        {
            item.SetPlayer(player);
        }
        OnPlayerColorChange(player.CurrentColor);
    }

    public void OnPlayerExit()
    {
        foreach (var enemy in spawnedEnemies)
            enemy.StateUpdate(EnemyStates.Idle);

    }

    public void RoadUpdate()
    {
        if (spawnedEnemies == null) return;

        foreach (var enemy in spawnedEnemies)
            enemy.EnemyUpdate();
    }

    public void OnPlayerColorChange(Color currentColor)
    {
        foreach (var enemy in spawnedEnemies)
        {
            if (Helpers.Colors.IsColorInRange(GameController.EnemyDetectSensitve, currentColor, TargetColor))
                enemy.StateUpdate(EnemyStates.Idle);
            else
                enemy.StateUpdate(EnemyStates.Shoot);
        }
    }

    public RoadDataModel GetDataModel()
    {
        RoadDataModel dataModel = new RoadDataModel();

        dataModel.Position = transform.position;
        dataModel.Rotation = transform.rotation;

        return dataModel;
    }
}