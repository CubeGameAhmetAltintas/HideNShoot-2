using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image imgHealthBar;
    [SerializeField] PlayerController player;

    public void HealthUpdate()
    {
        imgHealthBar.fillAmount = (float)player.Health / (float)player.MaxHealth;
    }
}
