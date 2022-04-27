using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticEvent : MonoBehaviour
{
    public void SetHaptic(VibrationTypes type)
    {
        VibrateController.Controller.SetHaptic(type);
    }
}
