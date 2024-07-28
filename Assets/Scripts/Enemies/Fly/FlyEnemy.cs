using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool chase = false;
    [SerializeField] private Transform startingPoint;
    [SerializeField] private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }
        if (chase == true)
            Chase();
        else
            ReturnStartPoint();
        Flip();
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //if (Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
        //{
        //    //Change speed, shoot, animation
        //}
        //else
        //{
        //    //reset variables
        //}
    }
    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
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
