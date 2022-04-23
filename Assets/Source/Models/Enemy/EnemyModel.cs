using System.Collections;
using UnityEngine;

public class EnemyModel : ObjectModel
{
    public int Id;
    public int Damage;
    private EnemyStates state;
    [SerializeField] PoolModel bulletPool; //TODO ???
    private PlayerController player;
    float timer = 0;
    [SerializeField] float fireRate;
    [SerializeField] float fireSpeed;
    [SerializeField] Animator animator;
    [SerializeField] Transform firePoint;
    [SerializeField] ParticleSystem shootFx;
    Vector3 walkPoint;
    Vector3 initialPoit;
    public bool isWalking;

    public void ShootPlayer(PlayerController player)
    {
        this.player = player;
        if (player.Health <= 0)
        {
            ChangeState(EnemyStates.Idle);
            return;
        }
        BulletModel bullet = (bulletPool.GetDeactiveItem() as BulletModel);
        bullet.transform.position = firePoint.position;
        bullet.Shoot(firePoint.position, new Vector3(player.transform.position.x, 1f, player.transform.position.z), fireSpeed);
    }
    public void EnemyIdlePoint()
    {
        var randomPointX = Random.Range(-1f, 1f);
        var randomPointZ = Random.Range(-0.9f, 0.1f);
        initialPoit = transform.position;
        walkPoint = initialPoit + new Vector3(randomPointX, 0, randomPointZ);
    }

    private void idleUpdate()
    {
        transform.LookAt(walkPoint);
        transform.position = Vector3.MoveTowards(transform.position, walkPoint, 1f * Time.deltaTime);
        if (transform.position == walkPoint)
        {
            walkPoint = initialPoit;
            if (transform.position == initialPoit)
                EnemyIdlePoint();
        }

        animator.SetBool("isFiring", false);
    }

    private void Update()
    {
        if (state != EnemyStates.Shoot && isWalking)
            idleUpdate(); //TODO might change
        else
            animator.SetBool("isFiring", true);
    }

    private void aimUpdate()
    {
        animator.SetBool("isFiring", true);
        transform.LookAt(player.transform);
        if (timer <= fireRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            animator.SetBool("isFiring", true);
            ShootPlayer(player);
            shootFx.Play();
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