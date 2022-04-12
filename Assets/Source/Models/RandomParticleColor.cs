using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystemRenderer))]
public class RandomParticleColor : MonoBehaviour
{
    [SerializeField] ParticleSystemRenderer particle;
    [SerializeField] Material[] Mats;

    private void OnEnable()
    {
        particle.material = Mats[Random.Range(0, Mats.Length)];
    }

    private void Reset()
    {
        particle = GetComponent<ParticleSystemRenderer>();
    }
}
