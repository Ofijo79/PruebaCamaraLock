using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    enum State
    {
        Chasing,
        Attacking
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
        _animator.SetBool("Run", false);
        _animator.SetInteger("attack", 1);

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
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);
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
        _animator.SetBool("Run", false);
        _animator.SetInteger("attack", 2);

        StartCoroutine(WaitForSecondAttackAnimation());
    }

    IEnumerator WaitForSecondAttackAnimation()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);
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
            return true;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Vector3 fovLine1 = Quaternion.AngleAxis(visionAngle * 0.5f, transform.up) * transform.forward * visionRange;
        Vector3 fovLine2 = Quaternion.AngleAxis(-visionAngle * 0.2f, transform.up) * transform.forward * visionRange;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);
    }
}
