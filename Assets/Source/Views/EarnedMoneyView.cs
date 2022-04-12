using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EarnedMoneyView : ObjectModel
{
    [SerializeField] TextMeshPro txtScore;
    [SerializeField] Animator animator;

    public void SetActive(int score)
    {
        base.SetActive();
        txtScore.text = "+" + score.ToString() + "$";
        gameObject.SetActive(true);
        animator.Play("Intro", 0, 0);
    }

}
