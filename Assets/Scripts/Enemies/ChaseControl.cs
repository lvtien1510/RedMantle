//using UnityEngine;

//public class ChaseControl : MonoBehaviour
//{
//    [SerializeField] private ChaseFly[] enemyArray;
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            foreach(ChaseFly enemy in enemyArray)
//            {
//                enemy.chase = true;
//            }
//        }
//    }
//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            foreach(ChaseFly enemy in enemyArray)
//            {
//                enemy.chase = false;
//            }
//        }
//    }
//}
