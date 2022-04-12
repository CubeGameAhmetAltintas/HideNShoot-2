#if ADJUST_ACTIVE
using com.adjust.sdk;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustController : AdSDKController
{
#if ADJUST_ACTIVE
    public void Adjust_SimpleEvent(string eventToken)
    {
        AdjustEvent adjustEvent = new AdjustEvent(eventToken);
        Adjust.trackEvent(adjustEvent);
    }

    public void Adjust_RevenueEvent(string eventToken, double amount, string currency)
    {
        AdjustEvent adjustEvent = new AdjustEvent(eventToken);
        adjustEvent.setRevenue(amount, currency);
        Adjust.trackEvent(adjustEvent);
    }

    public void Adjust_CallbackEvent(string eventToken, params ParameterModel[] parameters)
    {
        AdjustEvent adjustEvent = new AdjustEvent(eventToken);
        foreach (var item in parameters)
        {
            adjustEvent.addCallbackParameter(item.Key, item.Value);
        }

        Adjust.trackEvent(adjustEvent);
    }

    public void Adjust_PartnerEvent(string eventToken, params ParameterModel[] parameters)
    {
        AdjustEvent adjustEvent = new AdjustEvent(eventToken);
        foreach (var item in parameters)
        {
            adjustEvent.addPartnerParameter(item.Key, item.Value);
        }

        Adjust.trackEvent(adjustEvent);
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
#endif
}
