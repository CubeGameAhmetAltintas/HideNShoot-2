public class EnemyModel : ObjectModel
{
    public int Id;
    public int Damage;
    private EnemyStates state;
    private PoolModel bulletPool;

    public void ShootPlayer(PlayerController player)
    {

    }

    private void idleUpdate()
    {
        print(this + " Idleeee");
    }

    private void aimUpdate()
    {
        print(this + " shooooot");
    }

    public void EnemyUpdate()
    {
        ChangeState(state);
    }
    //enemyUpdate - road model

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