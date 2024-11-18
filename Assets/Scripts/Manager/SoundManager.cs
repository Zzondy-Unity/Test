using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    private AudioSource musicSource;
    public AudioClip musicClip;

    private void Awake()
    {
        instance = this;
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = musicVolume;
        musicSource.loop = true;
    }

    private void Start()
    {
        ChangeBackgroundMusic(musicClip);
    }

    private void ChangeBackgroundMusic(AudioClip musicClip)
    {
        instance.musicSource.Stop();
        instance.musicSource.clip = musicClip;
        instance.musicSource.Play();
    }

    public static void PlayerClip(AudioClip clip)
    {
        GameObject obj = GameManager.Instance.ObjectPool.SpawnFromPool("SoundSource");
        obj.SetActive(true);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }


    public void PauseBGM()
    {
        instance.musicSource.Pause();
    }

    public void ResumeBGM()
    {
        instance.musicSource.UnPause();
    }
}
