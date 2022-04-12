using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleBaseModel : ObjectModel
{
    public ParticleSystem Particle;
    public ParticleSystem.MainModule Main;
    public ParticleSystem[] SubParticles;

    public override void Initialize()
    {
        Main = Particle.main;
        Main.stopAction = ParticleSystemStopAction.Callback;
        base.Initialize();
    }

    public ParticleBaseModel SetActiveWithPosition(Vector3 pos)
    {
        transform.position = pos;
        SetActive();
        return this;
    }

    public ParticleBaseModel SetActiveWithLocalPosition(Vector3 pos)
    {
        transform.localPosition = pos;
        SetActive();
        return this;
    }

    public ParticleBaseModel SetStartColor(Color color, bool withChild)
    {
        Main.startColor = color;

        if (withChild)
        {
            for (int i = 0; i < SubParticles.Length; i++)
            {
                var main = SubParticles[i].main;
                main.startColor = color;
            }
        }
        return this;
    }

    public void OnParticleSystemStopped()
    {
        Disable();
    }

    public override void SetDeactive()
    {
        Particle.Stop();
    }


    public virtual void Disable()
    {
        base.SetDeactive();
    }

    private void Reset()
    {
        if (Particle == null)
        {
            Particle = GetComponent<ParticleSystem>();
        }
    }
}
