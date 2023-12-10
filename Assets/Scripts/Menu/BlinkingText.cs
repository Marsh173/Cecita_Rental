using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    public bool startBlinking = true;

    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        if (startBlinking)
        {
            // Start the blinking coroutine if enabled
            StartCoroutine(BlinkText());
        }
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            yield return FadeText(0f, 1f, fadeInDuration); // Fade In

            yield return new WaitForSeconds(0.5f); // Wait

            yield return FadeText(1f, 0f, fadeOutDuration); // Fade Out

            yield return new WaitForSeconds(0.5f); // Wait
        }
    }

    IEnumerator FadeText(float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = textMesh.color;

        while (elapsedTime < duration)
        {
            textMesh.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, targetAlpha), (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }
}
