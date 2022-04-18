using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthUpgrade : UpgradeModel
{
    [SerializeField] int increaseAmount;
    public int StartHealth;
    public int CurrentHealth
    {
        get
        {
            return StartHealth + (Level * increaseAmount);
        }
    }

    public override void Initialize(int level)
    {
        base.Initialize(level);
    }

    public override void Upgrade()
    {
        base.Upgrade();

        UpdatePrice();
        Level++;
        PlayerDataModel.Data.HealthLevel = Level;
        PlayerDataModel.Data.Save();
    }
}
