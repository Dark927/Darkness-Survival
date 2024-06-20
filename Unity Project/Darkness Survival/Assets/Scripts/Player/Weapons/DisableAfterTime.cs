using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    [SerializeField] float timeToDisable = 2f;
    float timeToFadeOut;
    bool isAlphaStarted;
    float timer;
    float elapsedTime;


    private void OnEnable()
    {
        elapsedTime = 0f;
        timer = timeToDisable;
        timeToFadeOut = timeToDisable / 2f;
        isAlphaStarted = false;
    }


    private void LateUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= timeToFadeOut && !isAlphaStarted)
        {
            StartCoroutine(FadeOutAndDisable());
        }
    }


    private IEnumerator FadeOutAndDisable()
    {
        isAlphaStarted = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < timeToFadeOut)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / timeToFadeOut);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        // Delay for a short time to make sure alpha = 0
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }
}