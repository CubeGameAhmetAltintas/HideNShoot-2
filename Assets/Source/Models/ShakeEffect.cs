using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : ObjectModel
{
    bool isShaking;
    float timer;
    float shakeValue;
    [SerializeField] Transform target;

    private void Update()
    {
        if (isShaking)
        {
            ShakeCam(shakeValue);

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                isShaking = false;
            }
        }
    }

    public void ShakeCam(float shakeValue)
    {
        Vector3 pos = target.localPosition;
        pos = Random.insideUnitSphere * shakeValue * Time.deltaTime;
        target.localPosition = pos;
    }

    public void SetShakeWithDuration(float duration, float shakeValue)
    {
        timer = duration;
        isShaking = true;
        this.shakeValue = shakeValue;
    }
}
