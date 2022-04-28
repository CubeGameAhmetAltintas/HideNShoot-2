using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image imgHealthBar, imgBgBar;
    [SerializeField] PlayerController player;
    float value;

    public void HealthUpdate()
    {
        value = (float)player.Health / (float)player.MaxHealth;
        imgHealthBar.fillAmount = value;
        OnShoot();
    }

    public void OnShoot()
    {
        imgBgBar.DOFillAmount(value, 0.1f).SetDelay(0.3f);
    }

}
