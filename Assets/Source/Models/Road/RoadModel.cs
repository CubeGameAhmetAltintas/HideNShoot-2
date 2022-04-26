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
        TargetColor = data.TargetColor;
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
        enemy.ChangeState(EnemyStates.Idle);
        enemy.EnemyIdlePoint();
        enemy.SetActive();
    }

    public void SetEnvironment(EnvironmentModel environment, WorldItemDataModel enironmentData, RoadDataModel data)
    {
        environment.transform.position = enironmentData.Position;
        environment.transform.rotation = enironmentData.Rotation;
        Component[] meshRenderers = environment.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer item in meshRenderers)
            item.material.color = data.TargetColor;
        environment.SetActive();
    }

    public void OnPlayerEnter(PlayerController playerController)
    {
        player = playerController;
        foreach (var enemy in spawnedEnemies)
            enemy.ShootPlayer(player);
    }

    public void OnPlayerExit()
    {
        foreach (var enemy in spawnedEnemies)
            enemy.ChangeState(EnemyStates.Idle);

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
                enemy.ChangeState(EnemyStates.Idle);
            else
                enemy.ChangeState(EnemyStates.Shoot);
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