using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextObj : ObjectModel
{
    [SerializeField] Text txtText;

    public  void SetActive(string txt)
    {
        txtText.text = txt;
        base.SetActive();
    }
}
