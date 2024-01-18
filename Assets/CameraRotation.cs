using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public static Vector3 look;
    float Xrot;
    float Yrot;
    public static float sensX;
    public static float sensY;
    public new Transform camera;
    [SerializeField] float xClamp = 85f;
    private float LookingX, LookingY;
    public static float Xsens = 100;
    public static float Ysens = 40;
    MasterInput controls;
    public Transform weapon;
    public static Transform CameraTransfom;
    public float smooth;
    //public Vector3 offset;
    private void Awake()
    {
        controls = new MasterInput();

        controls.ShootingMap.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
        controls.ShootingMap.Look.canceled += ctx => look = Vector2.zero;
    }
    private void OnEnable()
    {
        controls.ShootingMap.Enable();
    }
    private void OnDisable()
    {
        controls.ShootingMap.Disable();
    }
    private void Start()
    {
        CameraTransfom = transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        /*LookingX = look.x * Time.deltaTime * sensX;
        LookingY = look.y * Time.deltaTime * sensY;
        Xrot -= LookingY;
        Yrot += LookingX;
        Xrot = Mathf.Clamp(Xrot, -xClamp, xClamp);
        camera.localRotation = Quaternion.Euler(Xrot, Yrot, 0);*/

        float LookingX = look.x * Time.deltaTime * sensX;
        float LookingY = look.y * Time.deltaTime * sensY;
        Yrot += LookingX;
        Xrot -= LookingY;
        transform.Rotate(Vector3.up, LookingX);
        Xrot = Mathf.Clamp(Xrot, -xClamp, xClamp);
        Vector3 YRotation = transform.eulerAngles;
        YRotation.x = Xrot;
        camera.eulerAngles = YRotation;

    }
}
