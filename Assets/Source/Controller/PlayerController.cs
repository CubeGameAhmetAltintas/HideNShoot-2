using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerBaseModel
{
    [SerializeField] float forwardSpeed;
    [SerializeField] float rotationSpeed;
    public CharacterModel character;
    [SerializeField] Transform characterParent;
    [SerializeField] UpgradeController upgradeController;
    [SerializeField] ParticleSystem upgradeFx;
    int pointIndex;

    public int Health = 50;
    public int MaxHealth;
    //public int MaxHealth;
    private RoadModel currentRoad, lastRoad;
    [SerializeField] MeshRenderer characterColor;
    [SerializeField] PlayerColorBar colorBar;
    public Color CurrentColor;
    [SerializeField] HealthBar healthBar;

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
            if (!colors.Contains(level.RoadDatas[i].TargetColor))
                colors.Add(level.RoadDatas[i].TargetColor);
        }
        colorBar.Initialize(colors);
    }

    public void OnUpgrade(int upgradeId)
    {
        if(upgradeId == 0)
        {
            MaxHealth = upgradeController.HealthUpgrade.CurrentHealth;
            Health = MaxHealth;
        }
        upgradeFx.Play();
    }

    public void OnStartGame()
    {

    }

    public override void ControllerUpdate()
    {
        base.ControllerUpdate();
        if(Health > 0)
            movementUpdate();

        if (currentRoad != null)
            currentRoad.RoadUpdate();
    }

    public void OnEnterRoad(RoadModel road)
    {
        currentRoad = road;
        if (road != lastRoad && lastRoad)
            lastRoad.OnPlayerExit();

        lastRoad = road;

        OnColorChange(CurrentColor);
    }

    public void OnColorChange(Color color)
    {
        if (color == null) return;
        CurrentColor = color;
        currentRoad.OnPlayerColorChange(CurrentColor);
        characterColor.material.color = CurrentColor;
    }

    public void OnBulletHit(BulletModel bullet)
    {
        bullet.OnHitTarget();
        GetDamage(bullet.Damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Road")
        {
            OnEnterRoad(other.gameObject.GetComponent<RoadModel>());
        }

        if (other.tag == "Bullet")
        {
            OnBulletHit(other.GetComponent<BulletModel>());
        }
        // Switch case tag control
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        healthBar.HealthUpdate();
        if(Health <= 0)
            die();
    }

    private void die()
    {
        character.SetDeactive();
    }

    private void movementUpdate()
    {
        pointIndex = activePath.MoveObjectToPath(pointIndex, transform, forwardSpeed, rotationSpeed, onLevelComplete);
    }

    private void onLevelComplete()
    {
        GameStateHandler.StateHandler.ChangeState(GameStates.End);
    }

}
