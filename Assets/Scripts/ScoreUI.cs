using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    // public Transform character;
    public static float score;
    public float Highscore;
    public GameObject playercharacter;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI WaveUI;
    public TextMeshProUGUI BulletsUI;
    public bool Player = false;
   // public GameObject CrossHair;
    private void Start()
    {        
        Highscore = PlayerPrefs.GetFloat("HighScore");
    }
    void Update()
    {
        playercharacter = GameObject.Find("PlayerCharacter");
        if (playercharacter != null)
        {
            GameObject Bulletsamount = GameObject.Find("PlayerCharacter");
            GunShoot Bullets = Bulletsamount.GetComponent<GunShoot>();
            BulletsUI.text = Bullets.BulletAmount.ToString("0");
        }
        else
        {
            GameObject Bulletsamount = GameObject.Find("RedCar");
            //CrossHair.SetActive(false);
            CarController Bullets = Bulletsamount.GetComponent<CarController>();
            BulletsUI.text = Bullets.AmountOfBullets.ToString("0");

        }
        //scoreUI.text = score.ToString();
        scoreUI.text = score.ToString("0");
        WaveUI.text = randomaispawner.WaveNumber.ToString("Wave: " + randomaispawner.WaveNumber);
        if (score > Highscore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
            PlayerPrefs.Save();
        }
    }
}
