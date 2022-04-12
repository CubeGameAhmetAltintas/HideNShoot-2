using System;

public class IronSourceController : AdSDKController
{

#if IRONSOURCE_ACTIVE
    private static bool IsRewardSucces;
    private static Action<bool> OnRewardedClosed;

    public IronSourceController(string appKey)
    {
        Initialize(appKey);
    }

    public override void Initialize(string appKey)
    {
        base.Initialize(appKey);

        if (ActiveAdTypes == null)
        {
            IronSource.Agent.init(AppKey);
        }
        else
        {
            string[] activeAds = new string[ActiveAdTypes.Length];

            for (int i = 0; i < ActiveAdTypes.Length; i++)
            {
                switch (ActiveAdTypes[i])
                {
                    case AdTypes.RewardedVideo:
                        activeAds[i] = IronSourceAdUnits.REWARDED_VIDEO;
                        break;
                    case AdTypes.Interstital:
                        activeAds[i] = IronSourceAdUnits.INTERSTITIAL;
                        break;
                    case AdTypes.OfferWall:
                        activeAds[i] = IronSourceAdUnits.OFFERWALL;
                        break;
                    case AdTypes.Banner:
                        activeAds[i] = IronSourceAdUnits.BANNER;
                        break;
                    default:
                        break;
                }
            }

            IronSource.Agent.init(AppKey, activeAds);
        }

        OnRewardedClosed = OnRewardedVideoEnd;
        IronSource.Agent.validateIntegration();
    }

    public static void OnApplicationPause(bool pause)
    {
        IronSource.Agent.onApplicationPause(pause);
    }

    #region REWARDED_VIDEO

    private static void initRewardedVideo()
    {
        IronSourceEvents.onRewardedVideoAdOpenedEvent += rewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += rewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += rewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += rewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += rewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += rewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += rewardedVideoAdShowFailedEvent;
    }

    public override void LoadRewardedVideo()
    {
        base.LoadRewardedVideo();
        IronSource.Agent.isRewardedVideoAvailable();
    }

    public override void ShowRewardedVideo(Action onRewardSucces, Action onRewardFailed = null)
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IsRewardSucces = false;
            base.ShowRewardedVideo(onRewardSucces, onRewardFailed);
        }
        else
        {
            onRewardFailed?.Invoke();
        }
    }

    protected override void OnRewardedVideoEnd(bool isSucces)
    {
        base.OnRewardedVideoEnd(isSucces);
        IsRewardSucces = false;
    }

    private static void rewardedVideoAdShowFailedEvent(IronSourceError obj)
    {

    }

    private static void rewardedVideoAdRewardedEvent(IronSourcePlacement obj)
    {
        IsRewardSucces = true;
    }

    private static void rewardedVideoAdEndedEvent()
    {

    }

    private static void rewardedVideoAdStartedEvent()
    {

    }

    private static void rewardedVideoAvailabilityChangedEvent(bool obj)
    {

    }

    private static void rewardedVideoAdClosedEvent()
    {
        OnRewardedClosed?.Invoke(IsRewardSucces);
    }

    private static void rewardedVideoAdOpenedEvent()
    {

    }

    #endregion

    #region INTERSTITAL

    private static void initInterstital()
    {
        IronSourceEvents.onInterstitialAdReadyEvent += interstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += interstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += interstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += interstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += interstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += interstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += interstitialAdClosedEvent;
    }

    public override void LoadInterstital()
    {
        base.LoadInterstital();
        IronSource.Agent.loadInterstitial();
    }

    public override void ShowInterstital()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }
        else
        {
            LoadInterstital();
        }

        base.ShowInterstital();
    }

    private static void interstitialAdClosedEvent()
    {

    }

    private static void interstitialAdOpenedEvent()
    {

    }

    private static void interstitialAdClickedEvent()
    {

    }

    private static void interstitialAdShowFailedEvent(IronSourceError obj)
    {

    }

    private static void interstitialAdShowSucceededEvent()
    {

    }

    private static void interstitialAdLoadFailedEvent(IronSourceError obj)
    {

    }

    private static void interstitialAdReadyEvent()
    {

    }

    #endregion

    #region BANNER
    private static void initBanner()
    {
        IronSourceEvents.onBannerAdLoadedEvent += bannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += bannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent += bannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent += bannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent += bannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += bannerAdLeftApplicationEvent;
    }

    public override void LoadBanner()
    {
        base.LoadBanner();
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }

    public override void ShowBanner()
    {
        base.ShowBanner();
        IronSource.Agent.displayBanner();
    }

    public override void HideBannder()
    {
        base.HideBannder();
        IronSource.Agent.destroyBanner();
    }

    private static void bannerAdLeftApplicationEvent()
    {

    }

    private static void bannerAdScreenDismissedEvent()
    {

    }

    private static void bannerAdScreenPresentedEvent()
    {

    }

    private static void bannerAdClickedEvent()
    {

    }

    private static void bannerAdLoadFailedEvent(IronSourceError obj)
    {

    }

    private static void bannerAdLoadedEvent()
    {

    }

    #endregion

#endif
}
