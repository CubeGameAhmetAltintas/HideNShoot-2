using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Scoreboard : ObjectModel
{
    public List<ScoreViewModel> ScoreViewModels;
    [SerializeField] Transform dolar;
    [SerializeField] float duration;

    public int GetPassedScoreViewCount()
    {
        return ScoreViewModels.Count(x => x.IsPassed);
    }

    public float StartScoreUpdate(PlayerController playerController)
    {
        float targetHeight = GetTargetPosition(PlayerDataModel.Data.Money);
        targetHeight = targetHeight < ScoreViewModels[0].transform.position.y ? ScoreViewModels[0].transform.position.y : targetHeight;
        dolar.DOMoveY(targetHeight, duration + (1 * GetDiff(PlayerDataModel.Data.Money))).OnUpdate(onUpdate).OnComplete(() => onScoreUpdateComplete(playerController));
        return targetHeight;
    }

    private void onScoreUpdateComplete(PlayerController playerController)
    {
        ScreenManager.Manager.ChangeScreen(true, 3);
    }

    private void onUpdate()
    {
        for (int i = 0; i < ScoreViewModels.Count; i++)
        {
            ScoreViewModels[i].CheckScore(dolar.position);
        }
    }

    public float GetTargetPosition(int playerScore)
    {
        ScoreViewModel lastScoreView = ScoreViewModels.GetLastItem();
        float position = ScoreViewModels[0].transform.position.y + ((GetDiff(playerScore) * lastScoreView.transform.position.y) + 0.5f);

        return position;
    }

    public float GetDiff(int playerScore)
    {
        ScoreViewModel lastScoreView = ScoreViewModels.GetLastItem();
        float diff = (float)(playerScore - ScoreViewModels[0].Score) / (float)(lastScoreView.Score - ScoreViewModels[0].Score);

        return diff;
    }

    public void CheckScore()
    {
        for (int i = 0; i < ScoreViewModels.Count; i++)
        {
            ScoreViewModels[i].CheckScore(dolar.position);
        }
    }

    [EditorButton]
    public void E_GetScoreParts()
    {
#if UNITY_EDITOR
        UnityEditor.Undo.RecordObject(this, "UpdateScores");
#endif
        ScoreViewModels = new List<ScoreViewModel>();
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if (transform.GetChild(0).GetChild(i).GetComponent<ScorePartModel>() != null)
            {
                ScoreViewModels.AddRange(transform.GetChild(0).GetChild(i).GetComponent<ScorePartModel>().ScoreViews);
            }
        }
    }
}
