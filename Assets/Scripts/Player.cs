using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float gravity = -10f;
    public float speedForce;
    float velocity;
    bool canJump;
    public GroundCheck groundCheck;

    private void Start()
    {

        groundCheck = GetComponent<GroundCheck>();
    }

    void Update()
    {

        velocity += gravity * Time.deltaTime;


        if (groundCheck.isGrounded && velocity < 0)
        {

            float playerHeightOffset = 1f;
            velocity = 0;
            transform.position = new Vector3(transform.position.x, groundCheck.surfacePosition.y + playerHeightOffset, transform.position.z);
            canJump = true;
        }

        if (groundCheck.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {

            velocity = jumpForce;
        }

        float speed = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speedForce * 100;

        transform.Translate(new Vector3(speed, velocity, 0) * Time.deltaTime);
    }


}
