using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeController : ControllerBaseModel
{
    public HealthUpgrade HealthUpgrade;
    public EarningUpgrade EarningUpgrade;
    [SerializeField] MainScreen mainScreen;
    [SerializeField] PlayerController playerController;

    public override void Initialize()
    {
        base.Initialize();

        HealthUpgrade.Initialize(PlayerDataModel.Data.HealthLevel);
        EarningUpgrade.Initialize(PlayerDataModel.Data.EarningLevel);

        mainScreen.UpdatePrices(HealthUpgrade.Price, EarningUpgrade.Price);
    }

    public void UpgradeHealth()
    {
        if (PlayerDataModel.Data.Money >= HealthUpgrade.Price)
        {
            GameController.Controller.UpdatePlayerCoin(-HealthUpgrade.Price);
            PlayerDataModel.Data.Save();
            HealthUpgrade.Upgrade();
            mainScreen.UpdatePrices(HealthUpgrade.Price, EarningUpgrade.Price);
            playerController.OnUpgrade(0);
        }
    }

    public void UpgradeEarning()
    {
        if (PlayerDataModel.Data.Money >= EarningUpgrade.Price)
        {
            GameController.Controller.UpdatePlayerCoin(-EarningUpgrade.Price);
            //PlayerDataModel.Data.Money -= EarningUpgrade.Price;
            PlayerDataModel.Data.Save();
            EarningUpgrade.Upgrade();
            mainScreen.UpdatePrices(HealthUpgrade.Price, EarningUpgrade.Price);
            playerController.OnUpgrade(1);
        }
    }


    [EditorButton]
    public void TestUpgradePrice(int level)
    {
        EarningUpgrade.StartPrice = 100;
        EarningUpgrade.Initialize(level);

        Debug.Log(EarningUpgrade.CurrentEarning);
    }
}
