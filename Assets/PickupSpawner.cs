using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public bool IsActive = false;
    public static int MaxInScene = 0;
    public static int Amount = 3;
    private int prefabsAmount;
    private int previousPrefab;
    private Vector3 NewRandomSpawn;

    private void Start()
    {
        previousPrefab = prefabsAmount;
    }
    void Update()
    {
        if (MaxInScene < 2)
        {
            Invoke(nameof(SpawnObjectAtRandom), 10F);
        }

    }

    void SpawnObjectAtRandom()
    {
        prefabsAmount = Random.Range(0, Amount);
        while (previousPrefab == prefabsAmount)
        {
            print("Find another random number");
            prefabsAmount = Random.Range(0, Amount);
        }
        NewRandomSpawn = new(Random.Range(140, 240), 1, Random.Range(110, 180));
        previousPrefab = prefabsAmount;
        Instantiate(prefabs[prefabsAmount], NewRandomSpawn, Quaternion.Euler(-90, 0, 0));
        MaxInScene++;       
        CancelInvoke();
    }

}
