using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieAi : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent _agent;
    private Animator _animator;
    public bool canAttack = true;
    public int ZombieHealth = 600;
    public int MaxZombieHealth = 600;
    private GameObject playerCharacter;
    PlayerController FindHealthOverlay;
    CarController FindHealthOverlayCar;
    public static float timeOfHit;
    public static bool SpawningOnObject = false;
    // Start is called before the first frame update
    private void Awake()
    {
        ZombieHealth = MaxZombieHealth;
    }
    void Start()
    {
        //HealthBar = GameObject.Find("HealthBarZombie").GetComponent<Slider>();
        //HealthBar.maxValue = MaxZombieHealth;
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponent<Animator>();
        gameObject.tag = "Enemy";
        playerCharacter = GameObject.Find("PlayerCharacter");
        FindHealthOverlay = FindObjectOfType<PlayerController>();
        FindHealthOverlayCar = FindObjectOfType<CarController>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.name == "ObjectsInScene")
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
    // Update is called once per frame
    void Update()
    {
        //print(_animator.GetBool("isAttacking"));
        transform.LookAt(player);
        float distance = Vector3.Distance(transform.position, player.transform.position);
       // print(distance);
        if(distance <= 2)
        {
            _agent.isStopped = true;
            _animator.SetBool("isAttacking", true);
            _animator.SetBool("isWalking", false);
            AttackPlayer();
        }
        if (distance > 2)
        {
            FollowPlayer();
            _animator.SetBool("isWalking", true);
            _animator.SetBool("isAttacking", false);
            _agent.isStopped = false;
        }
    }
    void AttackPlayer()
    {
        if (canAttack && _animator.GetBool("isAttacking"))
        {
            if (!_animator.GetBool("isWalking"))
            {
                if (playerCharacter != null)
                {
                    timeOfHit = Time.time;
                    FindHealthOverlay.DMGOverlay();
                }
                else if (playerCharacter == null)
                {
                    timeOfHit = Time.time;
                    FindHealthOverlayCar.DMGOverlay();
                }
                canAttack = false;
                StartCoroutine(ResetAttack());
            }
        }
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1);
        canAttack = true;
        //AttackPlayer();
    }
    void FollowPlayer()
    {
        _agent.SetDestination(player.position);
        _animator.SetBool("isAttacking", false);
    }
}
