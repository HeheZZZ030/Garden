using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOOK : MonoBehaviour
{
    public GameObject imageObject;
    public GameObject interactionPanel;
    private bool isInteractionPanelActive;
    private PlayerController playercontroller;
    public AnimationCurve showCurve;
    public AnimationCurve hideCurve;
    public float animationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playercontroller = player.GetComponent<PlayerController>();
        }
        if (interactionPanel != null)
        {
            isInteractionPanelActive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteractionPanelActive)
        {

            if (Input.GetMouseButtonDown(0))
            {
                DetectClickInTriggerArea();
            }
        }

        if (isInteractionPanelActive)
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

    IEnumerator ShowPanel(GameObject panel)
    {
        float timer = 0;
        while (timer <= 1)
        {
            panel.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
    }

    IEnumerator HidePanel(GameObject panel)
    {
        float timer = 0;
        while (timer <= 1)
        {
            panel.transform.localScale = Vector3.one * hideCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        isInteractionPanelActive = false;
        if (playercontroller != null)
        {
            playercontroller.enabled = true;
        }
    }

}
