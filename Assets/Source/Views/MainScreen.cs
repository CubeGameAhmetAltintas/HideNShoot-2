using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : ScreenElement
{
    [SerializeField] Text txtLevel;
    [SerializeField] Text txtMoney;
    [SerializeField] Text txtUpgradePrice, txtEarningUpgradePrice;
    [SerializeField] Animator moneyAnim;

    public override void Initialize()
    {
        base.Initialize();

        txtLevel.text ="LEVEL " + PlayerDataModel.Data.Level.ToString();
        txtMoney.text = PlayerDataModel.Data.Money.ToCoinValues() + "$";
    }

    public void OnPlayerMoneyUpdate(int value)
    {
        if (value >= 0)
        {
            moneyAnim.Play("OnIncrease", 0, 0);
        }
        else
        {
            moneyAnim.Play("OnDecrease", 0, 0);
        }
        txtMoney.text = PlayerDataModel.Data.Money.ToCoinValues() + "$";
    }


    public void StartGame()
    {
        GameStateHandler.StateHandler.ChangeState(GameStates.Game);
    }

    public void UpdatePrices(int yearPrice, int earningPrice)
    {
        txtUpgradePrice.text = yearPrice.ToCoinValues() + "$";
        txtEarningUpgradePrice.text = earningPrice.ToCoinValues() + "$";
        txtMoney.text = PlayerDataModel.Data.Money.ToCoinValues() + "$";
    }
}
