using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    enum State
    {
        Patrolling,
        Chasing,
        Searching,
        Waiting,
        Attacking,
        Combo
    }

    State currentState;

    UnityEngine.AI.NavMeshAgent enemyAgent;

    public Transform playerTransform;

    [SerializeField] Transform patrolAreaCenter;
    [SerializeField] Vector2 patrolAreaSize;
    public Transform[] points;

    [SerializeField] private int destPoint = 0;
    [SerializeField] float visionRange = 15;
    [SerializeField] float visionAngle = 50;
    [SerializeField] float attackRange = 1;

    Vector3 lastTargetPosition;

    [SerializeField] float searchTimer;
    [SerializeField] float searchWaitTime = 15;
    [SerializeField] float searchRadius = 30;

    private bool repeat = true;

    Animator _animator;

    void Awake()
    {
        enemyAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        enemyAgent.autoBraking = false;
        currentState = State.Patrolling;
        enemyAgent.destination = points[destPoint].position;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Chasing:
                Chase();
                break;
            case State.Searching:
                Search();
                break;
            case State.Waiting:
                Waiting();
                break;
            case State.Attacking:
                FirstAttack();
                break;
        }
    }

    void Patrol()
    {
        _animator.SetBool("TenguStop", false);
        _animator.SetBool("TenguPatrolling", true);
        if (enemyAgent.remainingDistance < 0.5f)
        {
            currentState = State.Waiting;
        }

        if (OnRange())
        {
            currentState = State.Chasing;
        }
    }

    void Chase()
    {
        _animator.SetBool("TenguStop", false);
        _animator.SetBool("TenguPatrolling", true);
        _animator.SetInteger("enemyAttack", 0);
        enemyAgent.destination = playerTransform.position;

        if (!OnRange())
        {
            currentState = State.Searching;
        }

        if (OnRangeAttack())
        {
            currentState = State.Attacking;
        }
    }

    void Search()
    {
        _animator.SetBool("TenguStop", false);
        _animator.SetBool("TenguPatrolling", true);
        if (OnRange())
        {
            searchTimer = 0;
            currentState = State.Chasing;
        }

        searchTimer += Time.deltaTime;

        if (searchTimer < searchWaitTime)
        {
            if (enemyAgent.remainingDistance < 0.5f)
            {
                Vector3 randomSearchPoint = lastTargetPosition + Random.insideUnitSphere * searchRadius;
                randomSearchPoint.y = lastTargetPosition.y;
                enemyAgent.destination = randomSearchPoint;
            }
        }
        else
        {
            currentState = State.Patrolling;
        }
    }

    void Waiting()
    {
        if (repeat)
        {
            StartCoroutine(Esperar());
        }
    }

    IEnumerator Esperar()
    {
        _animator.SetBool("TenguPatrolling", false);
        _animator.SetBool("TenguStop", true);
        repeat = false;
        yield return new WaitForSeconds(1f);
        GotoNextPoint();
        currentState = State.Patrolling;
        repeat = true;
    }

    void FirstAttack()
    {
        DetenerMovimiento();
        RotateTowardsPlayer();
        _animator.SetBool("TenguStop", false);
        _animator.SetBool("TenguPatrolling", false);
        _animator.SetInteger("enemyAttack", 1);

        currentState = State.Combo;

        StartCoroutine(WaitForAttackAnimation());
    }

    void SecondAttack()
    {
        DetenerMovimiento();
        RotateTowardsPlayer();
        _animator.SetBool("TenguStop", false);
        _animator.SetBool("TenguPatrolling", false);
        _animator.SetInteger("enemyAttack", 2);

        StartCoroutine(WaitForSecondAttackAnimation());
    }

    void ThirdAttack()
    {
        DetenerMovimiento();
        RotateTowardsPlayer();
        _animator.SetBool("TenguStop", false);
        _animator.SetBool("TenguPatrolling", false);
        _animator.SetInteger("enemyAttack", 3);

        StartCoroutine(WaitForThirdAttackAnimation());
    }

    IEnumerator WaitForAttackAnimation()
    {
        yield return new WaitForSeconds(1f);
        ReanudarMovimiento();

        if (OnRangeAttack())
        {
            SecondAttack();
        }
        else
        {
            currentState = State.Chasing;
        }
    }

    IEnumerator WaitForSecondAttackAnimation()
    {
        yield return new WaitForSeconds(1f);
        ReanudarMovimiento();

        if (OnRangeAttack())
        {
            ThirdAttack();
        }
        else
        {
            currentState = State.Chasing;
        }
    }

    IEnumerator WaitForThirdAttackAnimation()
    {
        yield return new WaitForSeconds(2f);
        ReanudarMovimiento();

        if (OnRangeAttack())
        {
            FirstAttack();
        }
        else
        {
            currentState = State.Chasing;
        }
    }

    void DetenerMovimiento()
    {
        enemyAgent.isStopped = true;
    }

    void ReanudarMovimiento()
    {
        enemyAgent.isStopped = false;
    }

    bool OnRangeAttack()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= visionAngle / 2)
            {
                return true;
            }
        }

        return false;
    }

    bool OnRange()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= visionRange)
        {
            return true;
        }

        return false;
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        destPoint = (destPoint + 1) % points.Length;
        enemyAgent.destination = points[destPoint].position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(patrolAreaCenter.position, new Vector3(patrolAreaSize.x, 0, patrolAreaSize.y));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Vector3 fovLine1 = Quaternion.AngleAxis(visionAngle * 0.5f, transform.up) * transform.forward * visionRange;
        Vector3 fovLine2 = Quaternion.AngleAxis(-visionAngle * 0.5f, transform.up) * transform.forward * visionRange;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);
    }
}
