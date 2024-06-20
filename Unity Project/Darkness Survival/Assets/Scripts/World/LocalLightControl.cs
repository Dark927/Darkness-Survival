using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LocalLightControl : MonoBehaviour
{
    [SerializeField] Light2D globalLight;
    [SerializeField] float transitionSpeed = 2f;
    Light2D spotLight;

    float maxIntensity;        // Maximum intensity for the spot light
    [SerializeField] float minIntensity = 0f;   // Minimum intensity for the spot light

    private void Awake()
    {
        spotLight = GetComponent<Light2D>();
        maxIntensity = spotLight.intensity;
    }

    private void Start()
    {
        if(globalLight == null)
        {
            globalLight = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>();
        }
    }

    void Update()
    {
        AdjustSpotLightIntensity(globalLight.intensity);
    }

    void AdjustSpotLightIntensity(float globalIntensity)
    {
        // Map the global intensity to the spot light's intensity range

        float mappedIntensity = Map(globalIntensity, 0f, 0.75f, maxIntensity, minIntensity);
        spotLight.intensity = Mathf.Lerp(spotLight.intensity, mappedIntensity, Time.deltaTime * transitionSpeed);
    }

    float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}