using DG.Tweening;
using System;
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
    public Color CurrentColor => colorBar.CurrentColor;

    PathModel activePath
    {
        get
        {
            return LevelController.Controller.ActivePath;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public void Init(Level level)
    {
        for (int i = 0; i < level.RoadDatas.Length; i++)
        {
            colorBar.Initialize(level.RoadDatas[i].TargetColor);
        }
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
        // road �n update olacak
        //OnColorChange(CurrentColor);
    }

    public void OnColorChange(Color color)
    {
        // color set
        if (color == null) return;
        currentRoad.OnPlayerColorChange(color);
        characterColor.material.color = color;
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
