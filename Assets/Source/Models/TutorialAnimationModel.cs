using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimationModel : ObjectModel
{
    [HideInInspector] public int ShowCount;
    [SerializeField] Animator animator;
    public int LoopCount;

    public override void SetActive()
    {
        ShowCount = LoopCount;
        base.SetActive();
    }

    public override void SetDeactive()
    {
        ShowCount = LoopCount;
        base.SetDeactive();
    }

    public void Trig()
    {
        ShowCount--;
        if (ShowCount != 0)
        {
            animator.Play("Idle", 0, 0);
        }
    }

    public void SetPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition = pos;
    }
}
