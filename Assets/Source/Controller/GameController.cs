using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : ControllerBaseModel
{
    public static GameController Controller;
    public Transform FxGarbage;
    public IntEventModel onUpdatePlayerCoint;
    public static bool IsPlayerWin;
    public static float EnemyDetectSensitve = 0.45f;

    public override void Initialize()
    {
        base.Initialize();


        if (Controller == null)
        {
            Controller = this;
        }
        else
        {
            Destroy(Controller);
            Controller = this;
        }

    }

    public void Reload()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdatePlayerCoin(int value)
    {
        PlayerDataModel.Data.Money += value;
        onUpdatePlayerCoint?.Invoke(value);
    }

}

public static class GameValues
{
}
