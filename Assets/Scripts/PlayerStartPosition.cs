using UnityEngine;

public class PlayerStartPosition : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance != null)
        {
            transform.position = GameManager.Instance.startPosition;
        }
    }
}
