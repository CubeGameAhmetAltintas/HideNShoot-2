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

        velocity = (Direction - FirePoint) / 2f;
        SetActive();
    }

    public void movementUpdate()
    {
        // bullet movement
        transform.position += velocity * speed * Time.deltaTime;
    }

    public void OnHitTarget()
    {
        // bullet onTrigger
        // bullet damage
        // FX
        SetDeactive();
    }

}
