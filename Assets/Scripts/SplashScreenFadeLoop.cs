using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreenBlinkLoop : MonoBehaviour
{
    [Header("Timing")]
    public float repeatInterval = 4f;   // time between blinks
    public float blinkOffTime = 0.08f;  // how long it disappears (blink)

    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();

        if (img == null)
        {
            Debug.LogError("SplashScreenBlinkLoop: No Image component found!");
            enabled = false;
            return;
        }

        // Start visible so you don't see nothing at launch
        img.enabled = true;
    }

    void Start()
    {
        StartCoroutine(BlinkLoop());
    }

    IEnumerator BlinkLoop()
    {
        while (true)
        {
            // stay visible for 4 seconds
            yield return new WaitForSeconds(repeatInterval);

            // blink OFF
            img.enabled = false;

            // short blink
            yield return new WaitForSeconds(blinkOffTime);

            // back ON
            img.enabled = true;
        }
    }
}
