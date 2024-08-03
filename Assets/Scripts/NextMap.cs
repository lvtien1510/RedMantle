using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMap : MonoBehaviour
{
    [SerializeField] private float delayTime;
    [SerializeField] private string nameScene;
    [SerializeField] private Vector3 startPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.startPosition = startPosition;
            collision.gameObject.SetActive(false);
            StartCoroutine(LoadAfterDelay());
        }
    }

    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(nameScene);
    }
    
}
