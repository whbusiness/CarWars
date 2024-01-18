using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCodeForPlayer : MonoBehaviour
{
    private Transform Cameraangle;
    private Transform player;
    public Vector3 offset;

    private void Start()
    {
        Cameraangle = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        Cameraangle.transform.position = player.position + offset;
    }
}
