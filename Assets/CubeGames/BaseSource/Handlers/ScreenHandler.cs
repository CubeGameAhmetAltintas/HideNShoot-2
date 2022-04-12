using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenHandler : HandlerBaseModel
{
    [SerializeField] List<ScreenElement> screens;

    public void Initialize(bool deactiveAllScreen)
    {
        base.Initialize();

        foreach (var item in screens)
        {
            item.Initialize();

            if (deactiveAllScreen)
                item.SetDeactive();
        }
    }

}