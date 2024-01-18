using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float Radius = 1;
    private float _TimeElapsed;
    public float Timer1 = 30;
    public float Timer2 = 60;
    public float Timer3 = 90;
    public GameObject[] prefabs;

    //private float timeduration = 2f * 30;


    void Update()
    {
        _TimeElapsed += Time.deltaTime;
        if(_TimeElapsed <= Timer1)
        {
            Invoke(nameof(SpawnObjectAtRandom), 8.0F);
        }
        if (_TimeElapsed > Timer1)
        {
            if (_TimeElapsed <= Timer2)
            {
                Invoke(nameof(SpawnObjectAtRandom), 6.0F);
            }
        }
        if (_TimeElapsed > Timer2)
        {
                Invoke(nameof(SpawnObjectAtRandom), 4.0F);            
        }

    }

    void SpawnObjectAtRandom()
    {
        int prefabsAmount = Random.Range(0, prefabs.Length);
        Vector3 RandomPos = Random.insideUnitCircle * Radius;
        RandomPos = new Vector3(RandomPos.x, 1, RandomPos.y);
        Instantiate(prefabs[prefabsAmount], RandomPos, Quaternion.identity);


        CancelInvoke();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, Radius);

    }
}
