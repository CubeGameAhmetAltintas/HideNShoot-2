using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : ScreenElement
{
    [SerializeField] Text txtLevel,txtMoney;
    [SerializeField] Animator moneyAnim;

    public override void Initialize()
    {
        base.Initialize();

        txtLevel.text = "LEVEL " + PlayerDataModel.Data.Level.ToString();
    }

    public override void Show()
    {
        txtMoney.text = PlayerDataModel.Data.Money.ToCoinValues() + "$";
        base.Show();
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
}
