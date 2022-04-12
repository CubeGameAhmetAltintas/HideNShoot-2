using DG.Tweening;
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
