using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : ObjectModel
{
    public static ScreenManager Manager;
    public List<ScreenElement> screens;
    [SerializeField] RectTransform screensParent;
    public ScreenElement ActiveScreen;
    public static bool IsReScaled;

    public override void Initialize()
    {
        base.Initialize();
        if (Manager != null)
        {
            Destroy(Manager);
        }

        Manager = this;

        foreach (var item in screens)
        {
            item.Initialize();
            item.SetDeactive();
        }

        ActiveScreen.Show();
    }

    public void CheckReScale()
    {
#if UNITY_IOS
        switch (UnityEngine.iOS.Device.generation)
        {
            case UnityEngine.iOS.DeviceGeneration.iPhoneX:
                ReScale();
                break;
            case UnityEngine.iOS.DeviceGeneration.iPhoneXS:
                ReScale();
                break;
            case UnityEngine.iOS.DeviceGeneration.iPhoneXSMax:
                ReScale();
                break;
            case UnityEngine.iOS.DeviceGeneration.iPhoneXR:
                ReScale();
                break;
            case UnityEngine.iOS.DeviceGeneration.iPhone11:
                ReScale();
                break;
            case UnityEngine.iOS.DeviceGeneration.iPhone11Pro:
                ReScale();
                break;
            case UnityEngine.iOS.DeviceGeneration.iPhone11ProMax:
                ReScale();
                break;
            default:
                break;
        }
#endif
    }

    public void ReScale()
    {
        IsReScaled = true;
        screensParent.anchorMax = new Vector2(1, 0.96f);
        screensParent.anchorMin = new Vector2(0, 0.05f);
    }


    public T ChangeScreen<T>(bool showAfterHide, int index)
    {
        ScreenElement nextScreen = GetScreen<ScreenElement>(index);

        if (showAfterHide)
        {
            if (ActiveScreen != null)
            {
                ActiveScreen.Hide(nextScreen.Show);
                ActiveScreen = nextScreen;
            }
            else
            {
                ActiveScreen = nextScreen;
                ActiveScreen.Show();
            }
        }
        else
        {
            ActiveScreen.Hide();
            ActiveScreen = nextScreen;
            ActiveScreen.Show();
        }

        return (T)(object)nextScreen;
    }

    public void ChangeScreen(bool showAfterHide, int index)
    {
        ScreenElement nextScreen = GetScreen<ScreenElement>(index);

        if (showAfterHide)
        {
            if (ActiveScreen != null)
            {
                ActiveScreen.Hide(nextScreen.Show);
                ActiveScreen = nextScreen;
            }
            else
            {
                ActiveScreen = nextScreen;
                ActiveScreen.Show();
            }
        }
        else
        {
            ActiveScreen.Hide();
            ActiveScreen = nextScreen;
            ActiveScreen.Show();
        }

    }

    public T GetScreen<T>()
    {
        return (T)(object)screens.Find(x => x.GetType() == typeof(T));
    }

    public T GetScreen<T>(int index)
    {
        return (T)(object)screens[index];
    }
}
