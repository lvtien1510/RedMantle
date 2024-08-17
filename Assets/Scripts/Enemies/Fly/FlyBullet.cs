using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyBullet : MonoBehaviour
{
    private GameObject target;
    public float speed;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<HealthBar>().UpdateHealth(GameObject.FindObjectOfType<FlyShooter>().bulletDamage);
            Destroy(this.gameObject);
        }
        
        
    }
}
