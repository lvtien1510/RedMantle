using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private string[] dialogue;
    [SerializeField] private float wordSpeed;
    private int index;
    private bool playerIsClose;
    private bool isDialogueActive;

    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    [SerializeField] private GameObject interactionPrompt;
    private Camera _camera;

    [SerializeField] private QuestGiver questGiver;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (playerIsClose && !isDialogueActive)
        {
            interactionPrompt.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (dialoguePanel.activeInHierarchy)
                {
                    EndDialogue();
                }
                else
                {
                    StartDialogue();
                }
            }
        }
        else
        {
            interactionPrompt.SetActive(false);
        }

        if (isDialogueActive && dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextLine();
            }
        }
        interactionPrompt.transform.rotation = _camera.transform.rotation;
        Flip();
    }

    private void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = "";
        index = 0;
        isDialogueActive = true;
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        StartCoroutine(Typing());
    }

    private void EndDialogue()
    {
        ZeroText();
        isDialogueActive = false;
        playerMovement.enabled = true;
        playerAttack.enabled = true;

        // Check if quest can be received and start new quest if possible
        if (questGiver.canReceiveNewQuest == 1)
        {
            questGiver.ReceiveNewQuest();
        }
    }

    private void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        continueButton.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            EndDialogue();
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
            EndDialogue();
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
