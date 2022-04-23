using System;
using UnityEngine;

public class CharacterModel : ObjectModel
{
    [SerializeField] PlayerController playerController;
    [SerializeField] ParticleSystem upgradeFx;
    [SerializeField] Animator animator;

    public void StartMove()
    {
        animator.SetBool("isMoving", true);
    }

    public void StopMoving()
    {
        animator.SetBool("isMoving", false);
    }

    public void Dying()
    {
        animator.SetTrigger("isKilled");
    }
}
