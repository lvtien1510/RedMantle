using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject[] item;
    [SerializeField] GameObject image;
    private bool playerIsClose;
    [SerializeField] Transform instantiationPoitn;
    [SerializeField] int chestID;
    private bool isOpened;
    private Animator anim;
    private void Start()
    {
        isOpened = bool.Parse(PlayerPrefs.GetString("Chest" + chestID, "false"));
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetBool("IsOpened", isOpened);
        if (playerIsClose && !isOpened)
        {
            image.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < item.Length; i++)
                { 
                    Instantiate(item[i], instantiationPoitn.position, Quaternion.identity);
                }
                isOpened = true;
                PlayerPrefs.SetString("Chest" + chestID, isOpened.ToString());
            }
        }
        else
        {
            image.SetActive(false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
