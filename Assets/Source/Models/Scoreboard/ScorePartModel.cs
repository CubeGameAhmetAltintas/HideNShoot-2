using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePartModel : ObjectModel
{
    public ScoreViewModel[] ScoreViews;
    [SerializeField] ScoreVisualModel visualModel;
    public bool IsPassed;
    public int MinScore;
    public int MaxScore;
    public int ScorePartIndex;

    public void LoadModel(int scorePartIndex, int minScore, int maxScore, ScoreVisualModel scoreVisual)
    {
        ScorePartIndex = scorePartIndex;
        MaxScore = maxScore;
        IsPassed = false;
        transform.localPosition = new Vector3(0, scorePartIndex * 10, 0);
        int index = 0;
        int incValue = Mathf.Abs(maxScore - minScore) / ScoreViews.Length;

        for (int i = minScore; i <= maxScore; i += incValue)
        {

            if (index < 9)
            {
                if (index == 0 && i == 0)
                {
                    ScoreViews[index].Load(index % 2 == 0 ? scoreVisual.DarkColor : scoreVisual.LightColor, i, "NOT MUCH");
                }
                else
                {
                    ScoreViews[index].Load(index % 2 == 0 ? scoreVisual.DarkColor : scoreVisual.LightColor, i);
                }

            }
            else if (index == ScoreViews.Length - 1)
            {
                ScoreViews[index].Load(scoreVisual.TitleColor, i);
            }

            index++;
        }
    }

    [EditorButton]
    public void E_TestScoreView()
    {
        LoadModel(transform.GetSiblingIndex(), MinScore, MaxScore, visualModel);
    }

    [EditorButton]
    public void E_GetScoreViews()
    {
        int childCount = transform.childCount;
        ScoreViews = new ScoreViewModel[transform.childCount];

        for (int i = 0; i < childCount; i++)
        {
            ScoreViewModel scoreView = transform.GetChild(i).GetComponent<ScoreViewModel>();
            scoreView.transform.localPosition = new Vector3(0, i, 0);

            ScoreViews[i] = scoreView;
        }
    }
}
