using System;

public class AdSDKController
{
    public string AppKey;
    public AdTypes[] ActiveAdTypes;

    protected Action OnRewardedSucces, OnRewardedFail;

    public virtual void Initialize(string appKey)
    {
        this.AppKey = appKey;
    }

    public virtual void LoadRewardedVideo()
    {

    }

    public virtual void ShowRewardedVideo(Action onRewardSucces, Action onRewardFailed = null)
    {
        OnRewardedSucces = onRewardSucces;
        OnRewardedFail = onRewardFailed;

#if UNITY_EDITOR
        OnRewardedVideoEnd(true);
#endif
    }

    protected virtual void OnRewardedVideoEnd(bool isSucces)
    {
        if (isSucces)
        {
            OnRewardedSucces?.Invoke();
        }
        else
        {
            OnRewardedFail?.Invoke();
        }
    }

    public virtual void LoadInterstital()
    {

    }

    public virtual void ShowInterstital()
    {

    }

    public virtual void LoadBanner()
    {

    }

    public virtual void ShowBanner()
    {

    }

    public virtual void HideBannder()
    {

    }

}