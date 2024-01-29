using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;
    public float damage;
    private float nextAttackTime;
    public float attackRate;
    public Transform attackPos;
    public float attackRange;
    private Animator anim;
    public LayerMask whatisPlayer;

    private void Start()
    {

        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (Time.time >= nextAttackTime)
        {

            Attack();
            nextAttackTime = Time.time + attackRate;
        }

    }

    void Attack()
    {
        anim.SetTrigger("attackTrigger");
        Collider2D playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatisPlayer);

        if (playerToDamage != null)
        {

            playerToDamage.GetComponent<Player>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }



    public void TakeDamage(float damage)
    {

        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
