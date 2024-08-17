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
    public int attackDamage; // Sát thương của mỗi đòn đánh
    [Range(1.6f, 2.4f)]
    [SerializeField] private float attackRate; // Tốc độ tấn công

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] Transform firingPoint;
    public bool isShooting = false;
    [SerializeField] private GameObject bulletPrefabB;
    [SerializeField] Transform firingPointB;
    public int reduceMP;
    public int amountBonus;
    private MP mp;

    void Awake()
    {
        animator = GetComponent<Animator>();
        mp = FindObjectOfType<MP>();
    }

    void Update()
    {
        HandleAttack();
        Shoot();
        ShootB();
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Time.time > nextAttackTime && mp.currentMP >= 7)
            {
                Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
                nextAttackTime = Time.time + 0.5f;
                reduceMP = 10;
                isShooting = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            isShooting = false;
        }
    }

    public void ShootB()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Time.time > nextAttackTime && mp.currentMP >= 25)
            {
                for (int i = 0; i < 6; i++)
                {
                    GameObject bullet = Instantiate(bulletPrefabB, firingPointB.position, firingPointB.rotation);
                    Bullet2 bulletScript = bullet.GetComponent<Bullet2>();

                    // Determine the direction based on the player's facing direction
                    Vector2 direction = transform.localRotation.eulerAngles.y == 180 ? Vector2.left : Vector2.right;
                    bulletScript.SetDirection(direction);

                    // Randomize the throw angle between 20 and 50 degrees
                    float randomThrowAngle = Random.Range(20f, 50f);
                    bulletScript.SetThrowAngle(randomThrowAngle);

                    // Randomize the speed between 10 and 15
                    float randomSpeed = Random.Range(10f, 15f);
                    bulletScript.SetSpeed(randomSpeed);
                }
                reduceMP = 30;
                nextAttackTime = Time.time + 0.5f;
                isShooting = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.L))
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
