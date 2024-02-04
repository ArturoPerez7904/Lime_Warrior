using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

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

    PlayerControls controls;

    private void Awake()
    {

        controls = new PlayerControls();
        controls.Player.SweepAttack.performed += ctx => OnSweepAttack(ctx);
        controls.Player.PokeAttack.performed += ctx => OnPokeAttack(ctx);
    }

    private void Start()
    {

        anim = GetComponent<Animator>();
    }
    public void OnSweepAttack(InputAction.CallbackContext ctx)
    {
        if (Time.time >= nextAttackTime && ctx.started)
        {

            SweepAttack();
            nextAttackTime = Time.time + attackRate;
        }
    }

    public void OnPokeAttack(InputAction.CallbackContext ctx)
    {
        if (Time.time >= nextAttackTime && ctx.started)
        {

            PokeAttack();
            nextAttackTime = Time.time + attackRate;
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

    private void PokeAttack()
    {
        anim.SetTrigger("pokeAttackTrigger");
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(pokeAttackPos.position, pokeAttackRange, whatIsEnemies);
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


    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
