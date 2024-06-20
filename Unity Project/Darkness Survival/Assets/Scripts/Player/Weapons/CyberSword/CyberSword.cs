using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberSword : MonoBehaviour
{
    [SerializeField] private Collider2D firstHit;
    [SerializeField] private Collider2D secondHit;

    // Activates 

    public Collider2D onFirstHit()
    {
        firstHit.gameObject.SetActive(true);
        return firstHit;
    }
    public Collider2D onSecondHit()
    {
        secondHit.gameObject.SetActive(true);
        return secondHit;
    }

    // Deactivates 

    public void offFirstHit()
    {
        firstHit.gameObject.SetActive(false);
    }
    public void offSecondHit()
    {
        secondHit.gameObject.SetActive(false);
    }
}
