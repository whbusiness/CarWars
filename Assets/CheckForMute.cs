using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckForMute : MonoBehaviour
{
    private Scene currentScene;
    private Toggle MuteToggle;
    public bool IsToggled = false;
    void Awake()
    {
        MuteToggle = GameObject.Find("MuteToggle").GetComponent<Toggle>();
        currentScene = SceneManager.GetActiveScene();
        CheckMute();
    }

    void CheckMute()
    {
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            MuteToggle.isOn = true;
        }
        else
        {
            MuteToggle.isOn = false;
        }
    }
    private void Update()
    {
        if (MuteToggle.isOn)
        {
            PlayerPrefs.SetInt("Mute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
        }
    }

}
