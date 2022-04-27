using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModel : ObjectModel
{
    public float Speed;
    public int Damage = 10;
    public Vector3 Direction;
    public Vector3 FirePoint;
    private float speed;
    private float timer;
    [SerializeField] ParticleSystem hitFX;

    private void Update()
    {
        if (IsActiveInHierarchy)
        {
            movementUpdate();
            if (timer <= 2f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                SetDeactive();
            }
        }

    }

    public void Shoot(Vector3 start, Vector3 dir, float speed)
    {
        transform.position = start;
        FirePoint = start;
        Direction = dir;
        this.speed = speed;

        hitFX.transform.SetParent(transform);
        hitFX.transform.position = transform.position;
        
        SetActive();
    }

    public void movementUpdate()
    {
        // bullet movement
        transform.position += Direction * speed * Time.deltaTime;
    }

    public void OnHitTarget()
    {
        // FX
        SetDeactive();
        hitFX.transform.SetParent(null); //TODO ??
        hitFX.Play();
    }

}
