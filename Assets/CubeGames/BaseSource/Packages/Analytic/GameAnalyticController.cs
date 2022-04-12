using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if GA_ACTIVE
using GameAnalyticsSDK;
#endif

public class GameAnalyticController : AnalyticSDKController
{
#if GA_ACTIVE

    public override void Initialize()
    {
        base.Initialize();
        GameAnalytics.Initialize();
    }

#region AdEvents
    public void SendAdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement)
    {
        GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement);
    }

    public void SendAdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement, long duration)
    {
        GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement, duration);
    }

    public void SendAdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement, long duration, GAAdError adError)
    {
        GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement, adError);
    }
#endregion

#region BussinesEvent

    public void SendBusinessEvent(string curreny, int amount, string itemType, string itemId, string cartType)
    {
        GameAnalytics.NewBusinessEvent(curreny, amount, itemType, itemId, cartType);
    }

    public void SendGoogleBusinessEvent(string curreny, int amount, string itemType, string itemId, string cartType, string receip, string signature)
    {
        GameAnalytics.NewBusinessEventGooglePlay(curreny, amount, itemType, itemId, cartType, receip, signature);
    }

#endregion

#region DesignEvents
    public void SendDesignEvent(string eventName)
    {
        GameAnalytics.NewDesignEvent(eventName);
    }

    public void SendDesignEvent(string eventName, float eventValue)
    {
        GameAnalytics.NewDesignEvent(eventName, eventValue);
    }

#endregion

    public void SendErrorEvent(GAErrorSeverity errorSeverity, string message)
    {
        GameAnalytics.NewErrorEvent(errorSeverity, message);
    }

#region ProgressionEvent

    public void SendProgressionEvent(GAProgressionStatus progressionStatus, string progression1)
    {
        GameAnalytics.NewProgressionEvent(progressionStatus, progression1);
    }

    public void SendProgressionEvent(GAProgressionStatus progressionStatus, string progression1, int score)
    {
        GameAnalytics.NewProgressionEvent(progressionStatus, progression1, score);
    }

    public void SendProgressionEvent(GAProgressionStatus progressionStatus, string progression1, string progression2)
    {
        GameAnalytics.NewProgressionEvent(progressionStatus, progression1, progression2);
    }

    public void SendProgressionEvent(GAProgressionStatus progressionStatus, string progression1, string progression2, int score)
    {
        GameAnalytics.NewProgressionEvent(progressionStatus, progression1, progression2, score);
    }

    public void SendProgressionEvent(GAProgressionStatus progressionStatus, string progression1, string progression2, string progression3)
    {
        GameAnalytics.NewProgressionEvent(progressionStatus, progression1, progression2, progression3);
    }

    public void SendProgressionEvent(GAProgressionStatus progressionStatus, string progression1, string progression2, string progression3, int score)
    {
        GameAnalytics.NewProgressionEvent(progressionStatus, progression1, progression2, progression3, score);
    }

#endregion

    public void SendResourceEvent(GAResourceFlowType resourceFlowType, string currency, float amount, string itemType, string itemId)
    {
        GameAnalytics.NewResourceEvent(resourceFlowType, currency, amount, itemType, itemId);
    }
#endif
}
