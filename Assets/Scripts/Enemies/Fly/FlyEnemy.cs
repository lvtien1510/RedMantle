using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lineOfSize;
    [SerializeField] private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSize)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }

        Flip();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lineOfSize);
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
