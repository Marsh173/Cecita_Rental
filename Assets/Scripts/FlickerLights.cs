using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlickerLights : MonoBehaviour
{
    private Renderer rendererComponent;
    private Color originalEmissionColor;
    private Light spotlight;

    public bool flickering = true;

    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float minTime = 0.1f;
    public float maxTime = 0.5f;

    public float flickerSpeed = 1f;

    private float targetIntensity;
    private Color targetEmissionColor;

    private void Start()
    {
        try
        {
            rendererComponent = GetComponent<Renderer>();
            originalEmissionColor = rendererComponent.material.GetColor("_EmissionColor"); 
        }
        catch { }

        try
        {
            spotlight = GetComponent<Light>();
        }
        catch { }


        if(rendererComponent != null)
            StartCoroutine(FlickerEmission());

        if (spotlight != null)
            StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerEmission()
    {
        while (true)
        {
            if (flickering)
            {
                // Determine the target emission color based on a random intensity value within the specified range
                float intensity = Random.Range(minIntensity, maxIntensity);
                targetEmissionColor = originalEmissionColor * intensity;

                // Gradually adjust the emission color towards the target emission color
                while (rendererComponent.material.GetColor("_EmissionColor") != targetEmissionColor)
                {
                    Color currentColor = rendererComponent.material.GetColor("_EmissionColor");
                    Color newColor = Color.Lerp(currentColor, targetEmissionColor, Time.deltaTime * flickerSpeed);
                    rendererComponent.material.SetColor("_EmissionColor", newColor);
                    yield return null;
                }

                // Wait for a random duration before flickering again
                float flickerDuration = Random.Range(minTime, maxTime);
                yield return new WaitForSeconds(flickerDuration);
            }
            else
            {
                // Restore the original emission color
                rendererComponent.material.SetColor("_EmissionColor", originalEmissionColor);

                yield return null;
            }
        }
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            if (flickering)
            {
                // Determine the target intensity based on a random value within the specified range
                targetIntensity = Random.Range(minIntensity, maxIntensity);

                // Gradually adjust the intensity towards the target intensity
                while (Mathf.Abs(spotlight.intensity - targetIntensity) > 0.01f)
                {
                    spotlight.intensity = Mathf.Lerp(spotlight.intensity, targetIntensity, Time.deltaTime * flickerSpeed);
                    yield return null;
                }

                // Wait for a random duration before flickering again
                float flickerDuration = Random.Range(minTime, maxTime);
                yield return new WaitForSeconds(flickerDuration);
            }
            else
            {
                yield return null;
            }
        }
    }
}
