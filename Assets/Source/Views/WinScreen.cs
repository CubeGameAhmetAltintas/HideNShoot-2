using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : ScreenElement
{
    [SerializeField] Text txtLevel;
    int earning;

    public override void Initialize()
    {
        txtLevel.text = "LEVEL " + PlayerDataModel.Data.Level + " Completed";

        base.Initialize();
    }

    public override void Show()
    {
        base.Show();
    }

    public void OnShowed()
    {
    }

    public void NextLevel()
    {
        GameController.Controller.UpdatePlayerCoin(earning);
        PlayerDataModel.Data.Money += earning;
        PlayerDataModel.Data.Save();
        LevelController.Controller.NextLevel();
        GameController.Controller.Reload();
    }

}
