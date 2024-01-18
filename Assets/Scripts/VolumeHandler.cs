using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeHandler : MonoBehaviour
{
    //public AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void SetVolume()
    {
        //print(volume);
        // audioMixer.SetFloat("volume", volume);
        GameObject CheckIfMute = GameObject.FindGameObjectWithTag("Mute");
        MuteBackground MuteScript = CheckIfMute.GetComponent<MuteBackground>();
        if (MuteScript.muted == false)
        {
            AudioListener.volume = volumeSlider.value;
        }
        Save();
    }
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
}
