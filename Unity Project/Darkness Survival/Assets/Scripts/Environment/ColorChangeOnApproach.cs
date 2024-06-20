using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeOnApproach : MonoBehaviour
{
    // Attributes 

    #region Attributes

    Transform player;
    [SerializeField] Color targetColor;
    [SerializeField] float approachDistance = 5f;

    Material material;
    Color initialColor;

    float intensity = 6f;

    #endregion

    // Methods 

    #region Methods 

    #region Unity Callbacks

    private void Start()
    {
        player = GameManager.instance.playerTransform;
        material = GetComponent<SpriteRenderer>().material;
        initialColor = material.color;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        float colorChangeProgress = Mathf.Clamp01((approachDistance - distance) / approachDistance);

        Color lerpedColor = new Color(
            Mathf.Lerp(initialColor.r, targetColor.r, colorChangeProgress),
            Mathf.Lerp(initialColor.g, targetColor.g, colorChangeProgress),
            Mathf.Lerp(initialColor.b, targetColor.b, colorChangeProgress),
            Mathf.Lerp(initialColor.a, targetColor.a, colorChangeProgress)
        );

        material.color = lerpedColor * intensity;
    }


    #endregion

    #endregion
}
