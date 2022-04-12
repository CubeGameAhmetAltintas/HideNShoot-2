using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreViewModel : ObjectModel
{
    public int Score;
    public bool IsPassed;
    [SerializeField] Renderer model;
    [SerializeField] TextMeshPro txtScore;

    public void Load(Material mat, int score)
    {

#if UNITY_EDITOR
        UnityEditor.Undo.RecordObject(gameObject, "ScoreUpdate");
        UnityEditor.Undo.RecordObject(txtScore, "TextScoreUpdate");
        UnityEditor.Undo.RecordObject(model, "MatScoreUpdate");
#endif

        Score = score;
        model.material = mat;
        txtScore.text = Score.ToString();
        txtScore.alignment = TextAlignmentOptions.Right;

    }

    public void Load(Material mat, int score, string title)
    {
#if UNITY_EDITOR
        UnityEditor.Undo.RecordObject(gameObject, "ScoreUpdate");
        UnityEditor.Undo.RecordObject(txtScore, "TextScoreUpdate");
        UnityEditor.Undo.RecordObject(model, "MatScoreUpdate");
#endif

        Score = score;
        model.material = mat;
        txtScore.text = title.ToString();
        txtScore.alignment = TextAlignmentOptions.Center;
    }

    public void CheckScore(Vector3 dolarPos)
    {
        if (IsPassed == false)
        {
            if (dolarPos.y >= transform.position.y)
            {
                onPlayerPass();
            }
        }
    }

    private void onPlayerPass()
    {
        VibrateController.Controller.SetHaptic(VibrationTypes.Medium);
        IsPassed = true;
        model.transform.DOScale(new Vector3(1.3f, 1, 1), 0.25f);
        model.transform.DOScale(1, 0.25f).SetDelay(0.25f);
    }

}
