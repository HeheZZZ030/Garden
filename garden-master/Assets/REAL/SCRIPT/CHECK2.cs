using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHECK2 : MonoBehaviour
{
    public GameObject imageObject;
    public GameObject interactionPanel;
    private bool isPlayerIn;
    private bool isInteractionPanelActive;
    private PLAYERCONTROLLER playercontroller;
    public AnimationCurve showCurve;
    public AnimationCurve hideCurve;
    public float animationSpeed;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playercontroller = player.GetComponent<PLAYERCONTROLLER>();
        }
        if (interactionPanel != null)
        {
            isInteractionPanelActive = false;
        }
    }

    private void Update()
    {
        if (isPlayerIn && !isInteractionPanelActive)
        {
            imageObject.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                DetectClickInTriggerArea();
            }
        }
        else
        {
            imageObject.SetActive(false);
        }
        if (isPlayerIn && isInteractionPanelActive)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(HidePanel(interactionPanel));
            }
        }

    }

    void DetectClickInTriggerArea()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D collider = imageObject.GetComponent<Collider2D>();

        if (collider.OverlapPoint(mousePos))
        {
            StartCoroutine(ShowPanel(interactionPanel));
            isInteractionPanelActive = true;

            if (playercontroller != null)
            {
                playercontroller.enabled = false;
            }
        }
    }

    IEnumerator ShowPanel(GameObject CHECK1UI)
    {
        float timer = 0;
        while (timer <= 1)
        {
            CHECK1UI.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
    }

    IEnumerator HidePanel(GameObject CHECK1UI)
    {
        float timer = 0;
        while (timer <= 1)
        {
            CHECK1UI.transform.localScale = Vector3.one * hideCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        isInteractionPanelActive = false;
        if (playercontroller != null)
        {
            playercontroller.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerIn = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
         && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerIn = false;
            imageObject.SetActive(false);
        }
    }
}