using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*[SerializeField] private Vector3 offset;
    [SerializeField] private Transform Aim;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float Turnspeed;

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

  
    private void HandleTranslation()
    {
        var targetposition = Aim.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetposition, MovementSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var direction = Aim.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Turnspeed * Time.deltaTime);
    }*/
    public Transform player;
    public new Transform camera;
    public float changex;
    public float changey;
    public float changez;
    private void Update()
    {
        camera.position = player.position + new Vector3(changex, changey, changez);
    }

}
