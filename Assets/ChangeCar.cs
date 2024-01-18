using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCar : MonoBehaviour
{
    public GameObject greycar, bluecar, redcar;
    public void OnNext()
    {
        print("OnNext");
        if (greycar.activeInHierarchy)
        {
            print("Grey IN Scene");
            greycar.SetActive(false);
            bluecar.SetActive(true);
        }
        if (bluecar.activeInHierarchy)
        {
            bluecar.SetActive(false);
            redcar.SetActive(true);
        }
        if (redcar.activeInHierarchy)
        {
            redcar.SetActive(false);
            greycar.SetActive(true);
        }
    }
}
