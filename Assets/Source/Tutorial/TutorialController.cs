using UnityEngine;

namespace Game.Tutorial
{
    public class TutorialController : ControllerBaseModel
    {
        public static TutorialController Controller;
        public bool IsCompleted;
        public int MaxTutorialCount;
        ScreenManager screenManager;
        TutorialScreen tutorialScreen;
        int tutorialIndex;

        public override void Initialize()
        {
            base.Initialize();
            Controller = this;
            DontDestroyOnLoad(this.gameObject);
            tutorialIndex = PlayerPrefs.GetInt("MetaTutorial");
            IsCompleted = tutorialIndex == MaxTutorialCount;
        }

        public void OnLessonComplete(LessonModel lesson)
        {
            tutorialIndex++;
            IsCompleted = tutorialIndex == MaxTutorialCount;
            if (IsCompleted)
            {
                AnalyticController.Controller.Adjust_SimpleEvent("a8yyi8");
            }

            PlayerPrefs.SetInt("MetaTutorial", tutorialIndex);
            PlayerPrefs.Save();
        }

        public void OnGameTutorialComplete(int gameplayId)
        {

        }

        public void OnSceneChange(int sceneIndex)
        {
            if (IsCompleted == false)
            {
                screenManager = FindObjectOfType<ScreenManager>();
                tutorialScreen = screenManager.GetScreen<TutorialScreen>();
                tutorialScreen.Initialize();

                if (sceneIndex == 1)
                {
                    CheckLesson(0);
                    CheckLesson(2);
                }
            }
        }

        public void CheckLesson(int lessonId)
        {
            if (lessonId == tutorialIndex)
            {
                if (IsCompleted == false)
                    tutorialScreen.CheckLesson(lessonId);
            }
        }
    }
}