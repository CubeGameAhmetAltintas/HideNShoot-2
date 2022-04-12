using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gStoneBase.Sounds
{
    public class SoundBaseModel : ObjectModel
    {
        public AudioSource AudioSource;

        public virtual void SetVolume(float value)
        {
            AudioSource.volume = value;
        }

        public virtual void Play()
        {
            AudioSource.Play();
        }

        public override void SetDeactive()
        {
            AudioSource.Stop();
        }

        public virtual void Disable()
        {
            base.SetDeactive();
        }

    }
}
