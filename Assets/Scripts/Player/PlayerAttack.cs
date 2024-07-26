using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private float nextAttackTime = 0f;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayers;
    [Range(1, 1000)]
    [SerializeField] private int attackDamage; // Sát thương của mỗi đòn đánh
    [Range(1.6f, 2.4f)]
    [SerializeField] private float attackRate; // Tốc độ tấn công

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] Transform firingPoint;
    public bool isShooting = false;
    private MP mp;

    void Start()
    {
        animator = GetComponent<Animator>();
        mp = FindObjectOfType<MP>();
    }

    void Update()
    {
       
            HandleAttack();
            Shoot();
        
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Time.time > nextAttackTime && mp.currentMP > 0)
            {
                Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
                nextAttackTime = Time.time + 0.5f;
                isShooting = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            isShooting = false;
        }
    }

    void HandleAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKey(KeyCode.H))
            {
                PerformAttack();
                nextAttackTime = Time.time + 1f / attackRate; // Tính thời gian cho lần tấn công tiếp theo
            }
        }
    }

    void PerformAttack()
    {
        animator.SetTrigger("Attack");

        // Xác định các kẻ thù trong phạm vi tấn công
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Gây sát thương cho từng kẻ thù bị trúng
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Health>().TakeDamage(attackDamage); // Gọi hàm TakeDamage của kẻ thù

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
