using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator ChangeColor(Color newColor, float duration)
    {
        if(spriteRenderer == null)
        {
            Debug.Log(" Error :: ColorChange -> spriteRenderer is NULL.");
            yield break;
        }

        // Store the current color of the sprite
        Color currentColor = spriteRenderer.color;

        // Track the time passed
        float timer = 0f;

        while (timer < duration)
        {
            // Interpolate the color based on the time passed divided by the duration
            spriteRenderer.color = Color.Lerp(currentColor, newColor, timer / duration);

            timer += Time.deltaTime;

            // Return control and wait until the next frame
            yield return null;
        }

        // Ensure the final color is set
        spriteRenderer.color = newColor;
    }
}
