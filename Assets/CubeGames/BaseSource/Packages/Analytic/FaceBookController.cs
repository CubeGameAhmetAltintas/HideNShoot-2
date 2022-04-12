using System.Collections.Generic;
#if FB_ACTIVE
using Facebook.Unity;
#endif

public class FaceBookController : AnalyticSDKController
{
#if FB_ACTIVE
    public override void Initialize()
    {
        base.Initialize();

        FB.Init(() =>
        {
            FB.ActivateApp();
        });
    }

    public void SendLogAppEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
    {
        FB.LogAppEvent(logEvent, valueToSum, parameters);
    }
#endif
}
