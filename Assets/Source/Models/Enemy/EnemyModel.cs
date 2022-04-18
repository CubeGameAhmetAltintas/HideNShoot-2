using System.Collections;
using UnityEngine;

public class EnemyModel : ObjectModel
{
    public int Id;
    public int Damage;
    private EnemyStates state;
    [SerializeField] PoolModel bulletPool; //TODO ???
    [SerializeField] PlayerController player; //TODO ???
    
    float timer = 0;
    [SerializeField] float fireRate;

    public void ShootPlayer(PlayerController player)
    {
        if (player.Health <= 0)
        {
            ChangeState(EnemyStates.Idle);
            return;
        }
        BulletModel bullet = (bulletPool.GetDeactiveItem() as BulletModel);
        bullet.transform.position = transform.position;
        bullet.Shoot(transform.position, player.transform.position, 5f);
    }

    private void idleUpdate()
    {
    }
    
    private void aimUpdate()
    {
        if(timer <= fireRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            ShootPlayer(player);
            timer = 0;
        }
    }

    public void EnemyUpdate()
    {
        ChangeState(state);
    }

    public void ChangeState(EnemyStates enemyState)
    {
        state = enemyState;
        switch (state)
        {
            case EnemyStates.Idle:
                idleUpdate();
                break;
            case EnemyStates.Shoot:
                aimUpdate();
                break;
        }
    }
}

public enum EnemyStates
{
    Idle,
    Shoot
}