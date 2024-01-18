using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControl : MonoBehaviour
{
    public static MusicControl instance;
   /* bool canPlayBackgroundMusic = true;
    public AudioSource BGMusic;*/

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /*private void Update()
    {
        PlayBackgroundMusic();
        if (!canPlayBackgroundMusic)
        {
            BGMusic.Stop();
        }
    }
    public void OnResumeBackgroundMusic()
    {
        canPlayBackgroundMusic = true;
    }
    public void PlayBackgroundMusic()
    {
        BGMusic.Play();
    }*/
}
