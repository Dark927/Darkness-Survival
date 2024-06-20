using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class DisableLightAfterTime : MonoBehaviour
{
    [SerializeField] float timeToDisable = 1f;
    float elapsedTime;
    Light2D spotlight2D;

    private void OnEnable()
    {
        spotlight2D = gameObject.GetComponent<Light2D>();
        if (spotlight2D != null)
        {
            elapsedTime = 0f;
            StartCoroutine(FadeOutAndDisable());
        }

    }

    private IEnumerator FadeOutAndDisable()
    {
        float startIntensity = spotlight2D.intensity;
        float targetIntensity = 0f;

        while (elapsedTime < timeToDisable)
        {
            float newIntensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / timeToDisable);
            spotlight2D.intensity = newIntensity;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        spotlight2D.intensity = targetIntensity;
        gameObject.SetActive(false);
    }
}
