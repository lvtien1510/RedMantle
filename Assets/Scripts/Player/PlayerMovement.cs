using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool isDead = false;

    private HealthBar hp;
    private MP mp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        hp = FindObjectOfType<HealthBar>();
        mp = FindObjectOfType<MP>();
    }
    private void Update()
    {
        if (isDead) return;

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
        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime)
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
        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
        Flip();
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        isDead = true;
        if (isDead)
        {
            gameObject.GetComponent<HealthBar>().enabled = false;
            gameObject.GetComponent<MP>().enabled = false;
            gameObject.GetComponent<PlayerAttack>().enabled = false;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<HealthBar>().enabled = true;
            gameObject.GetComponent<MP>().enabled = true;
            gameObject.GetComponent<PlayerAttack>().enabled = true;
            rb.isKinematic = false;
            gameObject.GetComponent<Collider2D>().enabled = true;
           
        }
        hp.currentHealth = hp.maxHealth;
        mp.currentMP = mp.maxMP;
        GameManager.Instance.startPosition = Vector3.zero;
        StartCoroutine(LoadAfterDelay());

    }
    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(3);
        isDead = false;
        SceneManager.LoadScene("Lv2");
        
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
