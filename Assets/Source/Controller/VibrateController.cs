using MoreMountains.NiceVibrations;
using UnityEngine;

public class VibrateController : ControllerBaseModel
{
    public static VibrateController Controller;
    float passedTime;

    public override void Initialize()
    {
        base.Initialize();
        Controller = this;
    }

    public void SetHaptic(VibrationTypes type)
    {
        if (Time.time < passedTime + 0.15f)
        {
            return;
        }

        passedTime = Time.time;

        switch (type)
        {
            case VibrationTypes.Light:
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
                break;
            case VibrationTypes.Medium:
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                break;
            case VibrationTypes.Heavy:
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                break;
            case VibrationTypes.Succes:
                MMVibrationManager.Haptic(HapticTypes.Success);
                break;
            case VibrationTypes.Fail:
                MMVibrationManager.Haptic(HapticTypes.Failure);
                break;
            case VibrationTypes.RigidImpact:
                MMVibrationManager.Haptic(HapticTypes.RigidImpact);
                break;
            case VibrationTypes.Soft:
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                break;
            case VibrationTypes.Warning:
                MMVibrationManager.Haptic(HapticTypes.Warning);
                break;
            default:
                break;
        }

    }
}