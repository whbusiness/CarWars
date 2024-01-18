using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGunWIthCamera : MonoBehaviour
{
   // public float intensity;
    MasterInput controls;
    Vector3 look2;
    public float smooth, swayMultiplier;
    public Transform pistol;
    private void Start()
    {
       // originalRot = transform.localRotation;
        //newWeaponRotation = transform.localRotation.eulerAngles;
        //initialPos = transform.localPosition;
    }
    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        controls = new MasterInput();
        controls.ShootingMap.Look2.performed += ctx => look2 = ctx.ReadValue<Vector2>();
        controls.ShootingMap.Look2.canceled += ctx => look2 = Vector2.zero;
    }

        // Update is called once per frame
        void Update()
        {
        
        float lookX = look2.x * swayMultiplier;
        float lookY = look2.y * swayMultiplier;
        Quaternion rotX = Quaternion.AngleAxis(-lookX, Vector3.right);
        Quaternion rotY = Quaternion.AngleAxis(lookY, Vector3.up);
        Quaternion targetRot = rotX * rotY;
        pistol.localRotation = Quaternion.Slerp(pistol.localRotation, targetRot, Time.deltaTime * smooth);
    }
}
