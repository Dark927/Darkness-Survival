using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDetection : MonoBehaviour
{
    bool insideObject;
    HashSet<Collider2D> currentCollisions = new HashSet<Collider2D>();
    int objectsLayer;

    private void Awake()
    {
        objectsLayer = LayerMask.NameToLayer("Objects");
    }

    private void Start()
    {
        insideObject = false;   
    }

    public bool IsDetected()
    {
        return insideObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        bool isCharacter = (
            (collisionObject.GetComponentInParent<Monsters>() != null) ||
            (collisionObject.GetComponentInParent<Character>() != null));

        if (collision.gameObject.layer == objectsLayer && !isCharacter)
        {
            currentCollisions.Add(collision);
            insideObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentCollisions.Remove(collision);

        if (currentCollisions.Count == 0)
        {
            insideObject = false;
        }
    }
}
