using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DisplayStats : MonoBehaviour
{
    // public Transform character;
    public TextMeshProUGUI KillsUI;
    public TextMeshProUGUI DamageDealtUI;
    public TextMeshProUGUI HighScoreUI;

    private void Start()
    {
        InputSystem.ResetHaptics();
        KillsUI.text = GunShoot.AmountOfKills.ToString("Kills: " + GunShoot.AmountOfKills);
        //DamageDealtUI.text = GunShoot.DamageDealt.ToString("Damage Dealt: " + GunShoot.DamageDealt);
        DamageDealtUI.text = GunShoot.DamageDealt.ToString();
        HighScoreUI.text = "Highscore is:" + PlayerPrefs.GetFloat("HighScore").ToString(" 0");
    }
}
