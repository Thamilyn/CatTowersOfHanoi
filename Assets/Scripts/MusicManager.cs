using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    public AudioClip Grid_Sound;
    public AudioClip Correct_Sound;
    public AudioClip MayanTimeTraveler_Music;
    private AudioSource musicSource;


    public void PlayMusic()
    {
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
