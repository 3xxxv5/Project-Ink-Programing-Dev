using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;

    public void PlaySFX(AudioData audioData)
    {
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.vol);
    }

    public void PlayRandomSFX(AudioData[] audioData)
    {
        PlaySFX(audioData[Random.Range(0, audioData.Length)]);
    }
}

[SerializeField] public class AudioData
{
    public AudioClip audioClip;
    public float vol;
}
