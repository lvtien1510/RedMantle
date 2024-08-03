using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private float addForce;
    public int itemID;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.AddForce(new Vector2(Random.Range(-0.6f, 0.6f), 1) * addForce, ForceMode2D.Impulse);
        
    }
    
}
