using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomaispawner : MonoBehaviour
{
    public GameObject[] AI;
    private float _TimeElapsed;
    public float Timer1;
    public float Timer2;
    public GameObject player;
    public static GameObject clone;
    public static int WaveNumber = 1;
    public int AmountOfEnemies;
    public bool DuringWave = true;
    public int NumberOfEnemiesToSpawn = 0;
    public int SpawnWaveEnemies = 10;
    public bool IncreaseSpawn = false;
    public float TIme, Timer;
    public static bool ChangeAimAccuracy = false;
    public Collider[] objectAvoidance;
    public float overlapRadius;
    private int randomindex;
    private Vector3 randomposition;
    private bool validPosition = false;
    private int objectsInScene;

    // Update is called once per frame
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        objectsInScene = 1 << 12;
        Timer = 3f;
        WaveNumber = 1;
        TIme = 0;
        GunShoot.ValueOfAccuracyWhenAiming = 0.8f;
        ChangeAimAccuracy = false;
        SpawnWaveEnemies = 10;
    }
    
    void Update()
    {
        //print(GunShoot.ValueOfAccuracyWhenAiming);
        if (WaveNumber < 10)
        {
            if (WaveNumber % 2 == 0 && !ChangeAimAccuracy)
            {
                GunShoot.ValueOfAccuracyWhenAiming -= 0.05f;
                //Timer -= 0.3f;                
                ChangeAimAccuracy = true;
            }
            if (WaveNumber % 2 != 0)
            {
                ChangeAimAccuracy = false;
            }
        }
        AmountOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (DuringWave)
        {
            TIme += Time.deltaTime;
            if (TIme > Timer)
            {
                if (NumberOfEnemiesToSpawn < SpawnWaveEnemies)
                {                    
                    randomindex = Random.Range(0, AI.Length);
                    randomposition = new(Random.Range(5, 180), 2, Random.Range(5, 180));
                    //Check if spawn point is overlapping the objects in the scene
                    while(Physics.CheckSphere(randomposition, overlapRadius, objectsInScene))
                    {
                        print("Away From Object");
                        randomposition = new(Random.Range(5, 180), 2, Random.Range(5, 180));
                    }
                    //Check if spawn point is near player
                    while (Vector3.Distance(randomposition, player.transform.position) < 30)
                    {
                        print("Away From Player");
                        randomposition = new(Random.Range(5, 180), 2, Random.Range(5, 180));
                    }
                    clone = Instantiate(AI[randomindex], randomposition, Quaternion.identity);

                    if (randomindex == 0)//aicar
                    {
                        clone.name = "AICar";
                    }
                    if (randomindex == 1)//aiSoldier
                    {
                        clone.name = "AISoldier";
                    }
                    if (randomindex == 2)//aizombie
                    {
                        clone.name = "AIZombie";
                    }

                    NumberOfEnemiesToSpawn+=1;
                    print("Only Add 1");
                    TIme = 0;
                }
            }
        }

        if(NumberOfEnemiesToSpawn >= SpawnWaveEnemies)
        {
            DuringWave = false;
        }
        if (AmountOfEnemies == 0 && !DuringWave)
        {
            WaveNumber++;
            IncreaseSpawn = true;
            DuringWave = true;
            NumberOfEnemiesToSpawn = 0;
        }
        if (IncreaseSpawn == true && WaveNumber < 10)
        {
            print("Increase");
            IncreaseSpawn = false;
            SpawnWaveEnemies += Random.Range(1,3);
        }
    }
}
