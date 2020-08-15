using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Battle,
        Chase,
        Attack,
        Dead,
        AttackRanged
    }

    public FSMStates currentState;
    public float attackDistance = 2f;
    public float chaseDistance = 6f;
    public float detectionRange = 7f;
    public float rangedAttackDistance = 0f;
    public bool rangedAttack = false;
    public bool isBoss = false;
    public GameObject player;
    public AudioClip bossFightMusic;

    Animator anim;
    NavMeshAgent agent;
    float distanceToPlayer;
    float animDuration;
    EnemyHealth enemyhealth;
    int health;
    float originalSpeed;
    bool bossSpawned = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Doctor");
        originalSpeed = agent.speed;
        Initialize();
    }

    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            health = enemyhealth.currentHealth;

            if (health <= 0)
            {
                currentState = FSMStates.Dead;
            }

            switch (currentState)
            {
                case FSMStates.Idle:
                    UpdateIdleState();
                    break;
                case FSMStates.Battle:
                    UpdateBattleState();
                    break;
                case FSMStates.Chase:
                    UpdateChaseState();
                    break;
                case FSMStates.Attack:
                    UpdateAttackState();
                    break;
                case FSMStates.Dead:
                    UpdateDeadState();
                    break;
                case FSMStates.AttackRanged:
                    UpdateRangedAttack();
                    break;
            }
        }

        if (gameObject.activeInHierarchy && isBoss && !bossSpawned)
        {
            bossSpawned = true;
            Camera.main.transform.GetComponent<AudioSource>().clip = bossFightMusic;
            Camera.main.transform.GetComponent<AudioSource>().Play();
        }
    }

    void Initialize()
    {
        currentState = FSMStates.Idle;
        enemyhealth = GetComponent<EnemyHealth>();
        health = enemyhealth.currentHealth;

    }

    void UpdateIdleState()
    {
        anim.SetInteger("animState", 0);

        if (distanceToPlayer <= detectionRange)
        {
            currentState = FSMStates.Battle;
        }
    }

    void UpdateBattleState()
    {
        anim.SetInteger("animState", 1);

        if (rangedAttack && distanceToPlayer <= rangedAttackDistance)
        {
            currentState = FSMStates.AttackRanged;
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > detectionRange)
        {
            currentState = FSMStates.Idle;
        }

        FaceTarget(player.transform.position);

        if (!rangedAttack)
        {
            agent.speed = originalSpeed * 0.1f;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }

        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed * 0.1f * Time.deltaTime);
    }

    void UpdateChaseState()
    {
        anim.SetInteger("animState", 2);

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance && distanceToPlayer <= detectionRange)
        {
            currentState = FSMStates.Battle;
        }
        else if (distanceToPlayer > detectionRange)
        {
            currentState = FSMStates.Idle;
        }

        FaceTarget(player.transform.position);

        agent.speed = originalSpeed;
        agent.SetDestination(player.transform.position);

        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
    }

    void UpdateAttackState()
    {
        anim.SetInteger("animState", 3);

        if (distanceToPlayer > attackDistance)
        {
            if (distanceToPlayer <= chaseDistance)
            {
                currentState = FSMStates.Chase;
            }
            else if (rangedAttack && distanceToPlayer <= rangedAttackDistance)
            {
                currentState = FSMStates.AttackRanged;
            }
        }
        else if (distanceToPlayer > chaseDistance && distanceToPlayer <= detectionRange)
        {
            currentState = FSMStates.Battle;
        }
        else if (distanceToPlayer > detectionRange)
        {
            currentState = FSMStates.Idle;
        }

        FaceTarget(player.transform.position);

    }

    void UpdateDeadState()
    {
        anim.SetInteger("animState", 4);

        animDuration = anim.GetCurrentAnimatorStateInfo(0).length;

        // Optional: Call a script to drop a loot at certain chance.
        Destroy(gameObject, animDuration);

    }

    void UpdateRangedAttack()
    {
        anim.SetInteger("animState", 5);

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance && distanceToPlayer <= detectionRange)
        {
            currentState = FSMStates.Battle;
        }
        else if (distanceToPlayer > detectionRange)
        {
            currentState = FSMStates.Idle;
        }

        FaceTarget(player.transform.position);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = -0.31f;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
    }

    private void OnDrawGizmos()
    {
        // attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        // chase
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        // detection
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
