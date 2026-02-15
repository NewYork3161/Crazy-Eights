using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenAutoAdjust : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;

    private int lastScreenWidth;
    private int lastScreenHeight;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        ForceResize();
    }

    void Update()
    {
        // Detect resolution / window changes
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            ForceResize();
        }
    }

    void ForceResize()
    {
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;

        if (image.sprite == null)
            return;

        Sprite sprite = image.sprite;

        float screenRatio = (float)Screen.width / Screen.height;
        float imageRatio = sprite.rect.width / sprite.rect.height;

        // Anchor to full screen
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        // Scale to FILL screen (crop if needed)
        if (imageRatio > screenRatio)
        {
            // Image is wider → fit height
            float scale = Screen.height / sprite.rect.height;
            rectTransform.localScale = Vector3.one * scale;
        }
        else
        {
            // Image is taller → fit width
            float scale = Screen.width / sprite.rect.width;
            rectTransform.localScale = Vector3.one * scale;
        }
    }
}
