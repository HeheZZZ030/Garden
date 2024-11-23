using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    Vector2 targetPos;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip clickSound;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
        }

        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = targetPos;
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("ClickTrigger");
            if (audioSource && clickSound)
            {
                audioSource.PlayOneShot(clickSound);
            }
        }
    }
}
