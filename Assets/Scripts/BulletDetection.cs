using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletDetection : MonoBehaviour
{
    public ParticleSystem explosion;
    Add100Effect ScoreOnDeathDisplay;
    public GameObject[] prefabs;
    public static int MaxInScene = 0;
    public static int Amount = 3;
    private int prefabsAmount;
    public static Vector3 EnemyLocation;
    SpawnPickups Spawner;
    private void Start()
    {
        ParticleSystem explosion = GetComponent<ParticleSystem>();
        ScoreOnDeathDisplay = FindObjectOfType<Add100Effect>();
        Spawner = FindObjectOfType<SpawnPickups>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "AICar")
        {
            collision.collider.gameObject.GetComponent<AICar>().AICarHealth -= 100;
            Destroy(gameObject);
            if (collision.collider.gameObject.GetComponent<AICar>().AICarHealth < 1)
            {
                ScoreUI.score += 100;
                Destroy(collision.gameObject);
                PlayExplosion();
                EnemyLocation = transform.position;
                Spawner.SpawnPickupOnDeath();
                ScoreOnDeathDisplay.ScoreAdditionUI.enabled = true;
                ScoreOnDeathDisplay.Add100();
            }
        }
        if (collision.collider.gameObject.name == "AISoldier")
        {
            collision.collider.gameObject.GetComponent<SoldierAI>().EnemyHealth -= 100;
            Destroy(gameObject);
            if(collision.collider.gameObject.GetComponent<SoldierAI>().EnemyHealth < 1)
            {
                ScoreUI.score += 50;
                Destroy(collision.gameObject);
                PlayExplosion();
                EnemyLocation = transform.position;
                Spawner.SpawnPickupOnDeath();
                ScoreOnDeathDisplay.ScoreAdditionUI.enabled = true;
                ScoreOnDeathDisplay.Add50();
            }
        }
        if (collision.collider.gameObject.name == "AIZombie")
        {
            print("HitZombie");
            collision.collider.gameObject.GetComponent<ZombieAi>().ZombieHealth -= 100;
            Destroy(gameObject);
            if (collision.collider.gameObject.GetComponent<ZombieAi>().ZombieHealth < 1)
            {
                ScoreUI.score += 300;
                Destroy(collision.gameObject);
                PlayExplosion();
                EnemyLocation = transform.position;
                Spawner.SpawnPickupOnDeath();
                ScoreOnDeathDisplay.ScoreAdditionUI.enabled = true;
                ScoreOnDeathDisplay.Add300();
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        
    }
    void PlayExplosion()
    {
        Instantiate(explosion, transform.position, explosion.transform.rotation);
    }
    
}