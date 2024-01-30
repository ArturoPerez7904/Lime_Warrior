using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;
    public float offset = 0.1f;
    public Vector2 surfacePosition;
    public LayerMask groundMask;
    Collider2D groundCheck;
    Vector2 point;
    Vector2 size;

    private void Update()
    {
        point = transform.position + Vector3.down * offset;
        size = new Vector2(transform.localScale.x, transform.localScale.y);

        groundCheck = Physics2D.OverlapBox(point, size, 0, groundMask);

        if (groundCheck != null)
        {

            isGrounded = true;
            surfacePosition = Physics2D.ClosestPoint(transform.position, groundCheck);

        }

        else
        {
            isGrounded = false;
        }
    }
}
