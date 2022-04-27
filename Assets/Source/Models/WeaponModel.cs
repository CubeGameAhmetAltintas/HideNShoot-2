using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WeaponModel : ObjectModel
{
    public Transform Target;
    [SerializeField] CinemachineVirtualCamera camera;
    [SerializeField] PoolModel bulletPool;
    [SerializeField] Slider sliderValue;
    [SerializeField] Volume postProcessing;
    [SerializeField] float zoomValue;
    [SerializeField] float fireSpeed;
    float targetValue;

    public void OnAimStart()
    {
        camera.SetActive(true);
        targetValue = Random.Range(0.3f, 0.7f);
        sliderValue.maxValue = 1 + targetValue;
    }

    private void shoot()
    {
        BulletModel bullet = bulletPool.GetDeactiveItem() as BulletModel;
        bullet.Shoot(camera.transform.position + new Vector3(0,0,1), camera.transform.forward, fireSpeed);
    }

    public void WeaponUpdate()
    {
        if (Input.GetMouseButtonUp(0))
            shoot();


        camera.m_Lens.FieldOfView = Helpers.Maths.GetValueWithPercent(55, 2, zoomValue);
        camera.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset = Helpers.Vectors.GetValueWithPercent(new Vector3(-0.21f, 2.1f, 6), new Vector3(0, 1.79f, 6), zoomValue);
        ((Vignette)postProcessing.profile.components[1]).intensity.value = Helpers.Maths.GetValueWithPercent(0, 1, zoomValue);
    }

    public void OnZoomValueChange()
    {
        zoomValue = sliderValue.value > 1 ? 1 + (1 - sliderValue.value) : sliderValue.value;
    }
}
