using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] prefabs;
    public float Radius = 1;
    public bool IsActive = false;
    public static int MaxInScene = 0;
    public static int Amount = 3;
    private int prefabsAmount;
    private int previousPrefab;

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
            Vector3 RandomPos = Random.insideUnitCircle * Radius;
            RandomPos = new Vector3(RandomPos.x, 1, RandomPos.y);
            previousPrefab = prefabsAmount;
            Instantiate(prefabs[prefabsAmount], RandomPos, Quaternion.Euler(-90, 0, 0));
            MaxInScene++;
            // IsActive = true;        
            CancelInvoke();
        
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, Radius);
       
    }
}
