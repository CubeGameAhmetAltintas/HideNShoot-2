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

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }

    public void ShootPlayer()
    {
        if (player.Health <= 0)
        {
            StateUpdate(EnemyStates.Idle);
            return;
        }
        BulletModel bullet = (bulletPool.GetDeactiveItem() as BulletModel);
        bullet.Shoot(firePoint.position, transform.forward, fireSpeed);
    }
    public void EnemyIdlePoint()
    {
        var randomPointX = Random.Range(-0.5f, 1f);
        var randomPointZ = Random.Range(0.5f, 2f);
        initialPoit = transform.position;
        walkPoint = initialPoit + new Vector3(randomPointX, 0, randomPointZ);
    }

    private void idleUpdate()
    {
        transform.rotation = Quaternion.RotateTowards(
        transform.rotation,
        Quaternion.LookRotation(walkPoint - transform.position),
        Time.deltaTime * 180f);

        transform.position = Vector3.MoveTowards(transform.position, walkPoint, 0.5f * Time.deltaTime);
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
        if (TutorialController.Controller.ActiveLesseonIndex == 0)
            return;

        if (GameplayTypeController.CurrentType == GameplayTypes.Sniper)
            return;

        transform.LookAt(player.transform);
        if (timer <= fireRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            animator.SetTrigger("Fire");
            ShootPlayer();
            shootFx.Play();
            timer = 0;
        }
    }

    public void EnemyUpdate()
    {
        StateUpdate(state);
    }

    public void StateUpdate(EnemyStates enemyState)
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