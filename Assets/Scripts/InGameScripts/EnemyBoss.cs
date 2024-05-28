using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    enum State
    {
        Chasing,
        Attacking,
        Combo
    }

    State currentState;

    UnityEngine.AI.NavMeshAgent enemyAgent;

    public Transform playerTransform;

    [SerializeField] float visionRange = 15;
    [SerializeField] float visionAngle = 50;
    [SerializeField] float attackRange = 1;

    Animator _animator;

    void Awake()
    {
        enemyAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentState = State.Chasing;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Chasing:
                Chase();
                break;

            case State.Attacking:
                Attacking();
                break;
        }
    }

    void Chase()
    {
        _animator.SetBool("Stop", false);
        _animator.SetBool("Run", true);
        _animator.SetInteger("attack", 0);
        enemyAgent.destination = playerTransform.position;

        if (OnRangeAttack())
        {
            currentState = State.Attacking;
        }
    }

    void Attacking()
    {
        DetenerMovimiento();
        RotateTowardsPlayer();
        _animator.SetBool("Run", false);
        _animator.SetInteger("attack", 1);
        currentState = State.Combo;

        StartCoroutine(WaitForAttackAnimation());
    }

    void DetenerMovimiento()
    {
        enemyAgent.isStopped = true;
    }

    void ReanudarMovimiento()
    {
        enemyAgent.isStopped = false;
    }

    IEnumerator WaitForAttackAnimation()
    {
        yield return new WaitForSeconds(0.75f);
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

    void SecondAttack()
    {
        DetenerMovimiento();
        RotateTowardsPlayer();
        _animator.SetBool("Run", false);
        _animator.SetInteger("attack", 2);
        currentState = State.Combo;

        StartCoroutine(WaitForSecondAttackAnimation());
    }

    IEnumerator WaitForSecondAttackAnimation()
    {
        yield return new WaitForSeconds(0.75f);
        ReanudarMovimiento();

        if (OnRangeAttack())
        {
            Attacking();
        }
        else
        {
            currentState = State.Chasing;
        }
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

    void OnDrawGizmos()
    {
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
