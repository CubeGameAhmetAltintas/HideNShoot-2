using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : ScreenElement
{
    [SerializeField] WinScreen winScreen;
    [SerializeField] LoseScreen loseScreen;

    public override void Initialize()
    {
        base.Initialize();

        winScreen.Initialize();
        loseScreen.Initialize();
    }

    public override void Show()
    {
        base.Show();

        if (GameController.IsPlayerWin)
        {
            winScreen.Show();
        }
        else
        {
            loseScreen.Show();
        }
    }

    public void NextLevel()
    {

    }

    public void Retry()
    {
        GameController.Controller.Reload();
    }
}
