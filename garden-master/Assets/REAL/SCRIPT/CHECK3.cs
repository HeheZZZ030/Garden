using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHECK3 : MonoBehaviour
{
    public GameObject imageObject;
    public GameObject dialogueBox;
    public Text dialogueText;
    private bool isPlayerIn = false;
    private bool isImageClicked = false;
    private int currentLine = 0;
    public string[] dialogueLines;
    private bool isInteractionPanelActive = false;
    private PLAYERCONTROLLER playercontroller;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playercontroller = player.GetComponent<PLAYERCONTROLLER>();
        }
        if (dialogueBox != null)
        {
            isInteractionPanelActive = false;
        }
    }

    void Update()
    {
        if (isImageClicked && Input.GetMouseButtonDown(0))
        {
            if (!isInteractionPanelActive)
            {
                dialogueText.text = string.Join("\n", dialogueLines);
                isInteractionPanelActive = true;

                if (playercontroller != null)
                {
                    playercontroller.enabled = false; 
                }
            }
            else
            {
                dialogueBox.SetActive(false);
                isInteractionPanelActive = false;

                if (playercontroller != null)
                {
                    playercontroller.enabled = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerIn = true;
            imageObject.SetActive(true);
        }
    }

    void OnMouseDown()
    {
        if (isPlayerIn)
        {
            isImageClicked = true;
            dialogueBox.SetActive(true); 

            dialogueText.text = string.Join("\n", dialogueLines);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerIn = false;  
            imageObject.SetActive(false); 

            if (isInteractionPanelActive)
            {
                dialogueBox.SetActive(false);
                isInteractionPanelActive = false;
                if (playercontroller != null)
                {
                    playercontroller.enabled = true; 
                }
            }
        }
    }
}