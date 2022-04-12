using UnityEngine;

public class CamShakerModel : MonoBehaviour
{
    public Camera Camera;
    bool isShaking;
    float timer;
    float shakeValue;

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
        Vector3 pos = Camera.transform.localPosition;
        pos = Random.insideUnitSphere * shakeValue * Time.deltaTime;
        Camera.transform.localPosition = pos;
    }

    public void SetShakeWithDuration(float duration, float shakeValue)
    {
        timer = duration;
        isShaking = true;
        this.shakeValue = shakeValue;
    }
}
