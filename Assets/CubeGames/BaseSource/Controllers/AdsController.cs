using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsController : ControllerBaseModel
{
    public static AdsController Controller;
    public static bool IsAdsPlaying;
    public int TargetTriggerCount;
    //[SerializeField] IronSourceEvents ironSourceEvents;
    Action onRewardSucces, onRewardFailed;
    float showInterTime;
    int triggerCount = 1;

    public override void Initialize()
    {
        if (Controller == null)
        {
            //ironSourceEvents.Initialize();
            DontDestroyOnLoad(this);
            Controller = this;

            //Cubekatana.Initialize();

            //CubekatanaAds.OnRewardVideoCancelled = OnRewardClosed;
            //CubekatanaAds.OnRewardVideoSucceed = OnRewardComplete;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowInterstital()
    {
        if (Time.time > showInterTime + 30)
        {
            showInterTime = Time.time;
            //CubekatanaAds.ShowInterstitial(false);
        }
    }

    public void ShowInterWithTrigger()
    {
        if (triggerCount == TargetTriggerCount)
        {
            triggerCount = 1;
            //CubekatanaAds.ShowInterstitial(false);

        }
        else
        {
            triggerCount++;
        }
    }

    public void ShowReward(Action onRewardSucces, string eventDescs, string adjustEvent = "", Action onRewardFailed = null)
    {
        if (string.IsNullOrEmpty(adjustEvent) == false)
        {
            AnalyticController.Controller.Adjust_SimpleEvent(adjustEvent);
        }

        AnalyticController.Controller.SendFacebookEvent(eventDescs);
        IsAdsPlaying = true;

        this.onRewardSucces = onRewardSucces;
        this.onRewardFailed = onRewardFailed;

        if (Application.isEditor)
        {
            OnRewardComplete();
        }
        else
        {
            //CubekatanaAds.ShowRewardedVideo(true);
        }
    }

    public void OnRewardComplete()
    {
        IsAdsPlaying = false;
        showInterTime = Time.time;
        onRewardSucces();
    }

    public void OnRewardClosed()
    {
        IsAdsPlaying = false;
        if (onRewardFailed != null)
            onRewardFailed();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
#if UNITY_IOS || UNITY_ANDROID
            //Cubekatana.Resume();
#endif
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
#if UNITY_IOS || UNITY_ANDROID
            //Cubekatana.Pause();
#endif
        }
    }
}
