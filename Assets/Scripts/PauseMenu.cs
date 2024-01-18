using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Slider SensXSlider, SensYSlider;
    public GameObject PauseMenuUI, SettingsMenuUI;
    [SerializeField] Button SettingsButton, backButton;
    private void Start()
    {
        backButton.Select();
        LoadSensX();
        LoadSensY();
    }
    public void SensitivityX()
    {
        print("Change Sens X");
        CameraRotation.sensX = SensXSlider.value;
        SaveSensX();
       
    }
    public void SensitivityY()
    {
        print("Change Sens Y");
       CameraRotation.sensY = SensYSlider.value;
       SaveSensY();           
    }
    private void LoadSensX()
    {
        SensXSlider.value = PlayerPrefs.GetFloat("SensX");
        CameraRotation.sensX = SensXSlider.value;
    }
    private void SaveSensX()
    {
        PlayerPrefs.SetFloat("SensX", SensXSlider.value);
        PlayerPrefs.Save();
    }
    private void LoadSensY()
    {
        SensYSlider.value = PlayerPrefs.GetFloat("SensY");
        CameraRotation.sensY = SensYSlider.value;
    }
    private void SaveSensY()
    {
        PlayerPrefs.SetFloat("SensY", SensYSlider.value);
        PlayerPrefs.Save();
    }
    private void Update()
    {
        if (SettingsMenuUI.activeInHierarchy)
        {
            if (Gamepad.current.bButton.wasPressedThisFrame)
            {
                print("B");
                PauseMenuUI.SetActive(true);
                SettingsMenuUI.SetActive(false);
                SettingsButton.Select();
            }
        }
    }
}
