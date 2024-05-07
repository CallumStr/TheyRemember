using UnityEngine;
using System.Collections;

public class OverlayFade : MonoBehaviour
{
    public float fadeDuration = 2f; // Duration of the fade-out in seconds
    public bool fadeOutOnStart = true; // Whether to start with fade-out

    private Renderer overlayRenderer; // Reference to the Renderer component of the overlay GameObject
    private Color originalColor; // Original color of the overlay

    void Start()
    {
        overlayRenderer = GetComponent<Renderer>();
        originalColor = overlayRenderer.material.color;

        // Set the initial alpha value to fully opaque (alpha = 1)
        Color startColor = originalColor;
        startColor.a = 1f;
        overlayRenderer.material.color = startColor;

        if (fadeOutOnStart)
        {
            StartFadeOut();
        }
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Use cubic easing function to interpolate alpha value
            float t = elapsedTime / fadeDuration;
            t = Mathf.SmoothStep(0f, 1f, t);
            float alpha = Mathf.Lerp(1f, 0f, t);

            Color newColor = originalColor;
            newColor.a = alpha;
            overlayRenderer.material.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure alpha is exactly 0 at the end
        Color finalColor = originalColor;
        finalColor.a = 0f;
        overlayRenderer.material.color = finalColor;
    }
}
