using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] soundEffects;   
    public AudioSource musicSource;
    public AudioSource soundEffectSource;

    private float musicVolume = 1.0f;
    private float soundEffectVolume = 1.0f;

    private static SoundManager _instance;

    public static SoundManager instance { get {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();
            }

            return _instance;
        } }


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void PlayMusic()
    {
        musicSource.Play();
    }

    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length)
        {
            soundEffectSource.clip = soundEffects[index];
            soundEffectSource.Play();
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = volume;
        soundEffectSource.volume = soundEffectVolume;
    }

    public void PlayWinSound()
    {
        PlaySoundEffect(0);
    }
}