using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : ScreenElement
{
    [SerializeField] Text txtLevel;
    [SerializeField] UpgradeController upgradeController;
    [SerializeField] PlayerController playerController;
    int earning;

    public override void Initialize()
    {
        txtLevel.text = "LEVEL " + PlayerDataModel.Data.Level + " Completed";

        base.Initialize();
    }

    public override void Show()
    {
        float healthPercent = (float)playerController.Health / (float)playerController.MaxHealth;

        earning = (int)((upgradeController.EarningUpgrade.CurrentEarning * healthPercent) * 1.25f) + (GameController.IsGeneralShooted == true ? 20 : 0);
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
