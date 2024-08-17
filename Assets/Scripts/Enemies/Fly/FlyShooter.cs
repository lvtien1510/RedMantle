using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyShooter : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lineOfSize;
    public float shootingRange;
    public GameObject bullet;
    public GameObject pointBullet;
    [SerializeField] private float fireRate;
    private float nextFireTime;
    [SerializeField] private Transform player;
    public int bulletDamage;

    Animator anim;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSize && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        } 
        else if(distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            anim.SetTrigger("Attack");         
            nextFireTime = Time.time + fireRate;
            transform.position = this.transform.position;
        }
        else if(distanceFromPlayer < shootingRange && distanceFromPlayer > 0.5f * shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, -(speed / 2) * Time.deltaTime);
        }

        Flip();
    }
    public void EnemyShooting()
    {
        Instantiate(bullet, pointBullet.transform.position, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lineOfSize);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0); // Không xoay
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180f, 0); // Xoay 180 độ quanh trục y
        }
    }
}
