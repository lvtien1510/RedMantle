using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MovingLedge : MonoBehaviour
{
    [SerializeField] private Transform postA, postB;
    [SerializeField] private int speed;
    Vector2 targetPos;
    private void Start()
    {
        targetPos = postB.position;
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, postA.position) < 0.1f) targetPos = postB.position;

        if (Vector2.Distance(transform.position, postB.position) < 0.1f) targetPos = postA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime );

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(postA.position, postB.position);
    }
}
