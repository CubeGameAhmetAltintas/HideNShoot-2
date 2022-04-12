//TODO REMOVE FOR SDK
//using Facebook.Unity;
//using GameAnalyticsSDK;
//using com.adjust.sdk;
using System;

public class AnalyticController : ControllerBaseModel
{
    public static AnalyticController Controller;
    //TODO REMOVE FOR SDK
    //[SerializeField] Adjust adjust;

    public override void Initialize()
    {
        base.Initialize();
        DontDestroyOnLoad(this.gameObject);
        //TODO REMOVE FOR SDK
        //adjust.Initialize();
        Controller = this;
    }

    public void ProgressionStart()
    {
#if UNITY_IOS || UNITY_ANDROID
        //TODO REMOVE FOR SDK
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "gameplay_" + DataManager.Player.Level + "_start");
        //SendFacebookEvent("gameplay_" + DataManager.Player.Level + "_start");
#endif
    }

    public void ProgressionEnd()
    {
#if UNITY_IOS || UNITY_ANDROID
        //TODO REMOVE FOR SDK
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "gameplay_" + DataManager.Player.Level + "_finish");
        //SendFacebookEvent("gameplay_" + DataManager.Player.Level + "_finish");
#endif
    }

    public void SendGA(string eventValue, bool sendFacebook)
    {
#if UNITY_IOS || UNITY_ANDROID
        //TODO REMOVE FOR SDK
        //GameAnalytics.NewDesignEvent(eventValue);
        //SendFacebookEvent(eventValue);
#endif
    }

    public void SendFacebookEvent(object value)
    {
#if UNITY_IOS || UNITY_ANDROID
        //TODO REMOVE FOR SDK
        //FB.LogAppEvent(value.ToString());
#endif
    }

    public void Adjust_SimpleEvent(string eventToken)
    {
        //TODO REMOVE FOR SDK
        //AdjustEvent adjustEvent = new AdjustEvent(eventToken);
        //Adjust.trackEvent(adjustEvent);
        //Debug.Log("ADJUST EVENT SEND: " + eventToken + " AppId: " + adjust.appToken);
    }

    public void Adjust_RevenueEvent(string eventToken, double amount, string currency)
    {
        //TODO REMOVE FOR SDK
        //AdjustEvent adjustEvent = new AdjustEvent(eventToken);
        //adjustEvent.setRevenue(amount, currency);
        //Adjust.trackEvent(adjustEvent);
    }

    public void Adjust_CallbackEvent(string eventToken, params ParameterModel[] parameters)
    {
        //TODO REMOVE FOR SDK
        //AdjustEvent adjustEvent = new AdjustEvent(eventToken);
        //foreach (var item in parameters)
        //{
        //    adjustEvent.addCallbackParameter(item.Key, item.Value);
        //}

        //Adjust.trackEvent(adjustEvent);
    }

    public void Adjust_PartnerEvent(string eventToken, params ParameterModel[] parameters)
    {
        //TODO REMOVE FOR SDK
        //AdjustEvent adjustEvent = new AdjustEvent(eventToken);
        //foreach (var item in parameters)
        //{
        //    adjustEvent.addPartnerParameter(item.Key, item.Value);
        //}

        //Adjust.trackEvent(adjustEvent);
    }
}



public class ParameterModel
{
    public string Key;
    public string Value;

    public ParameterModel(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
