using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x=Input.GetAxisRaw("Horizontal");
        animator.SetFloat("horizontal",movement.x);
        animator.SetFloat("speed",movement.sqrMagnitude);
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movespeed*Time.deltaTime);
    }
}
