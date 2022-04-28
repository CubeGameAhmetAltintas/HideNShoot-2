using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EarningAnimation : ObjectModel
{
    [SerializeField] Transform[] moneyIcons;
    [SerializeField] Transform startPos, endPos;
    [SerializeField] float duration;
    [SerializeField] float delay;
    [SerializeField] Text txtEarning, txtMoney;
    int animEarning;
    int playerCoin;

    public void OnWinShow(int earning)
    {
        for (int i = 0; i < moneyIcons.Length; i++)
        {
            moneyIcons[i].position = startPos.position;
        }

        txtEarning.text = "EARNING: " + earning.ToCoinValues() + "$";
        txtMoney.text = PlayerDataModel.Data.Money.ToCoinValues() + "$";
        playerCoin = PlayerDataModel.Data.Money;
    }

    public void Play(int earning)
    {
        animEarning = earning;
        for (int i = 0; i < moneyIcons.Length; i++)
        {
            moneyIcons[i].SetActive(true);
            if (i == moneyIcons.Length - 1)
            {
                moneyIcons[i].DOMove(endPos.position, duration).From(startPos.position).SetDelay(i * delay).OnComplete(OnAnimComplete);
            }
            else
            {
                moneyIcons[i].DOMove(endPos.position, duration).From(startPos.position).SetDelay(i * delay);
            }
        }

        DOTween.To(() => animEarning, x => animEarning = x, 0, duration).OnUpdate(() =>
        {
            txtEarning.text = "EARNING: " + animEarning.ToCoinValues() + "$";
        });

        DOTween.To(() => playerCoin, x => playerCoin = x, playerCoin + earning + (GameController.IsGeneralShooted ? 20 : 0), duration).OnUpdate(() =>
          {
              txtMoney.text = playerCoin.ToCoinValues() + "$";
          });
    }

    public void OnAnimComplete()
    {
        for (int i = 0; i < moneyIcons.Length; i++)
        {
            moneyIcons[i].SetActive(false);
        }

        PlayerDataModel.Data.Save();
        LevelController.Controller.NextLevel();
        GameController.Controller.Reload();
    }

}
