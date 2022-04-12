using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : ScreenElement
{
    [SerializeField] Text txtEarningMoney;
    [SerializeField] Scoreboard scoreboard;
    [SerializeField] UpgradeController upgradeController;
    int earning;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Show()
    {
        base.Show();

        earning = upgradeController.EarningUpgrade.GetEarning(scoreboard.GetPassedScoreViewCount());
        txtEarningMoney.text = "Earned : " + earning + "$";
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
