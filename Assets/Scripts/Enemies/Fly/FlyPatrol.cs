using System.Collections;
using UnityEngine;

public class FlyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 enemy;
    [SerializeField] private float speed;
    private bool coroutineAllowed;
    private void Start()
    {
        routeToGo = 0;
        tParam = 0;
        coroutineAllowed = true;
    }
    private void Update()
    {
        if(coroutineAllowed) 
            StartCoroutine(GoByTheRoute(routeToGo));
    }
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;
        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        while(tParam < 1)
        {
            tParam += Time.deltaTime * speed;
            enemy = Mathf.Pow(1 - tParam, 3) * p0 + 
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + 
                Mathf.Pow(tParam, 3) * p3;
            transform.position = enemy;

            yield return new WaitForEndOfFrame();
        }
        tParam = 0f;
        // Gọi Flip khi đi từ p0 đến p3
        if (routeToGo == 1 || routeToGo == 3)
        {
            Flip();
        }
        routeToGo += 1;
        if (routeToGo > routes.Length - 1)
            routeToGo = 0;
        coroutineAllowed = true;
    }
    private void Flip()
    {
        transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y == 0 ? 180f : 0, 0);
    }
}
