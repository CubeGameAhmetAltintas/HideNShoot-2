using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioModel : ObjectModel
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;

    public void PlaySound(float volume, bool randomPitch = false, float min = 0, float max = 0)
    {
        if (randomPitch)
            SetRandomPitch(min, max);

        audioSource.volume = volume;
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayLoopSound(float volume, bool randomPitch = false, float min = 0, float max = 0)
    {
        audioSource.volume = volume;
        if (audioSource.isPlaying == false)
        {
            if (randomPitch)
            {
                SetRandomPitch(min, max);
            }

            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void SetRandomPitch(float min, float max)
    {
        audioSource.pitch = Random.Range(min, max);
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    private void Reset()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
