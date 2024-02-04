using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float health;
    public float jumpForce;
    public float gravity;
    public float horizontalVelocity;
    float verticalVelocity;
    float speed;
    GroundCheck groundCheck;
    private bool isFacingRight = true;

    public float jumpBufferTime;
    private float jumpBufferCounter;
    private int allowedJumps = 1;

    PlayerControls controls;

    private void Awake()
    {

        controls = new PlayerControls();
        controls.Player.Jump.performed += ctx => OnJump(ctx);
    }

    private void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
    }

    void Update()
    {

        verticalVelocity += -gravity * Time.deltaTime;


        if (groundCheck.isGrounded && verticalVelocity < 0)
        {

            float playerHeightOffset = 1f;
            verticalVelocity = 0;
            transform.position = new Vector3(transform.position.x, groundCheck.surfacePosition.y + playerHeightOffset, transform.position.z);
            allowedJumps = 1;
        }

        jumpBufferCounter -= Time.deltaTime;

        if ((groundCheck.coyoteCounter > 0f) && jumpBufferCounter >= 0 && allowedJumps > 0)
        {
            verticalVelocity = jumpForce;
            allowedJumps = 0;
        }

        speed = Input.GetAxisRaw("Horizontal") * Time.deltaTime * horizontalVelocity * 100;
        FlipScale();
        transform.Translate(new Vector3(speed, verticalVelocity, 0) * Time.deltaTime);
    }

    void FlipScale()
    {

        if (speed < 0 && isFacingRight || speed > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }

    }

    public void TakeDamage(float damage)
    {

        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.started)
        {

            jumpBufferCounter = jumpBufferTime;

        }

        if (context.canceled && verticalVelocity > 0)
        {

            verticalVelocity = verticalVelocity / 2;
        }
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
