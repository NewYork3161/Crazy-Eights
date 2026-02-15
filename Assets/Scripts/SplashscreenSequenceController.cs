using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashscreenSequenceController : MonoBehaviour
{
    [Header("MAIN SPLASH")]
    public Image mainSplash;

    public float mainHoldTime = 3f;
    public float mainFadeIn = 0.4f;
    public float mainFadeOut = 0.4f;

    [Header("FLASH IMAGES (ORDER MATTERS)")]
    public Image[] flashImages;

    public float flashHoldTime = 0.5f;
    public float flashFadeIn = 0.05f;
    public float flashFadeOut = 0.2f;

    [Header("BLINK OPTIONS")]
    public bool enableBlink = false;
    public float blinkDuration = 0.08f;
    public int blinkCount = 2;

    private int flashIndex = 0;

    void Start()
    {
        if (mainSplash == null)
        {
            Debug.LogError("SplashscreenSequenceController: MAIN SPLASH not assigned.");
            return;
        }

        // Main visible at start
        SetAlpha(mainSplash, 1f);

        // All flash images hidden
        foreach (Image img in flashImages)
        {
            if (img != null)
                SetAlpha(img, 0f);
        }

        StartCoroutine(SequenceLoop());
    }

    IEnumerator SequenceLoop()
    {
        while (true)
        {
            // MAIN SCREEN
            yield return ShowMain();

            // FLASH SCREEN
            if (flashImages.Length > 0)
            {
                yield return ShowFlash(flashImages[flashIndex]);

                flashIndex++;
                if (flashIndex >= flashImages.Length)
                    flashIndex = 0;
            }
        }
    }

    IEnumerator ShowMain()
    {
        yield return Fade(mainSplash, 0f, 1f, mainFadeIn);

        yield return new WaitForSeconds(mainHoldTime);

        yield return Fade(mainSplash, 1f, 0f, mainFadeOut);
    }

    IEnumerator ShowFlash(Image img)
    {
        if (img == null)
            yield break;

        yield return Fade(img, 0f, 1f, flashFadeIn);

        float elapsed = 0f;

        while (elapsed < flashHoldTime)
        {
            if (enableBlink)
            {
                for (int i = 0; i < blinkCount; i++)
                {
                    SetAlpha(img, 0f);
                    yield return new WaitForSeconds(blinkDuration);
                    SetAlpha(img, 1f);
                    yield return new WaitForSeconds(blinkDuration);
                }
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return Fade(img, 1f, 0f, flashFadeOut);
    }

    IEnumerator Fade(Image img, float from, float to, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            float a = Mathf.Lerp(from, to, t / duration);
            SetAlpha(img, a);

            yield return null;
        }

        SetAlpha(img, to);
    }

    void SetAlpha(Image img, float a)
    {
        if (img == null)
            return;

        Color c = img.color;
        c.a = a;
        img.color = c;
    }
}
