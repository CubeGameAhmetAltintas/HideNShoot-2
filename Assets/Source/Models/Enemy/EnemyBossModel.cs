using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossModel : ObjectModel
{
    [SerializeField] Rigidbody[] rigidBodies;
    [SerializeField] Animator animator;

    public void OnHit(BulletModel bullet)
    {
        animator.enabled = false;
        foreach (var rb in rigidBodies)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(100, bullet.transform.position, 10);
        }

        bullet.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            OnHit(other.GetComponent<BulletModel>());
        }
    }

}
