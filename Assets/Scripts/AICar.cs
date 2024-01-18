using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AICar : MonoBehaviour
{
    public Transform player;
    //public Transform PatrolRoute;
    //public List<Transform> Locations;
    //private int _locationIndex = 0;
    private NavMeshAgent _agent;
    public int BulletForwardSpeed = 15;

    //public float lookradius = 10f;
    public Transform spawnPoint;
    //public float projectilespeed;
    public GameObject projectile;

    //public EnemyHealthBar AICarHealthBar;
    //public Slider HealthBar;

    bool CanShoot = true;
    bool DontShootOnTheMove = false;
    public int AICarHealth = 300;
    public int MaxAICarHealth = 300;
    public GameObject PlayerCharacter;
    public static bool SpawningOnObject = false; 
    //public Canvas HealthBarCanvas;
    /*public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public LayerMask whatIsGround;*/

    //public Transform Patrol1;
    //public Transform Patrol2;
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerCharacter = GameObject.Find("PlayerCharacter");
        AICarHealth = MaxAICarHealth;
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.gameObject.name == "ObjectsInScene")
        {
            SpawningOnObject = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.name == "ObjectsInScene")
        {
            SpawningOnObject = false;
        }
    }
    void Start()
    {
        //HealthBar = GameObject.Find("HealthBarCar").GetComponent<Slider>();
        //HealthBar.gameObject.SetActive(false);
       // HealthBar.maxValue = MaxAICarHealth;
        //AICarHealthBar.MaxHealth(MaxAICarHealth);
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.tag = "Enemy";
        if (PlayerCharacter != null)
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        else
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        // InitializePatrolRoute();
        // MoveToNextPatrolLocation();
    }
    private void Update()
    {
        /* if (GunShoot.damagecar)
         {
             AICarHealthBar.Health(AICarHealth);
             GunShoot.damagecar = false;
         }
        if (!GunShoot.HitAICar)
        {
            HealthBar.gameObject.SetActive(false);
        }
        if (HealthBar != null && HealthBarCanvas != null)
        {
            //HealthBar.value = AICarHealth;
            HealthBarCanvas.transform.LookAt(player);
        }*/
        transform.LookAt(player);
        float distance = Vector3.Distance(transform.position, player.transform.position);
        // print(distance);
        if (distance <= 10)
        {
            StopEnemy();
            ShootPlayer();
            DontShootOnTheMove = true;
            _agent.isStopped = true;

        }
        if (distance > 16)
        {
            FollowPlayer();
            DontShootOnTheMove = false;
            _agent.isStopped = false;
        }







    }
    void StopEnemy()
    {
        transform.LookAt(player);
        ShootPlayer();

    }

    private void FollowPlayer()
    {
        transform.LookAt(player);
        _agent.SetDestination(player.position);
    }

    void ShootPlayer()
    {
        if (CanShoot == true)
        {
            if (DontShootOnTheMove == true)
            {
                Rigidbody rb = Instantiate(projectile, spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * BulletForwardSpeed, ForceMode.Impulse);
                // newProjectileL.GetComponent<Rigidbody>().AddForce(projectilespeed * transform.forward);
                CanShoot = false;
                StartCoroutine(ReShoot());
            }
        }

    }
    IEnumerator ReShoot()
    {
        yield return new WaitForSeconds(1);
        CanShoot = true;
        ShootPlayer();
    }

    /*public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Out Of Range");
        }
    }*/
}
