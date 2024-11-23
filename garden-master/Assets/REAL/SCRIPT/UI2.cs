using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI2 : MonoBehaviour
{
    public Image uiImage;
    public float delayBeforeShowing; 
    public float fadeDuration;
    public float displayDuration;

    void Start()
    {
        Color color = uiImage.color;
        color.a = 0f;
        uiImage.color = color;
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        yield return new WaitForSeconds(delayBeforeShowing);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(displayDuration);
        yield return StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = uiImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            uiImage.color = color;
            yield return null;
        }
        color.a = 1f;
        uiImage.color = color;
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = uiImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            uiImage.color = color;
            yield return null;
        }
        color.a = 0f;
        uiImage.color = color;
    }
}
