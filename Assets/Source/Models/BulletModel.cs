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
    private Vector3 velocity;
    private float timer;
    [SerializeField] ParticleSystem hitFX;

    private void Update()
    {
        if (IsActiveInHierarchy)
        {
            movementUpdate(); //TODO call it elsewhere ??
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

    public void Shoot(Vector3 start, Vector3 end, float speed)
    {
        FirePoint = start;
        Direction = end;
        this.speed = speed;

        velocity = (Direction - FirePoint);
        hitFX.transform.SetParent(transform);
        hitFX.transform.position = transform.position;
        
        SetActive();
    }

    public void movementUpdate()
    {
        // bullet movement
        transform.position += velocity * speed * Time.deltaTime;
    }

    public void OnHitTarget()
    {
        // FX
        SetDeactive();
        hitFX.transform.SetParent(null); //TODO ??
        hitFX.Play();
    }

}
