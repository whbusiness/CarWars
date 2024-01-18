using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFollowCar : MonoBehaviour
{
    public Transform car;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(90, car.transform.eulerAngles.y, 0);
    }
}
