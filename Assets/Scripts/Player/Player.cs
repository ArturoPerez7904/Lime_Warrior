using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float gravity;
    public float horizontalVelocity;
    float verticalVelocity;
    float speed;
    GroundCheck groundCheck;
    private bool isFacingRight = true;

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
        }

        if (groundCheck.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {

            verticalVelocity = jumpForce;
        }

        if ((verticalVelocity > 0) && Input.GetKeyUp(KeyCode.Space))
        {

            verticalVelocity = verticalVelocity / 2;

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


}