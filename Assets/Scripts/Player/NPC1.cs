using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC1 : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    private bool playerIsClose;
    [SerializeField] private GameObject interactionPrompt;
    private Camera _camera;

    [SerializeField] private GameObject shop;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (playerIsClose)
        {
            interactionPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (shop.activeSelf)
                {
                    shop.SetActive(false);
                    playerMovement.enabled = true;
                    playerAttack.enabled = true;
                }
                else
                {

                    shop.SetActive(true);
                    playerMovement.enabled = false;
                    playerAttack.enabled = false;
                }
            }
        }
        else interactionPrompt.SetActive(false);
        
        interactionPrompt.transform.rotation = _camera.transform.rotation;
        Flip();
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

    private void Flip()
    {
        if (transform.position.x < playerMovement.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
