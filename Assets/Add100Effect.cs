using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Add100Effect : MonoBehaviour
{

    public float score1, score2, score3;
    public TextMeshProUGUI ScoreAdditionUI;
    public float timer;
    public float time = 2f;

    public void Add100()
    {
        ScoreAdditionUI.text = score1.ToString("+ 0");
        StartCoroutine(nameof(Displayit));
    }
    public void Add50()
    {
        ScoreAdditionUI.text = score2.ToString("+ 0");
        StartCoroutine(nameof(Displayit));
    }
    public void Add300()
    {
        ScoreAdditionUI.text = score3.ToString("+ 0");
        StartCoroutine(nameof(Displayit));
    }
    public void RocketAdd()
    {
        ScoreAdditionUI.text = RocketGun.OverallScore.ToString("+ 0");
        StartCoroutine(nameof(Displayit2));
    }
    public void KillAllAdd()
    {
        ScoreAdditionUI.text = DestroyAllEnemies.overallScore.ToString("+ 0");
        StartCoroutine(nameof(Displayit3));
    }


    IEnumerator Displayit()
    {
        yield return new WaitForSeconds(2f);
        ScoreAdditionUI.enabled = false;
    }
    IEnumerator Displayit2()
    {
        yield return new WaitForSeconds(2f);
        ScoreAdditionUI.enabled = false;
        RocketGun.AiCarsInArea = 0;
        RocketGun.SoldierAIinArea = 0;
        RocketGun.ZombieAIinArea = 0;
        RocketGun.OverallScore = 0;
    }
    IEnumerator Displayit3()
    {
        yield return new WaitForSeconds(2f);
        ScoreAdditionUI.enabled = false;
        DestroyAllEnemies.zombies = 0;
        DestroyAllEnemies.soldiers = 0;
        DestroyAllEnemies.cars = 0;
        DestroyAllEnemies.overallScore = 0;
    }
}
