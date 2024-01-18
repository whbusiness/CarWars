 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllEnemies : MonoBehaviour
{
    public GameObject DestroyEnemies;
    public static int zombies, soldiers, cars;
    public static int overallScore;
    Add100Effect AddScoreForAll;
    public GameObject[] Enemies;
    private GameObject player;
    void Start()
    {
        DestroyEnemies = GameObject.FindGameObjectWithTag("Destroy");
        AddScoreForAll = FindObjectOfType<Add100Effect>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        else 
        {
            player = GameObject.Find("PlayerCharacter");
            Destroy(gameObject);

            /*Enemies[0] = GameObject.Find("AICar");
            Enemies[1] = GameObject.Find("AISoldier");
            Enemies[2] = GameObject.Find("AIZombie");*/
            foreach (GameObject enemies in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if (enemies.name == "AICar")
                {
                    if (player != null)
                    {
                        GunShoot.HealthBarCanvas.transform.SetParent(null);
                    }
                    Destroy(enemies);
                    cars++;
                }
                if(enemies.name == "AISoldier")
                {
                    if(player != null)
                    { 
                        GunShoot.HealthBarCanvas.transform.SetParent(null);
                    }
                    Destroy(enemies);
                    soldiers++;
                }
                if(enemies.name == "AIZombie")
                {
                    if (player != null)
                    {
                        GunShoot.HealthBarCanvas.transform.SetParent(null);
                    }
                    Destroy(enemies);
                    zombies++;
                }
            }
            /*if (Enemies[0] != null)
                {
                    // Destroy(randomaispawner.clone);
                    Destroy(Enemies[0]);
                    cars++;
                }
                if (Enemies[1] != null)
                {
                    Destroy(Enemies[1]);
                    // Destroy(randomaispawner.clone);
                    soldiers++;
                }
                if (Enemies[2] != null)
                {
                    Destroy(Enemies[2]);
                    // Destroy(randomaispawner.clone);
                    zombies+= Enemies[2].;
                }*/
            
                overallScore = (cars * 100) + (soldiers * 50) + (zombies * 300);
            ScoreUI.score += overallScore;
            AddScoreForAll.ScoreAdditionUI.enabled = true;
            AddScoreForAll.KillAllAdd();
        }
    }
}
