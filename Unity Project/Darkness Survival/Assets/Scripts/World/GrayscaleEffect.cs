using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GrayscaleEffect : MonoBehaviour
{
    // Attributes 

    public static GrayscaleEffect instance;

    [SerializeField] Volume volume;
    [SerializeField] float timeToChange = 2f;

    private ColorAdjustments colorAdjustments;
    private Vignette vignette;


    // Methods

    void Start()
    {
        instance = this;

        if (!volume.profile.TryGet(out colorAdjustments) || !volume.profile.TryGet(out vignette))
        {
            Debug.Log("GrayscaleEffect : Volume error");
        }
    }

    public void ToggleGrayscale(bool isGrayscale)
    {
        if(volume != null)
        {
        StartCoroutine(ChangeSaturation(isGrayscale ? -100f : 0f));
        }
        else
        {
            Debug.Log("ToogleGrayScale :: Volume is null !");
        }
    }

    // Color change coroutine

    private IEnumerator ChangeSaturation(float targetSaturation)
    {
        // Parameters 

        float targetContrast;
        float targetVignette;

        float elapsedTime = 0f;

        float startSaturation = colorAdjustments.saturation.value;
        float startContrast = colorAdjustments.contrast.value;
        float startVignette = vignette.intensity.value;

        // Check toogle mode 

        if (Mathf.Approximately(targetSaturation, -100f))
        {
            targetContrast = 30f;
            targetVignette = 0.47f;
        }
        else
        {
            targetContrast = 0f;
            targetVignette = 0.27f;
        }

        // Change color over time 

        while (elapsedTime < timeToChange)
        {
            elapsedTime += Time.unscaledDeltaTime;
            colorAdjustments.saturation.value = Mathf.Lerp(startSaturation, targetSaturation, elapsedTime / timeToChange);
            colorAdjustments.contrast.value = Mathf.Lerp(startContrast, targetContrast, elapsedTime / timeToChange);

            vignette.intensity.value = Mathf.Lerp(startVignette, targetVignette, elapsedTime / timeToChange);

            yield return null;
        }

        // Final change 

        colorAdjustments.saturation.value = targetSaturation;
        vignette.intensity.value = targetVignette;
    }
}