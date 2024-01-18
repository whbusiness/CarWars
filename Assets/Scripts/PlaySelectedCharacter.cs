using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlaySelectedCharacter : MonoBehaviour
{
    public GameObject[] CharacterPrefabs;
    private Vector3 PositionForSpawn = new(100f, 0, 100f);
    public Button primaryButton;
    public GameObject pauseMenuUI, OptionsMenuUI, RebindUI;
    public AudioSource Horn, Idle, Moving;
    private bool Pause = true;
    bool canPlaySFX = true;
    public GameObject RedCar;
    public Rigidbody _rb;
    public float ScoreByTime;
    private GameObject clone;
    public GameObject CrossHair;

    void Start()
    {
        int CurrentCharacter = PlayerPrefs.GetInt("CurrentCharacter");
        GameObject characters = CharacterPrefabs[CurrentCharacter];
        clone = Instantiate(characters, PositionForSpawn, Quaternion.identity);
        if (CurrentCharacter == 0)
        {
            clone.name = "RedCar";
        }
        if (CurrentCharacter == 1)
        {
            clone.name = "PlayerCharacter";
        }
        RedCar = GameObject.Find("RedCar");
        if (RedCar != null)
        {
            _rb = RedCar.GetComponent<Rigidbody>();
        }
    }
    private void Update()
    {
        if (RedCar != null && canPlaySFX)
        {
            PLaySFX();
        }
        Gamepad gamepad = Gamepad.current;
        if (Pause)
        {
            ScoreUI.score += ScoreByTime;
            if (gamepad == null)
            {
                canPlaySFX = false;
                CrossHair.SetActive(false);
                pauseMenuUI.SetActive(true);
                primaryButton.Select();
                Time.timeScale = 0;
            }
            else
            {
                if (RedCar == null)
                {
                    CrossHair.SetActive(true);
                }
                canPlaySFX = true;
                pauseMenuUI.SetActive(false);
                OptionsMenuUI.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if (!canPlaySFX)
        {
            Idle.Stop();
            Moving.Stop();
        }
    }

    public void OnPause()
    {
        if (Pause)
        {
            canPlaySFX = false;
            pauseMenuUI.SetActive(true);
            primaryButton.Select();
            Time.timeScale = 0;
            if (RedCar == null)
            {
                CrossHair.SetActive(false);
            }
            Pause = false;
        }
        else
        {
            if (RedCar == null)
            {
                CrossHair.SetActive(true);
            }
            ResumeGame();
            canPlaySFX = true;
        }
    }
    public void ResumeGame()
    {
        if (RedCar == null)
        {
            CrossHair.SetActive(true);
        }
        pauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(false);
        RebindUI.SetActive(false);
        Time.timeScale = 1;
        Pause = true;
    }
    public void PLaySFX()
    {
        if (_rb.velocity.magnitude > 0.5f)
        {
            Moving.Play();
        }
        else
        {
            Idle.Play();
        }
    }

    public void OnHorn()
    {
        if (RedCar != null)
        {
            Horn.Play();
            print("HORN");
        }
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
