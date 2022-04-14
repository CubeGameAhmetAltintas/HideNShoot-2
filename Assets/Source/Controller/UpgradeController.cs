using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeController : ControllerBaseModel
{
    public YearUpgrade YearUpgrade;
    public EarningUpgrade EarningUpgrade;
    [SerializeField] MainScreen mainScreen;
    [SerializeField] PlayerController playerController;

    public override void Initialize()
    {
        base.Initialize();

        //YearUpgrade.Initialize(PlayerDataModel.Data.YearLevel);
        EarningUpgrade.Initialize(PlayerDataModel.Data.EarningLevel);

        mainScreen.UpdatePrices(YearUpgrade.Price, EarningUpgrade.Price);
    }

    public void UpgradeYear()
    {
        if (PlayerDataModel.Data.Money >= YearUpgrade.Price)
        {
            GameController.Controller.UpdatePlayerCoin(-YearUpgrade.Price);
            //PlayerDataModel.Data.Money -= YearUpgrade.Price;
            PlayerDataModel.Data.Save();
            YearUpgrade.Upgrade();
            mainScreen.UpdatePrices(YearUpgrade.Price, EarningUpgrade.Price);
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
            mainScreen.UpdatePrices(YearUpgrade.Price, EarningUpgrade.Price);
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
