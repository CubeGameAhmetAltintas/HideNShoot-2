using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponModel : ObjectModel
{
    public Transform Target;
    [SerializeField] CinemachineVirtualCamera camera;
    [SerializeField] PoolModel bulletPool;
    [SerializeField] Slider sliderValue;
    private float zoomValue;

    private void shoot()
    {
        BulletModel bullet = bulletPool.GetDeactiveItem() as BulletModel;
        bullet.transform.position = transform.position;
        bullet.Shoot(transform.position, new Vector3(0,0,transform.position.z + 5f), 5f);
    }

    private void aimUpdate()
    {
        
    }

    public void WeaponUpdate()
    {
        camera.SetActive(true);
        if (Input.GetMouseButtonUp(0))
            shoot();
    }

    public void OnZoomValueChange()
    {
        zoomValue = sliderValue.value;
    }
}
