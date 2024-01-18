using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketGun : MonoBehaviour
{
    public GameObject RocketProjectile;
    public GameObject enemy;
    public static int AiCarsInArea, SoldierAIinArea, ZombieAIinArea, OverallScore;
    public Collider[] colliders1, colliders2, colliders3, colliders;
    Add100Effect AddScoreForAiCars;
    SpawnPickups spawn;
    public static Vector3 EnemyLocations;
    private void Start()
    {
        AddScoreForAiCars = FindObjectOfType<Add100Effect>();
        spawn = FindObjectOfType<SpawnPickups>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AOEDamage();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy2"))
        {
            AOEDamage();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy3"))
        {
            AOEDamage();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
    }
    void AOEDamage()
    {
        /*float dist = Vector3.Distance(RocketProjectile.transform.position, enemy.transform.position);
        if(dist <= 6)
        {
            foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(Enemy);
                ScoreUI.score += 100;
            }
        }*/
        Vector3 explosionPosition = transform.position;
        float explosionRadius = 20f;
        /*colliders1 = Physics.OverlapSphere(explosionPosition, explosionRadius, AiCars);
        foreach (Collider carai in colliders1)
        {
            Destroy(carai.GetComponent<Collider>().gameObject);
            AiCarsInArea = colliders1.Length * 100;
            AddScoreForAiCars.ScoreAdditionUI.enabled = true;                       
            AddScoreForAiCars.RocketAdd100();
        }
        ScoreUI.score += AiCarsInArea;
        colliders2 = Physics.OverlapSphere(explosionPosition, explosionRadius, SoldierAI);
        foreach (Collider soldierai in colliders2)
        {
            Destroy(soldierai.GetComponent<Collider>().gameObject);
            SoldierAIinArea = colliders2.Length * 50;
            AddScoreForAiCars.ScoreAdditionUI.enabled = true;
            AddScoreForAiCars.RocketAdd50();
        }
        ScoreUI.score += SoldierAIinArea;
        colliders3 = Physics.OverlapSphere(explosionPosition, explosionRadius, ZombieAI);
        foreach (Collider zombieai in colliders3)
        {
            Destroy(zombieai.GetComponent<Collider>().gameObject);
            SoldierAIinArea = colliders2.Length * 300;
            AddScoreForAiCars.ScoreAdditionUI.enabled = true;
            AddScoreForAiCars.RocketAdd300();
        }
        ScoreUI.score += ZombieAIinArea;*/
        colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (Collider col in colliders)
        {
            if (col.GetComponent<Collider>().gameObject.name == "AICar")
            {
                //EnemyLocations = col.GetComponent<Collider>().transform.position;
                Destroy(col.GetComponent<Collider>().gameObject);
                AiCarsInArea++;
               // spawn.SpawnPickupOnRocket();
            }
            if (col.GetComponent<Collider>().gameObject.name == "AISoldier")
            {
                //EnemyLocations = col.GetComponent<Collider>().transform.position;
                Destroy(col.GetComponent<Collider>().gameObject);
                SoldierAIinArea++;
                //spawn.SpawnPickupOnRocket();
            }
            if (col.GetComponent<Collider>().gameObject.name == "AIZombie")
            {
                //EnemyLocations = col.GetComponent<Collider>().transform.position;
                Destroy(col.GetComponent<Collider>().gameObject);
                ZombieAIinArea++;
                //spawn.SpawnPickupOnRocket();
            }
            if ((col.GetComponent<Collider>().gameObject.name == "AICar") || (col.GetComponent<Collider>().gameObject.name == "AISoldier") || (col.GetComponent<Collider>().gameObject.name == "AIZombie"))
            {
                EnemyLocations = col.GetComponent<Collider>().transform.position;
                OverallScore = (AiCarsInArea * 100) + (SoldierAIinArea * 50) + (ZombieAIinArea * 300);
                AddScoreForAiCars.ScoreAdditionUI.enabled = true;
                AddScoreForAiCars.RocketAdd();
                spawn.SpawnPickupOnRocket();
            }
        }
        ScoreUI.score += OverallScore;


        /*foreach (Collider col in colliders)
        {
            if (col.GetComponent<Collider>().CompareTag("Enemy"))
            {
                Destroy(col.GetComponent<Collider>().gameObject);
                ScoreUI.score += 100;
            }
            if (col.GetComponent<Collider>().CompareTag("Enemy2"))
            {
                Destroy(col.GetComponent<Collider>().gameObject);
                ScoreUI.score += 50;
            }
        }*/
    }

}
