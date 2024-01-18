using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ArrowInPauseMenu : MonoBehaviour
{
    private RectTransform thisObj;
    public GameObject backButton, MuteToggle, volume, sensx, sensy;
    private Scene currentScene;
    private GameObject FullScreenToggle;
    private void Start()
    {
        FullScreenToggle = GameObject.Find("FullScreen");
        currentScene = SceneManager.GetActiveScene();
        thisObj = GameObject.Find("ArrowForSettings").GetComponent<RectTransform>();
        if (currentScene.name == "Level1")
        {
            thisObj.localPosition = new Vector3(-70.4000015f, -42.4000015f, 0);
        }
        else if(currentScene.name == "StartMenu")
        {
            thisObj.localPosition = new Vector3(-51.4000015f, -74f, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (currentScene.name == "StartMenu")
        {
            if (EventSystem.current.currentSelectedGameObject == backButton)
            {
                thisObj.localPosition = new Vector3(-51.4000015f, -74, 0);
            }
            else if (EventSystem.current.currentSelectedGameObject == MuteToggle)
            {
                thisObj.localPosition = new Vector3(-57.5999985f, -45.2999992f, 0);
            }
            else if (EventSystem.current.currentSelectedGameObject == sensx)
            {
                thisObj.localPosition = new Vector3(-97.3000031f, -23.3999996f, 0);
            }
            else if (EventSystem.current.currentSelectedGameObject == sensy)
            {
                thisObj.localPosition = new Vector3(-97.3000031f, 8.80000019f, 0);
            }
            else if (EventSystem.current.currentSelectedGameObject == volume)
            {
                thisObj.localPosition = new Vector3(-65.0999985f, 43.7000008f, 0);
            }
        }
        else if (currentScene.name == "Level1")
        {
            if (EventSystem.current.currentSelectedGameObject == backButton)
            {
                thisObj.localPosition = new Vector3(-48.7999992f, -84.9000015f, 0);
            }
            else if (EventSystem.current.currentSelectedGameObject == MuteToggle)
            {
                thisObj.localPosition = new Vector3(-57f, -53.4000015f, 0);
            }
            else if (EventSystem.current.currentSelectedGameObject == sensx)
            {
                thisObj.localPosition = new Vector3(-94.5999985f, -32.2000008f, 0);
            }
            else if (EventSystem.current.currentSelectedGameObject == sensy)
            {
                thisObj.localPosition = new Vector3(-94.5999985f, 0.600000024f, 0);
            }
            else if (EventSystem.current.currentSelectedGameObject == volume)
            {
                thisObj.localPosition = new Vector3(-67.1999969f, 32.7999992f, 0);
            }
            else
            {
                thisObj.localPosition = new Vector3(-88.4000015f, 65.5999985f, 0);
            }
        }
    }
}
