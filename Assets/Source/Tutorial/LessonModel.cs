//using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tutorial
{
    public class LessonModel : ObjectModel
    {
        public int Index;
        public bool IsCompleted;
        //public ObjectCopier[] ObjectCopiers;
        public Image MaskImage;

        public void Initialize(TutorialDataModel data)
        {
            base.Initialize();
            IsCompleted = data.IsCompleted;
        }


        public void Load()
        {
            if (MaskImage != null)
            {
                //MaskImage.transform.DOScale(1, 0.75f);
            }

            SetActive();
            //for (int i = 0; i < ObjectCopiers.Length; i++)
            //{
            //    ObjectCopiers[i].CopyItem();
            //}
        }

        public void OnLessonComplete()
        {
            SetDeactive();
            IsCompleted = true;
            TutorialController.Controller.OnLessonComplete(this);
        }

    }
}