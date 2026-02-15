using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartButtonFade : MonoBehaviour
{
    [Header("Fade Settings")]
    public float fadeDuration = 1.2f;   // Time to fade in/out
    public float minAlpha = 0.25f;      // Lowest visibility
    public float maxAlpha = 1f;         // Full visibility

    private Graphic graphic;

    void Awake()
    {
        graphic = GetComponent<Graphic>();

        if (graphic == null)
        {
            Debug.LogError("StartButtonFade requires an Image or UI Graphic component.");
            enabled = false;
        }
    }

    void OnEnable()
    {
        StartCoroutine(FadeLoop());
    }

    IEnumerator FadeLoop()
    {
        while (true)
        {
            yield return FadeTo(maxAlpha);
            yield return FadeTo(minAlpha);
        }
    }

    IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = graphic.color.a;
        float timer = 0f;

        Color c = graphic.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            c.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            graphic.color = c;

            yield return null;
        }

        c.a = targetAlpha;
        graphic.color = c;
    }
}
