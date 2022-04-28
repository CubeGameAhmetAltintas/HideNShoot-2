using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : ScreenElement
{
    [SerializeField] Text txtLevel;
    [SerializeField] UpgradeController upgradeController;
    [SerializeField] PlayerController playerController;
    [SerializeField] EarningAnimation earningAnimation;
    [SerializeField] Transform missText;
    int earning;

    public override void Initialize()
    {
        txtLevel.text = "LEVEL " + PlayerDataModel.Data.Level + " Completed";

        base.Initialize();
    }

    public override void Show()
    {
        missText.SetActive(!GameController.IsGeneralShooted);
        float healthPercent = (float)playerController.Health / (float)playerController.MaxHealth;
        earning = (int)((upgradeController.EarningUpgrade.CurrentEarning * healthPercent) * 1.25f) + (GameController.IsGeneralShooted == true ? 20 : 0);
        earningAnimation.OnWinShow(earning);

        base.Show();
    }

    public void OnShowed()
    {
    }

    public void NextLevel()
    {
        GameController.Controller.UpdatePlayerCoin(earning);
        earningAnimation.Play(earning);
        PlayerDataModel.Data.Money += earning;
    }

}
