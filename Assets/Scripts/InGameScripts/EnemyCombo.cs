using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombo : MonoBehaviour
{
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] float timeBetweenAttacks = 1.5f;
    [SerializeField]int numberOfAttacks = 3;
    public GameObject player;
    public Animator _animator;
    private bool isComboing = false;
    private int currentAttackIndex = 0;
    private float lastAttackTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
        {
            if (!isComboing)
            {
                StartCombo();
            }
            else
            {
                if (Time.time - lastAttackTime >= timeBetweenAttacks)
                {
                    PerformNextAttack();
                }
            }
        }
        else
        {
            if (isComboing)
            {
                FinishCombo();
            }
        }
    }

    public void StartCombo()
    {
        isComboing = true;
        currentAttackIndex = 0;
        _animator.SetBool("FirstAttack", true);
        PerformNextAttack();
    }

    public void PerformNextAttack()
    {
        // Realiza el siguiente ataque en la secuencia
        _animator.SetBool("SecondAttack", true);
        _animator.SetBool("FIrstAttack", false);
        currentAttackIndex++;
        lastAttackTime = Time.time;

        // Verifica si se ha completado el combo
        if (currentAttackIndex >= numberOfAttacks)
        {
            FinishCombo();
        }
    }

    public void FinishCombo()
    {
        isComboing = false;
        currentAttackIndex = 0;
        _animator.SetBool("SecondAttack", false);
        _animator.SetBool("FinishAttack", true);
    }
}
