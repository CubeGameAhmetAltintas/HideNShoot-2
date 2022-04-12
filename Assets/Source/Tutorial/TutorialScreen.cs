using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tutorial
{
    public class TutorialScreen : ScreenElement
    {
        public LessonModel[] Lessons;
        public LessonModel ActiveLesson;

        public override void Initialize()
        {
            base.Initialize();
            Show();
        }

        public void CheckLesson(int id)
        {

            if (ActiveLesson != null)
            {
                if (ActiveLesson.IsCompleted == false)
                {
                    return;
                }
            }

            LessonModel lesson = Lessons.Find(x => x.Index == id);
            if (lesson != null)
            {
                if (lesson.IsCompleted == false)
                {
                    LoadLesson(lesson);
                }
            }
        }

        public void LoadLesson(LessonModel lesson)
        {
            ActiveLesson = lesson;
            ActiveLesson.Load();
        }
    }
}