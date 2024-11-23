using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{
    public float scaleFactor = 1.2f; 
    private Vector3 originalScale;
    public string sceneToLoad; 
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null && collider.OverlapPoint(mousePosition))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    private void OnMouseEnter()
    {
        transform.localScale = originalScale * scaleFactor;
    }

    private void OnMouseExit()
    {
        transform.localScale = originalScale;
    }
}
