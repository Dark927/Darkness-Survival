using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    float timer = 5f;
    float damage = 0.2f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Character character = collision.GetComponentInParent<Character>();

        if(character != null)
        {
            timer += Time.deltaTime;
            character.TakeDamage(damage*timer);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timer = 5f;
    }
}
