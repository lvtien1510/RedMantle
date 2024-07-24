﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField] private float speed;

    [Range(1, 10)]
    [SerializeField] private float lifeTime;

    [SerializeField] private int damage; // Sát thương của đạn

    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Animator animator;
    private bool hit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        if (hit) return;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Gây sát thương nếu va vào kẻ thù
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("20");
            //Enemy enemy = collision.GetComponent<Enemy>();
            //if (enemy != null)
            //{
            //    enemy.TakeDamage(damage);
            //}
        }
        hit = true;
        // Thiết lập trạng thái hit và vô hiệu hóa collider
        capsuleCollider.enabled = false;
        //if (rb.velocity.x > 0.01f)
        //{
        //    transform.position += new Vector3(1.3f, 0, 0);
        //}
        //else if (rb.velocity.x < -0.01f)
        //{
        //    transform.position -= new Vector3(1.3f, 0, 0);
        //}
        // Ngừng di chuyển viên đạn
        rb.velocity = Vector2.zero;
        
        // Kích hoạt animation nổ và gọi Coroutine để hủy đối tượng sau khi animation hoàn tất
        animator.SetTrigger("Explode");
        Destroy(gameObject, 0.3f);
    }
}
