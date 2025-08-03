using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruzMother : MonoBehaviour
{
    //For idle stage
    [Header("Idle")]
    [SerializeField] float idleMoveSpeed;
    [SerializeField] Vector2 idleMoveDirection;
    //For attack up N down stage
    [Header("AttackUpNDown")]
    [SerializeField] float attackMoveSpeed;
    [SerializeField] Vector2 attackMoveDirection;
    //For attack player stage
    [Header("AttackPlayer")]
    [SerializeField] float attackPlayerSpeed;
    [SerializeField] Transform player;
    private Vector2 playerPositon;
    private bool hasPlayerPosition;
    //Other
    [Header("Others")]
    [SerializeField] Transform groundCheckUp;
    [SerializeField] Transform groundCheckDown;
    [SerializeField] Transform groundCheckWall;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;
    private bool goingUp = true;
    private bool facingLeft = true;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, groundLayer);
        //IdleState();
    }
    void RandomStatePicker()
    {
        int randomwState = Random.Range(0, 2);
        if(randomwState == 0)
        {
            anim.SetTrigger("AttackUpNDown");
        }
        else if(randomwState == 1)
        {
            anim.SetTrigger("AttackPlayer");
        }
    }
    public void IdleState()
    {
        if(isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if(isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }
        if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }
        }
        rb.velocity = idleMoveSpeed * idleMoveDirection;
    }
    public void AttackUpNDown()
    {
        if (isTouchingUp && goingUp)
        {
           
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }
        if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }
        }
        rb.velocity = attackMoveSpeed * attackMoveDirection;
    }
    public void AttackPlayer()
    {
        if (!hasPlayerPosition)
        {
            playerPositon = player.position - transform.position;
            playerPositon.Normalize();
            hasPlayerPosition = true;
        }
        if (hasPlayerPosition)
        {
            rb.velocity = playerPositon * attackPlayerSpeed;
        }
        if(isTouchingWall || isTouchingDown)
        {
            anim.SetTrigger("Slam");
            rb.velocity = Vector2.zero;
            hasPlayerPosition = false;
            
        }
        
        
     }
    private void FlipTowardPlayer()
    {
        float playerDirection = player.position.x - transform.position.x;
        if(playerDirection > 0 && facingLeft)
        {
            Flip();
        }
        else if(playerDirection < 0 && !facingLeft)
        {
            Flip();
        }
    }
    private void ChangeDirection()
    {
        goingUp = !goingUp;
        idleMoveDirection.y *= -1;
        attackMoveDirection.y *= -1;
    }
    private void Flip()
    {
        facingLeft = !facingLeft;
        idleMoveDirection.x *= -1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckWall.position, groundCheckRadius);

    }
}
