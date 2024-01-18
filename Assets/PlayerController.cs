using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private float MoveHorizontally;
    private float MoveVertically;
    private float CurrentsteeringAngle;
    public static float health = 100;
    private float _TimeElapsed;
    public float _TimePassed = 0.4f;
    //private float TimerForStart;
    public float TimerEndedForStart = 3f;
    bool canBrake = false;
    public Transform RocketSpawn;
    public GameObject DisableMainCamInScene;
    public Rigidbody _rb;
    public float speed;
    public float regenTime;

    public Transform spawnPoint;
    public float projectilespeed;
    public GameObject projectile;
    // private bool braking = false;
    public int AmountOfBullets = 10;



    //private Vector3 _direction;
    public bool shooting = false;
    public float timer;
    public bool waitfornextshot = false;
    //public static bool DelayOnStart = true;
    MasterInput controls;
    Vector3 move;
    public int BackwardsForce, BackwardsForceWhilstMoving;
    private Vector3 movementDirection;
    public new Transform camera;
    public AudioSource WalkingSound;
    public float maxHealth;
    public Image BloodOverlay;
    public Color alphaBloodOverlay;
    private float timeSinceLastHit;
    private float timeSinceLastHitZombie;
    private void Awake()
    {
        health = 100;
        maxHealth = health;
        controls = new MasterInput();
        BloodOverlay = GameObject.Find("BloodOverlay").GetComponent<Image>();
        controls.ShootingMap.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.ShootingMap.Move.canceled += ctx => move = Vector2.zero;
        DisableMainCamInScene = GameObject.Find("Camera1");
        DisableMainCamInScene.SetActive(false);
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
        alphaBloodOverlay.a = 0;
        health = maxHealth;
        //StartCoroutine(WaitForStart());
        _rb = GetComponent<Rigidbody>();
        ScoreUI.score = 0;
        //DelayOnStart = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            print("Colliding WIth Wall");
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
        timeSinceLastHit = Time.time - EnemyBulletDetection.timeOfHit;
        timeSinceLastHitZombie = Time.time - ZombieAi.timeOfHit;
        if(timeSinceLastHit > 8 && timeSinceLastHitZombie > 8)
        {
            RegenHealth();
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if(health < 1)
        {
            SceneManager.LoadScene("EndScreen");
        }
        /*if (_rb.velocity.magnitude > 0.1f)
        {
            WalkingSound.Play();
        }*/
    }
    public void DMGOverlay()
    {
        if (health > 0)
        {
            health -= 10;
            alphaBloodOverlay.a += .1f;
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
            health += 0.1f;
            alphaBloodOverlay.a -= .001f;
            BloodOverlay.color = alphaBloodOverlay;
        }        
        if(alphaBloodOverlay.a == 0)
        {
            health = maxHealth;
        }
        if(alphaBloodOverlay.a < 0)
        {
            alphaBloodOverlay.a = 0;
        }
    }
    /*IEnumerator WaitForStart()
    {

        Time.timeScale = 0f;
        shooting = false;
        yield return new WaitForSeconds(5f);
        Time.timeScale = 1f;
        DelayOnStart = false;

    }*/
    private void FixedUpdate()
    {
        /* if (!DelayOnStart)
         {
             ApplyMovement();
         }*/
        ApplyMovement();
        //Debug.Log(health);
    }
    private void ApplyMovement()
    {
        /*Vector3 ForwardMovement = Camera.main.transform.forward;
        Vector3 RightMovement = Camera.main.transform.right;
        ForwardMovement.y = 0;
        RightMovement.y = 0;
        Vector3 ForwardMovementY = move.y * ForwardMovement;
        Vector3 ForwardMovementX = move.x * RightMovement;
        Vector3 MovementBasedOnCamera = ForwardMovementY + ForwardMovementX;*/
        //Vector3 movement = (move.x, 0, move.y);
        movementDirection = new(move.x, 0, move.y);
        movementDirection = camera.transform.TransformDirection(movementDirection);
        movementDirection.y = 0;
        //controller.Move(speed * Time.fixedDeltaTime * movementDirection.normalized);
        _rb.velocity = speed * Time.fixedDeltaTime * movementDirection.normalized;
    }

}
