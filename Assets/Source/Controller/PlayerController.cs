using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerBaseModel
{
    [SerializeField] float forwardSpeed;
    [SerializeField] float rotationSpeed;
    public CharacterModel character;
    [SerializeField] UpgradeController upgradeController;
    [SerializeField] ParticleSystem upgradeFx;
    int pointIndex;
    public int Health = 100;
    public int MaxHealth;
    private RoadModel currentRoad, lastRoad;
    [SerializeField] Material characterColor;
    [SerializeField] PlayerColorBar colorBar;
    [SerializeField] Transform aimBar;
    public Color CurrentColor;
    [SerializeField] HealthBar healthBar;
    [SerializeField] WeaponModel weaponModel;
    [SerializeField] Cinemachine.CinemachineVirtualCamera gameplayCamera;
    PathModel activePath
    {
        get
        {
            return LevelController.Controller.ActivePath;
        }
    }

    public void Initialize(Level level)
    {
        base.Initialize();
        MaxHealth = upgradeController.HealthUpgrade.CurrentHealth;
        Health = MaxHealth;

        List<Color> colors = new List<Color>();
        for (int i = 0; i < level.RoadDatas.Length; i++)
        {
            bool isExist = false;
            if (colors.Count != 0)
            {
                for (int j = 0; j < colors.Count; j++)
                {
                    if (Helpers.Colors.IsEqual(colors[j],GameController.GetAreaColor(level.RoadDatas[i].ColorId)))
                    {
                        isExist = true;
                    }
                }
            }

            if (isExist == false)
            {
                colors.Add(GameController.GetAreaColor(level.RoadDatas[i].ColorId));
            }
        }
        colorBar.Initialize(colors.ShuffleList());
    }

    public void OnUpgrade(int upgradeId)
    {
        if (upgradeId == 0)
        {
            MaxHealth = upgradeController.HealthUpgrade.CurrentHealth;
            Health = MaxHealth;
        }
        upgradeFx.Play();
    }

    public void OnStartGame()
    {
        character.StartMove();
    }

    public override void ControllerUpdate()
    {
        base.ControllerUpdate();

        OnGameplayTypeUpdate(GameplayTypeController.CurrentType);

        if (currentRoad != null)
            currentRoad.RoadUpdate();
    }

    public void OnEnterRoad(RoadModel road)
    {
        currentRoad = road;
        if (road != lastRoad && lastRoad)
            lastRoad.OnPlayerExit();

        lastRoad = road;
        road.OnPlayerEnter(this);
        if (GameplayTypeController.CurrentType == GameplayTypes.Running)
            OnColorChange(CurrentColor);
    }

    public void OnColorChange(Color color)
    {
        if (color == null || Health == 0) return;

        CurrentColor = color;
        if (currentRoad != null)
            currentRoad.OnPlayerColorChange(CurrentColor);

        characterColor.color = CurrentColor;
    }

    public void OnBulletHit(BulletModel bullet)
    {
        bullet.OnHitTarget();
        GetDamage(bullet.Damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Road":
                OnEnterRoad(other.gameObject.GetComponent<RoadModel>());
                break;
            case "Bullet":
                OnBulletHit(other.GetComponent<BulletModel>());
                break;
        }
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        healthBar.HealthUpdate();
        if (Health <= 0)
            die();
    }

    private void die()
    {
        character.Dying();
        GameController.IsPlayerWin = false;
        GameStateHandler.StateHandler.ChangeState(GameStates.End);
    }

    public void OnGameplayTypeUpdate(GameplayTypes type)
    {
        switch (type)
        {
            case GameplayTypes.Running:
                if (Health > 0)
                    movementUpdate();
                break;
            case GameplayTypes.Sniper:
                sniperUpdate();
                break;
        }
    }

    private void sniperUpdate()
    {
        weaponModel.WeaponUpdate();
    }

    private void movementUpdate()
    {
        pointIndex = activePath.MoveObjectToPath(pointIndex, transform, forwardSpeed, rotationSpeed, onRunningStateComplete);
    }

    private void onRunningStateComplete()
    {
        colorBar.SetActive(false);
        aimBar.SetActive(true);
        weaponModel.OnAimStart();
        gameplayCamera.SetActive(false);
        GameplayTypeController.GameplayType.ChangeGameplay(GameplayTypes.Sniper);
        character.StopMoving();
    }

    private void onLevelComplete()
    {
        GameStateHandler.StateHandler.ChangeState(GameStates.End);
        character.StopMoving();
    }

}
