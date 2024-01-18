using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform player;
    //public Transform CarS;
    //public Transform PatrolRoute;
    //public List<Transform> Locations;
    //private int _locationIndex = 0;
    private NavMeshAgent _agent;

   // public float lookradius = 10f;
    public Transform spawnPoint;
    public float projectilespeed = 12;
    public GameObject projectile;

    bool CanShoot = true;
    bool DontShootOnTheMove = false;
    public static int CarAIhealth = 300;
    public int maxHealth = 300;
    /*public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public LayerMask whatIsGround;*/

    //public Transform Patrol1;
    //public Transform Patrol2;

    // Start is called before the first frame update

    private void Awake()
    {
        CarAIhealth = maxHealth;
    }
    void Start()
    {
        //CarS.tag = "Enemy";
        gameObject.tag = "Enemy";
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Car").transform;
        // InitializePatrolRoute();
        // MoveToNextPatrolLocation();
    }

    private void Update()
    {
        //Debug.Log(EnemyHealth);
        /*if (_agent.remainingDistance < 0.5f && !_agent.pathPending)
        {
            //MoveToNextPatrolLocation();
        }*/
        if (GameObject.Find("AICar") != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= 20)
            {
                StopEnemy();
                ShootPlayer();
                DontShootOnTheMove = true;
                _agent.isStopped = true;

            }
            if (distance > 20)
            {
                FollowPlayer();
                ShootPlayer();
                DontShootOnTheMove = false;
                _agent.isStopped = false;
            }
        }
    }
    void StopEnemy()
    {
        transform.LookAt(player);
        _agent.isStopped = true;
        ShootPlayer();

    }

    private void FollowPlayer()
    {
        transform.LookAt(player);
        _agent.SetDestination(player.position);
        
    }

    void ShootPlayer()
    {
        if(CanShoot == true)
        {
            if (DontShootOnTheMove == true)
            {
                Rigidbody rb = Instantiate(projectile, spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 14f, ForceMode.Impulse);
                // rb.AddForce(transform.up * 17f, ForceMode.Impulse);
                //GameObject newProjectileL = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
                //newProjectileL.GetComponent<Rigidbody>().AddForce(projectilespeed * transform.forward);
                //Debug.Log("Shoot"); 
                // newProjectileL.GetComponent<Rigidbody>().AddForce(projectilespeed * transform.forward);
                CanShoot = false;
                StartCoroutine(ReShoot());
            }
        }
        
    }
    IEnumerator ReShoot()
    {
        yield return new WaitForSecondsRealtime(1f);
        CanShoot = true;
        ShootPlayer();
    }
    


    /*private void InitializePatrolRoute()
    {
        foreach (Transform child in PatrolRoute)
        {
            Locations.Add(child);
        }
    }*/
    
    /*private void MoveToNextPatrolLocation()
    {
        DontShootOnTheMove = false;

        /*_agent.SetDestination(Patrol1.position);
        transform.LookAt(Patrol1);

        if(_agent.transform.position == Patrol1.position)
        {
            print("Move to next location");
            _agent.SetDestination(Patrol2.position);
        }
        if(_agent.transform.position == Patrol2.position)
        {
            _agent.SetDestination(Patrol1.position);
        }*/



        /*if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            _agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        
         if (Locations.Count == 0)
             return;
         _agent.destination = Locations[_locationIndex].position;

         _locationIndex = (_locationIndex + 1) % Locations.Count;
         
    }*/

    /*private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }*/






   /* public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Detected - Attack");
            _agent.SetDestination(player.position);
        }
    }*/

    /*public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Out Of Range");
        }
    }*/
}
