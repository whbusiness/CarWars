using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public GameObject[] Characters;
    public int CurrentCharacter;
    //public Button NextButton;
    public Button PrevButton;
    public GameObject PreviousButton;
    public GameObject NextButton;
    public Button NxtButton;
    private void Start()
    {
        PreviousButton.SetActive(false);
    }
    public void OnNext()
    {
        Characters[CurrentCharacter].SetActive(false);
        if(CurrentCharacter < 1)
        {
            CurrentCharacter++;
        }
        Characters[CurrentCharacter].SetActive(true);
        if(CurrentCharacter == 1)
        {
            NextButton.SetActive(false);
            PreviousButton.SetActive(true);
            PrevButton.Select();
        }
    }
    public void OnPrevious()
    {
        Characters[CurrentCharacter].SetActive(false);
        if(CurrentCharacter > 0)
        {
            CurrentCharacter--;
        }
        Characters[CurrentCharacter].SetActive(true);
        if(CurrentCharacter == 0)
        {
            NextButton.SetActive(true);
            PreviousButton.SetActive(false);
            NxtButton.Select();
        }
    }

    public void OnStart()
    {
        PlayerPrefs.SetInt("CurrentCharacter", CurrentCharacter);
        SceneManager.LoadScene("Level1");
    }
}
