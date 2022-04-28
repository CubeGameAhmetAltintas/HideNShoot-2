using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : ControllerBaseModel
{
    public static TutorialController Controller;
    public bool IsCompleted;
    public int ActiveLesseonIndex;
    [SerializeField] Transform tutorialView;
    [SerializeField] PlayerController playerController;
    [SerializeField] Text txtTutorial;

    public override void Initialize()
    {
        base.Initialize();

        if (Controller == null)
        {
            Controller = this;
        }
        else
        {
            Destroy(Controller);
            Controller = this;
        }

        IsCompleted = PlayerPrefs.GetInt("TutorialCompleted", 0) == 2;

        if (IsCompleted == false)
        {
            PrepareLesson("Match Road Color!");
        }
    }

    public void CompleteTutorial()
    {
        IsCompleted = true;
        PlayerPrefs.SetInt("TutorialCompleted", 2);
    }

    public void PrepareLesson(string strTutorial)
    {
        if (IsCompleted == false)
        {
            txtTutorial.text = strTutorial;
            tutorialView.SetActive(true);
        }
    }

    public void OnLessonComplete()
    {
        if (ActiveLesseonIndex == 0)
        {
            playerController.OnTutorialLessonComplete();
            tutorialView.SetActive(false);
        }
        else if (ActiveLesseonIndex == 1)
        {
            tutorialView.SetActive(false);
            CompleteTutorial();
        }

        ActiveLesseonIndex++;
    }
}
