using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField] private float speed;
    [Range(1, 10)]
    [SerializeField] private float lifeTime;
    [SerializeField] private int damage; // Sát thương của đạn
    private float throwAngle = 45f; // Góc ném

    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Animator animator;
    private bool hit;

    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        // Apply initial force based on direction and speed
        float angleInRadians = throwAngle * Mathf.Deg2Rad;
        Vector2 force = new Vector2(direction.x * speed * Mathf.Cos(angleInRadians), speed * Mathf.Sin(angleInRadians));
        rb.AddForce(force, ForceMode2D.Impulse);

        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        if (hit) return;

        // Rotate the bullet based on its velocity
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Gây sát thương nếu va vào kẻ thù
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("20");
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        hit = true;

        // Ngừng di chuyển viên đạn
        rb.velocity = Vector2.zero;

        // Kích hoạt animation nổ và gọi Coroutine để hủy đối tượng sau khi animation hoàn tất
        animator.SetTrigger("Explode");
        Destroy(gameObject, 0.3f);
    }

    public void SetDirection(Vector2 _direction)
    {
        direction = _direction;
    }

    public void SetThrowAngle(float angle)
    {
        throwAngle = angle;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
}
