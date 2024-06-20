using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] float timeToLive = 1.5f;
    [SerializeField] float riseHeight = 0.5f;
    [SerializeField] Color endColor = Color.grey;
    TextMeshPro damageText;

    bool isStarted = false;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if (!isStarted)
        {
            StartCoroutine(FadeOutRoutine());
            isStarted = true;
        }
    }

    private IEnumerator FadeOutRoutine()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + Vector3.up * riseHeight;

        Color startColor = damageText.color;

        for (float t = 0f; t < timeToLive; t += Time.deltaTime)
        {
            float normalizedTime = t / timeToLive;
            transform.position = Vector3.Lerp(startPosition, endPosition, normalizedTime);
            damageText.color = Color.Lerp(startColor, endColor, normalizedTime);
            yield return null;
        }

        damageText.color = endColor;
        gameObject.SetActive(false);
        isStarted = false;
        damageText.color = startColor;
    }
}
