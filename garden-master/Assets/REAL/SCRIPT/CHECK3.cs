using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHECK3 : MonoBehaviour
{
    public GameObject imageObject;
    private bool isPlayerIn;

    void Start()
    {

    }

    void Update()
    {
        if (isPlayerIn)
        {
            imageObject.SetActive(true);
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