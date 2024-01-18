using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CarController : MonoBehaviour
{
    private float MoveHorizontally;
    private float MoveVertically;
    private float CurrentsteeringAngle;
    public static float health = 100, maxHealth;
    private float _TimeElapsed;
    public float _TimePassed = 0.4f;
    //private float TimerForStart;
    public float TimerEndedForStart = 3f;
    public Transform RocketSpawn;


    public Rigidbody _rb;
    [SerializeField] private float Speed;
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private WheelCollider flwCollider;
    [SerializeField] private WheelCollider frwCollider;
    [SerializeField] private WheelCollider rlwCollider;
    [SerializeField] private WheelCollider rrwCollider;

    [SerializeField] private Transform flwTransform;
    [SerializeField] private Transform frwTransform;
    [SerializeField] private Transform rlwTransform;
    [SerializeField] private Transform rrwTransform;

    public Transform spawnPointL; public Transform spawnPointR;
    public float projectilespeed;
    public GameObject projectile;
    public GameObject Rocket;
    public bool CanShootRocket = true;
   // private bool braking = false;
    private readonly float BrakeAmount = 20000f;
    private float timeSinceLastHitZombie;
    public int AmountOfBullets;

    private bool ShootSide;
    public GameObject EnableMainCamInScene;

    //private Vector3 _direction;
    public bool shooting = false;
    public bool shooting2 = false;
    public float timer;
    public bool waitfornextshot = false;
    MasterInput controls;
    Vector3 move;
    public int BackwardsForce, BackwardsForceWhilstMoving;
    public Image BloodOverlay;
    public Color alphaBloodOverlay;
    private float timeSinceLastHit;
    public GameObject CrosshairGO;
    public GameObject HitMarker;
    private float FireBullet, FireRocket, CanBrake;
    [SerializeField] InputActionReference _inputActionReference, BrakeRef, RocketRef;
    private GameObject RebindMenuUI;
    public Toggle InvertToggle;
    private void Awake()
    {
        health = 200;
        CrosshairGO = GameObject.FindWithTag("Crosshair");
        BloodOverlay = GameObject.Find("BloodOverlay").GetComponent<Image>();
        controls = new MasterInput();
        _inputActionReference.action.performed += ctx => FireBullet = ctx.ReadValue<float>();
        _inputActionReference.action.canceled += ctx => FireBullet = 0;
        BrakeRef.action.performed += ctx => CanBrake = ctx.ReadValue<float>();
        BrakeRef.action.canceled += ctx => CanBrake = 0;
        RocketRef.action.performed += ctx => FireRocket = ctx.ReadValue<float>();
        RocketRef.action.canceled += ctx => FireRocket = 0;
        controls.ShootingMap.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.ShootingMap.Move.canceled += ctx => move = Vector2.zero;
        EnableMainCamInScene = GameObject.Find("Camera1");
        EnableMainCamInScene.SetActive(true);
        maxHealth = health;
        InvertToggle = GameObject.Find("Invert").GetComponent<Toggle>();
        InvertToggle.isOn = false;
        RebindMenuUI = GameObject.Find("ButtonRebindMenu");
        RebindMenuUI.SetActive(false);
    }
    private void OnEnable()
    {
        RocketRef.action.Enable();
        BrakeRef.action.Enable();
        _inputActionReference.action.Enable();
        controls.ShootingMap.Enable();
    }
    private void OnDisable()
    {
        RocketRef.action.Disable();
        BrakeRef.action.Disable();
        _inputActionReference.action.Enable();
        controls.ShootingMap.Disable();
    }
    private void Start()
    {
        AmountOfBullets = 100;
        alphaBloodOverlay.a = 0;
        _rb = GetComponent<Rigidbody>();
        ScoreUI.score = 0;
        CrosshairGO.SetActive(false);
        HitMarker = GameObject.Find("HitMarker");
        HitMarker.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Gamepad.current.SetMotorSpeeds(1f, 3f);
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Gamepad.current.ResetHaptics();
        }
    }
    private void Update()
    {
        GetInput();
        timeSinceLastHit = Time.time - EnemyBulletDetection.timeOfHit;
        timeSinceLastHitZombie = Time.time - ZombieAi.timeOfHit;
        if (alphaBloodOverlay.a == 0)
        {
            health = maxHealth;
        }
        if (timeSinceLastHit > 8 && timeSinceLastHitZombie > 8)
        {
            RegenHealth();
        }
        if (health < 1)
        {
            SceneManager.LoadScene("EndScreen");
        }
        if (FireBullet > 0)
        {
             shooting = true;
        }
        else
        {
             shooting = false;
             waitfornextshot = false;
        }
        
        
        if (shooting)
        {
            if (AmountOfBullets != 0)
            {
                if (ShootSide && !waitfornextshot)
                {
                    GameObject newProjectileL = Instantiate(projectile, spawnPointL.position, Quaternion.identity);
                    newProjectileL.GetComponent<Rigidbody>().AddForce(projectilespeed * transform.forward);
                    ShootSide = false;
                    waitfornextshot = true;
                    AmountOfBullets -= 1;                    
                   // shooting = true;
                }
                else if(!ShootSide && !waitfornextshot)
                {
                    
                    GameObject newProjectileR = Instantiate(projectile, spawnPointR.position, Quaternion.identity);
                    newProjectileR.GetComponent<Rigidbody>().AddForce(projectilespeed * transform.forward);
                    ShootSide = true;
                    waitfornextshot = true;
                    AmountOfBullets -= 1;
                    //shooting = true;
                }
            }
        }
        if (waitfornextshot)
        {
            Invoke(nameof(DelayNextBullet), 0.15F);
            /*_TimeElapsed += Time.deltaTime;
            if(_TimeElapsed > 0.2f)
            {
                waitfornextshot = false;
            }*/
        }
        if (FireRocket > 0)
        {
            if (CanShootRocket)
            {
                shooting2 = true;
                if (shooting2)
                {
                    if (_rb.velocity.magnitude < 0.3f)
                    {
                        _rb.AddForce(transform.forward * -BackwardsForce, ForceMode.Acceleration);
                    }
                    if (_rb.velocity.magnitude > 0.3f)
                    {
                        _rb.AddForce(transform.forward * -BackwardsForceWhilstMoving, ForceMode.Acceleration);
                    }
                    GameObject newProjectileL = Instantiate(Rocket, RocketSpawn.position, Quaternion.identity);
                    newProjectileL.GetComponent<Rigidbody>().AddForce(projectilespeed * transform.forward);
                    shooting2 = false;
                    CanShootRocket = false;
                    StartCoroutine(nameof(DelayRocket));
                }
            }
        }




        //print(canBrake);
        if (CanBrake > 0)
        {
            flwCollider.brakeTorque = BrakeAmount;
            frwCollider.brakeTorque = BrakeAmount;
            flwCollider.motorTorque = 0f;
            frwCollider.motorTorque = 0f;
        }
        else
        {
            flwCollider.brakeTorque = 0f;
            frwCollider.brakeTorque = 0f;
            GetSpeed();
        }
    }
    public void DMGOverlay()
    {
        if (health > 0)
        {
            health -= 10;
            alphaBloodOverlay.a += .05f;
            BloodOverlay.color = alphaBloodOverlay;
            StartCoroutine(Rumble());
        }
    }
    IEnumerator Rumble()
    {
        Gamepad.current.SetMotorSpeeds(1, 3);
        yield return new WaitForSeconds(.25f);
        InputSystem.ResetHaptics();
    }
    public void RegenHealth()
    {
        if (health < maxHealth)
        {
            health += 0.5f;
            if (alphaBloodOverlay.a > 0)
            {
                alphaBloodOverlay.a -= .003f;
            }
            if(alphaBloodOverlay.a < 0)
            {
                alphaBloodOverlay.a = 0;
            }
            BloodOverlay.color = alphaBloodOverlay;
        }
    }
    void DelayNextBullet()
    {
        waitfornextshot = false;
        CancelInvoke();
    }
    
    private void FixedUpdate()
    {
       
        GetSpeed();
        Steer();
        Wheels();
        
        //Debug.Log(health);
    }
    IEnumerator DelayRocket()
    {
        yield return new WaitForSeconds(5f);
        CanShootRocket = true;
    }
    private void GetInput()
    {

        MoveHorizontally = move.x;
        MoveVertically = move.y;
        
    }
   

    
    private void GetSpeed()
    {
        flwCollider.motorTorque = MoveVertically * Speed * Time.deltaTime;
        frwCollider.motorTorque = MoveVertically * Speed * Time.deltaTime;      

    }
   

    private void Steer()
    {
        if (!InvertToggle.isOn)
        {
            CurrentsteeringAngle = maxSteeringAngle * MoveHorizontally;
            flwCollider.steerAngle = CurrentsteeringAngle;
            frwCollider.steerAngle = CurrentsteeringAngle;
        }
        else
        {
            CurrentsteeringAngle = maxSteeringAngle * -MoveHorizontally;
            flwCollider.steerAngle = CurrentsteeringAngle;
            frwCollider.steerAngle = CurrentsteeringAngle;
        }
    }

    private void Wheels()
    {
        UpdateWheelIndividually(flwCollider, flwTransform);
        UpdateWheelIndividually(frwCollider, frwTransform);
        UpdateWheelIndividually(rlwCollider, rlwTransform);
        UpdateWheelIndividually(rrwCollider, rrwTransform);
    }

    private void UpdateWheelIndividually(WheelCollider wheelcollider, Transform wheeltransform)
    {
        wheelcollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        wheeltransform.SetPositionAndRotation(position, rotation);
    }
    
    /*public void SetRumble()
    {
        _TimeElapsed = 0f;
        print(_TimeElapsed);
        _TimeElapsed += Time.deltaTime;
        if(_TimeElapsed < _TimePassed)
        {
            print("Set Motor Speeds");
            Gamepad.current.SetMotorSpeeds(0.25f, 0.75f);
        }
        else
        {
            // Gamepad.current.ResetHaptics();
            print("Reset Motor Speeds");
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }*/

    /*public void SetRumble()
    {
        TimerForRumble += Time.deltaTime;
        if (TimerForRumble > TimerPassedForRumble)
        {
            print("Timer Running");
            Gamepad.current.SetMotorSpeeds(0.25f, 0.75f);
        }
        else
        {
            print("Timer Ran Out");
            TimerForRumble = 0f;
            Gamepad.current.ResetHaptics();
        }
        
    }*/



        /*public void OnBrake()
        {
            flwCollider.brakeTorque = BrakeAmount;
            frwCollider.brakeTorque = BrakeAmount;
            flwCollider.motorTorque = 0f;
            frwCollider.motorTorque = 0f; print("Please stgop braking");
            flwCollider.brakeTorque = 0f;
            frwCollider.brakeTorque = 0f;
            flwCollider.motorTorque = MoveVertically * Speed * Time.deltaTime;
            frwCollider.motorTorque = MoveVertically * Speed * Time.deltaTime;
            _rb.Sleep();
        }*/



   /* public void OnShoot()
    {
       if (AmountOfBullets != 0)
        {
            if (ShootSide)
            {
                GameObject newProjectileL = Instantiate(projectile, spawnPointL.position, Quaternion.identity);
                newProjectileL.GetComponent<Rigidbody>().AddForce(projectilespeed * transform.forward);
                ShootSide = false;
                AmountOfBullets -= 1;
            }
            else
            {
                GameObject newProjectileR = Instantiate(projectile, spawnPointR.position, Quaternion.identity);
                newProjectileR.GetComponent<Rigidbody>().AddForce(projectilespeed * transform.forward);
                ShootSide = true;
                AmountOfBullets -= 1;
            }
        }

    }*/
    
}
