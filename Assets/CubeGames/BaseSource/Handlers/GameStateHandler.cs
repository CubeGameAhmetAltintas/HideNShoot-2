using ElephantSDK;
using UnityEngine;

public class GameStateHandler : HandlerBaseModel
{
    public static GameStates CurrentStates;
    public static GameStateHandler StateHandler;
    [SerializeField] PlayerController playerController;
    [SerializeField] RoadController roadController;

    public override void Initialize()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = -1;

        StateHandler = this;
        ChangeState(GameStates.Loading);
        base.Initialize();
    }

    public void ChangeState(GameStates state)
    {
        CurrentStates = state;

        switch (state)
        {
            case GameStates.Loading:
                SetLoadingState();
                break;
            case GameStates.Start:
                SetStartState();
                break;
            case GameStates.Game:
                SetGameState();
                break;
            case GameStates.End:
                SetEndState();
                break;
        }
    }

    private void Update()
    {
        switch (CurrentStates)
        {
            case GameStates.Loading:
                LoadingStateUpdate();
                break;
            case GameStates.Start:
                StartStateUpdate();
                break;
            case GameStates.Game:
                GameStateUpdate();
                break;
            case GameStates.End:
                EndStateUpdate();
                break;
            default:
                break;
        }
    }

    public void SetLoadingState()
    {
        ScreenManager.Manager.ChangeScreen(false, 0);
    }

    public void SetStartState()
    {
        ScreenManager.Manager.ChangeScreen(true, 1);
        Elephant.LevelStarted(PlayerDataModel.Data.Level);
    }

    public void SetGameState()
    {
        playerController.OnStartGame();
        ScreenManager.Manager.ChangeScreen(true, 2);
    }

    public void SetEndState()
    {
        if (GameController.IsPlayerWin)
        {
            Elephant.LevelCompleted(PlayerDataModel.Data.Level);
        }
        else
        {
            Elephant.LevelFailed(PlayerDataModel.Data.Level);
        }

        roadController.OnLevelCompleted();
    }

    public void LoadingStateUpdate()
    {

    }

    public void StartStateUpdate()
    {
    }

    public void GameStateUpdate()
    {
        playerController.ControllerUpdate();
    }

    public void EndStateUpdate()
    {
    }
}

public enum GameStates
{
    Loading,
    Start,
    Game,
    End
}