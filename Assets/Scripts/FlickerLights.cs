using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using FMODUnity;

public class FlickerLights : FMODStudioNewEmitter
{
    
    private Renderer rendererComponent;
    private Light spotlight;
    private bool enableEmission;


    [Header("Flicker Properties")]
    public float minTime = 0.1f;
    public float maxTime = 0.5f;

    private void Start()
    {
        try
        {
            rendererComponent = GetComponent<Renderer>();   
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
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            if (enableEmission)
            {
                rendererComponent.material.EnableKeyword("_EMISSION");
                enableEmission = false;
            }
            else
            {
                rendererComponent.material.DisableKeyword("_EMISSION");
                enableEmission = true;
            }
        }
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            spotlight.enabled = !spotlight.enabled;
        }   
    }
}
