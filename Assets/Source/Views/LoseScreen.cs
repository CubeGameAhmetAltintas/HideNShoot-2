using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : ScreenElement
{
    [SerializeField] Text txtLevel;

    public override void Initialize()
    {
        base.Initialize();

        txtLevel.text = "LEVEL " + PlayerDataModel.Data.Level +  " FAIL";
    }
}
