using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MatchScoreboardController
{
    public int HomeScore;
    public int AwayScore;
    [SerializeField] Text txtHomeScore, txtAwayScore, txtEarnedHomeScore, txtEarnedAwayScore;

    public void Initialize()
    {
        HomeScore = 0;
        AwayScore = 0;

        txtHomeScore.text = HomeScore.ToString();
        txtAwayScore.text = AwayScore.ToString();
    }

    public void OnHomeSetScore(int score)
    {
        HomeScore += score;
        txtEarnedHomeScore.text = "+" + score.ToString();
        txtEarnedHomeScore.SetActive(true);
    }

    public void UpdateHomeScore()
    {
        txtHomeScore.text = HomeScore.ToString();
    }

    public void UpdateAwayScore()
    {
        txtAwayScore.text = AwayScore.ToString();
    }

    public void OnAwaySetScore(int score)
    {
        AwayScore += score;
        txtEarnedAwayScore.text = "+" + score.ToString();
        txtEarnedAwayScore.SetActive(true);
    }
}
