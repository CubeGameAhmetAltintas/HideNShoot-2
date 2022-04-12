using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextAnimation : TutorialAnimationModel
{
    [SerializeField] Text txtTitle;

    public  void SetActive(string value)
    {
        base.SetActive();
        txtTitle.text = value;
    }
}
