using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayTypeController : ControllerBaseModel
{
    public static GameplayTypeController GameplayType;
    public static GameplayTypes CurrentType;
    [SerializeField] PlayerController playerController;

    public override void Initialize()
    {
        GameplayType = this;
        ChangeGameplay(GameplayTypes.Running);
        base.Initialize();
    }

    private void onRunning()
    {
        print("Running");
    }

    private void onSniper()
    {
        print("snipeeer");
    }

    public void ChangeGameplay(GameplayTypes type)
    {
        CurrentType = type;
        switch (CurrentType)
        {
            case GameplayTypes.Running:
                onRunning();
                break;
            case GameplayTypes.Sniper:
                onSniper();
                break;
        }
    }
}

public enum GameplayTypes 
{
    Running,
    Sniper
}
