using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public GameObject[] prefabs;
    private float prefabsAmount;
    public static GameObject bulletpickups, healthpickups, destroypickups;
    private GameObject[] pickups;
    public GameObject player;

    private void Start()
    {
        pickups = GameObject.FindGameObjectsWithTag("PickUps");
    }
    public void SpawnPickupOnDeath()
    {
        player = GameObject.Find("PlayerCharacter");
        prefabsAmount = Random.value;
        if (player != null)
        {
            if (prefabsAmount < 0.9) // 50% chance
            {
                print("Spawned Bullet");
                bulletpickups = Instantiate(prefabs[0], GunShoot.EnemyLocation + new Vector3(0f, 0.2f, 0f), Quaternion.Euler(-90, 0, 0));
                Destroy(bulletpickups, 12f);
            }
            else // 10% chance
            {
                print("Spawned Nuke");
                destroypickups = Instantiate(prefabs[1], GunShoot.EnemyLocation + new Vector3(0f, 0.2f, 0f), Quaternion.Euler(-90, 0, 0));
                Destroy(destroypickups, 12f);
            }
        }
        else
        {
            if (prefabsAmount < 0.9) // 50% chance
            {
                bulletpickups = Instantiate(prefabs[0], BulletDetection.EnemyLocation + new Vector3(0f, 0.2f, 0f), Quaternion.Euler(-90, 0, 0));
                Destroy(bulletpickups, 12f);
            }
            else // 10% chance
            {
                destroypickups = Instantiate(prefabs[1], BulletDetection.EnemyLocation + new Vector3(0f, 0.2f, 0f), Quaternion.Euler(-90, 0, 0));
                Destroy(destroypickups, 12f);
            }
        }
    }
    public void SpawnPickupOnRocket()
    {
        prefabsAmount = Random.value;
            if (prefabsAmount < 0.9)
            {
                bulletpickups = Instantiate(prefabs[0], RocketGun.EnemyLocations + new Vector3(0, 0.2f, 0), Quaternion.Euler(-90, 0, 0));
                //Destroy(bulletpickups, 8f);
                StartCoroutine(nameof(StartOtherCoroutine));
            }
            else
            {
                destroypickups = Instantiate(prefabs[1], RocketGun.EnemyLocations + new Vector3(0, 0.2f, 0), Quaternion.Euler(-90, 0, 0));
                //Destroy(destroypickups, 8f);
                StartCoroutine(nameof(StartOtherCoroutine));
            }
        
    }
    IEnumerator StartOtherCoroutine()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(nameof(DestroyOneByOne));
    }
    IEnumerator DestroyOneByOne()
    {
        foreach (GameObject Pickups in GameObject.FindGameObjectsWithTag("PickUps"))
        {
            Destroy(Pickups);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
