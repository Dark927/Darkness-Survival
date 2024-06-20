using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    Light2D globalLight2D;

    [Header("Setting")]
    [SerializeField] Color nightColor;
    [SerializeField] float transitionMinutes = 2f;

    float targetIntensity;
    float dayIntensity = 0.7f;
    float transitionSeconds;

    Color targetColor;
    float nightIntensity = 0.05f;
    bool isChangeToNight = true;

    private Coroutine activeCoroutine;

    void Start()
    {
        globalLight2D = GetComponent<Light2D>();
        transitionSeconds = transitionMinutes * 60f;
        SetTargetColor(nightColor);
        SetNight();
    }

    // Coroutine for changing color

    IEnumerator ChangeLightOverTime(Color targetColor, float targetIntensity, float time)
    {
        // Variables 

        float initialIntensity = globalLight2D.intensity;
        Color initialColor = globalLight2D.color;
        float elapsedTime = 0f;

        // Change light

        while (elapsedTime < time)
        {
            float intensity = Mathf.Lerp(initialIntensity, targetIntensity, elapsedTime / time);
            globalLight2D.intensity = intensity;

            Color color = Color.Lerp(initialColor, targetColor, elapsedTime / time);
            globalLight2D.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Final values 

        globalLight2D.intensity = targetIntensity;
        globalLight2D.color = targetColor;
    }

    // Set light settings 

    void SetNight()
    {
        // Stop current change 

        if (!isChangeToNight && activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }

        // Start light change 

        isChangeToNight = true;
        targetIntensity = nightIntensity;
        activeCoroutine = StartCoroutine(ChangeLightOverTime(targetColor, targetIntensity, transitionSeconds));
    }

    void SetDay()
    {
        // Stop current change 

        if(isChangeToNight && activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }

        // Start light change 

        isChangeToNight = false;
        targetIntensity = dayIntensity;
        activeCoroutine = StartCoroutine(ChangeLightOverTime(targetColor, targetIntensity, transitionSeconds));
    }

    void SetTargetColor(Color newColor)
    {
        targetColor = newColor;
    }
}
