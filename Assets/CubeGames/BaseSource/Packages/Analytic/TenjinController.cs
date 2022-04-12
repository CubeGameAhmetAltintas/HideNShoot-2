using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenjinController : AnalyticSDKController
{
#if TENJIN_ACTIVE
    public override void Initialize()
    {
        base.Initialize();

        BaseTenjin instance = Tenjin.getInstance(AppKey);
        instance.Init(AppKey);
    }

    public void Connect()
    {
        BaseTenjin instance = Tenjin.getInstance(AppKey);
        instance.Connect();
    }

    public void SendCustomEvent(string eventName, string eventValue)
    {
        BaseTenjin instance = Tenjin.getInstance(AppKey);
        instance.SendEvent(eventName, eventValue);
    }

    public void SendCustomEvent(string eventName)
    {
        BaseTenjin instance = Tenjin.getInstance(AppKey);
        instance.SendEvent(eventName);
    }
#endif
}
