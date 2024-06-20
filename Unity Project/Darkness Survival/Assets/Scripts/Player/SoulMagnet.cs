using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMagnet : MonoBehaviour
{
    CircleCollider2D collider2d;
    [SerializeField] float defaultRadius = 2.5f;

    private void Awake()
    {
        collider2d = GetComponent<CircleCollider2D>();
        collider2d.radius = 1f;
    }

    public float MAGNET_RADIUS
    {
        get { return defaultRadius; }
        set { collider2d.radius = value; }
    }
}
