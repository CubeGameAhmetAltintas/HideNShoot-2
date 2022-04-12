using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAnimation : ObjectModel
{
    [SerializeField] Text txtScore;
    [SerializeField] GameScreen gameScreen;
    public void SetActive(int score)
    {
        if (score > 0)
        {
            txtScore.text = "+" + score.ToString();
        }
        else
        {
            txtScore.text = score.ToString();

        }
        base.SetActive();
    }

    public void OnScoreComplete()
    {
        SetDeactive();
    }
}
