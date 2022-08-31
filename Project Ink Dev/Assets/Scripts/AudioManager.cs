using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;
    private const string ENTER_SFX_PATH = "music/clickSFX1";
    private const string QUIT_SFX_PATH = "music/clickSFX2";

    public void PlaySFX(AudioData audioData)
    {
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.vol);
    }

    public void PlayRandomSFX(AudioData[] audioData)
    {
        PlaySFX(audioData[Random.Range(0, audioData.Length)]);
    }

    public void PlayClickEnterSFX()
    {
        AudioClip clip = Resources.Load<AudioClip>(ENTER_SFX_PATH);
        sFXPlayer.PlayOneShot(clip, 1f);
    }

    public void PlayClickQuitSFX()
    {
        AudioClip clip = Resources.Load<AudioClip>(QUIT_SFX_PATH);
        sFXPlayer.PlayOneShot(clip, 1f);
    }
}

[SerializeField] public class AudioData
{
    public AudioClip audioClip;
    public float vol;
}
