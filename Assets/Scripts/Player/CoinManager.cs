using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;
    private int currentCoins;
    private int valuation = 1;

    public QuestGiver questGiver;

    private void Start()
    {
        currentCoins = PlayerPrefs.GetInt("Coin", 0);
        textCoin.text = currentCoins.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Item"))
        {
            // Thay đổi dòng này để lấy itemID từ collider
            Crystal item = collision.collider.GetComponent<Crystal>();
            if (item != null)
            {
                currentCoins += valuation;
                textCoin.text = currentCoins.ToString();
                PlayerPrefs.SetInt("Coin", currentCoins);
                // Gọi ItemCollected của QuestGiver với itemID của item
                if (questGiver != null)
                {
                    questGiver.ItemCollected(item.itemID);
                }

                Destroy(collision.gameObject);
            }
            else
            {
                Debug.LogWarning("Crystal component not found on item.");
            }
        }
    }
}
