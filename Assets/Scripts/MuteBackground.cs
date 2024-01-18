using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteBackground : MonoBehaviour
{
    [SerializeField] Button MuteButton;
    public bool muted = false;
    private void Start()
    {
        Time.timeScale = 1;
    }
    
    public void OnMute(bool isMuted)
    {
        if (isMuted)
        {
            muted = true;
            AudioListener.volume = 0;
            //PlayerPrefs.SetInt("MuteButton", 0);
            //PlayerPrefs.Save();
        }
        else
        {
            muted = false;
            GameObject GetSliderVolume = GameObject.FindGameObjectWithTag("Slider");
            VolumeHandler GetSliderVolumeScript = GetSliderVolume.GetComponent<VolumeHandler>();
            GetSliderVolumeScript.SetVolume();
           // PlayerPrefs.SetInt("MuteButton", 1);
           // PlayerPrefs.Save();
        }
    }
    /*public void ValueChange()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("MuteVolume", );
    }*/

    // public void OnLoad()
    //{

    //}
    /*public void OnResume()
    {
        AudioListener.volume = 1;
    }*/
}
