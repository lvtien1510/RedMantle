using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Speed")]
    [SerializeField] float speed;

    [Header("Jump")]
    [SerializeField] float jumpTime;
    [SerializeField] int jumpPower;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float horizontalInput;
    private bool isJumping;
    private float jumpCounter;
    Vector2 vecGravity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        animator.SetFloat("Move", Mathf.Abs(horizontalInput));
        animator.SetFloat("Jump", rb.velocity.y);
        animator.SetBool("IsGround", IsGround());

        if (Input.GetButtonDown("Jump") && IsGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            jumpCounter = 0;
        }
        if(rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if(jumpCounter > jumpTime) 
                isJumping = false;
            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;
            if (t > 0.5)
                currentJumpM = jumpMultiplier * (1 - t);

            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        if(rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        Flip();
    }
    private void Flip()
    {
        if (horizontalInput > 0.01f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0); // Không xoay
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localRotation = Quaternion.Euler(0, 180f, 0); // Xoay 180 độ quanh trục y
        }
    }
    private bool IsGround()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.2f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);

    }
}
