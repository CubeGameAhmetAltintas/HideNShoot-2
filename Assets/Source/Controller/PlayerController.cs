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

    public int Health;
    public int MaxHealth => Health * PlayerDataModel.Data.HealthLevel;
    private RoadModel currentRoad;
    private RoadModel lastRoad;
    [SerializeField] MeshRenderer characterColor;
    //public Color CurrentColor => characterColor.material.color;

    [SerializeField] PlayerColorBar colorBar;
    public Color CurrentColor;

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
        List<Color> colors = new List<Color>();
        for (int i = 0; i < level.RoadDatas.Length; i++)
        {
            //TODO Check again
            bool isExists = false;
            for (int j = 0; j < colors.Count; j++)
            {
                if (level.RoadDatas[i].TargetColor.Equals(colors[j]))
                    isExists = true;
            }
            if (!isExists)
                colors.Add(level.RoadDatas[i].TargetColor);
        }
        colorBar.Initialize(colors);
    }

    public void OnUpgrade(int upgradeId)
    {
        upgradeFx.Play();
    }

    public void OnStartGame()
    {

    }

    public override void ControllerUpdate()
    {
        base.ControllerUpdate();
        movementUpdate();

        if (currentRoad != null)
            currentRoad.RoadUpdate();
    }

    public void OnEnterRoad(RoadModel road)
    {
        if (road != lastRoad && lastRoad)
            lastRoad.OnPlayerExit();

        lastRoad = road;
        //current color
        // road ýn update olacak
        OnColorChange(CurrentColor);
    }

    public void OnColorChange(Color color)
    {
        // color set
        if (color == null) return;
        CurrentColor = color;
        currentRoad.OnPlayerColorChange(CurrentColor);
        characterColor.material.color = CurrentColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Road")
        {
            currentRoad = other.gameObject.GetComponent<RoadModel>();
            OnEnterRoad(currentRoad);
        }
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
