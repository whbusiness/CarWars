using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBulletPowerup : MonoBehaviour
{
    public float RotateX;
    public float RotateY;
    public float RotateZ;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(RotateX, RotateY, RotateZ, Space.World);
    }
}
