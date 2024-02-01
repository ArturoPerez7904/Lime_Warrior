using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float damage;
    private float nextAttackTime;
    public float attackRate;
    public Transform sweepAttackPos;
    public float sweepAttackRange;
    public Transform pokeAttackPos;
    public float pokeAttackRange;
    public LayerMask whatIsEnemies;

    private Animator anim;


    private void Start()
    {

        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (Time.time >= nextAttackTime)
        {

            if (Input.GetKey(KeyCode.F))
            {
                SweepAttack();
                nextAttackTime = Time.time + attackRate;
            }

            else if (Input.GetKey(KeyCode.G))
            {
                PokeAttack();
                nextAttackTime = Time.time + attackRate;
            }
        }

    }

    private void SweepAttack()
    {
        anim.SetTrigger("sweepAttackTrigger");
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(sweepAttackPos.position, sweepAttackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sweepAttackPos.position, sweepAttackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pokeAttackPos.position, pokeAttackRange);
    }

    private void PokeAttack()
    {
        anim.SetTrigger("pokeAttackTrigger");
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(pokeAttackPos.position, pokeAttackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
