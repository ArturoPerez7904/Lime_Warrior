using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float damage;
    private float nextAttackTime;
    public float attackRate;
    public Transform attackPos;
    public float attackRange;
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
                Attack();
                nextAttackTime = Time.time + attackRate;
            }
        }

    }

    private void Attack()
    {
        anim.SetTrigger("attackTrigger");
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
