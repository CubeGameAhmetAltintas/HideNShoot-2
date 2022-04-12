using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : ScreenElement
{
    public WinScreen WinScreen;

    public override void Initialize()
    {
        base.Initialize();

        WinScreen.Initialize();
    }

    public override void Show()
    {
        base.Show();

        WinScreen.Show();
    }

    public void NextLevel()
    {
       
    }

    public void Retry()
    {
        GameController.Controller.Reload();
    }
}
