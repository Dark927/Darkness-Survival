using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ElectroBlade : WeaponBase
{
    [Header("Settings")]
    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;

    PlayerMove playerMove;

    [Space]

    [SerializeField] Light2D leftLight;
    [SerializeField] Light2D rightLight;

    [Space]

    [SerializeField] Material defaultMaterial;
    [SerializeField] Material bloodyMaterial;
    [SerializeField] Color bloodyLightColor;
    Color defaultLightColor;


    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        defaultLightColor = leftLight.GetComponent<Light2D>().color;
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        for(int i = 0; i < colliders.Length; i++)
        {
            IDamageable damageableObj = colliders[i].GetComponent<IDamageable>();
            if (damageableObj != null)
            {
                int damage = Random.Range((int)weaponStats.minDamage, (int)weaponStats.maxDamage + 1);
                damageableObj.TakeDamage(damage);
            }
        }
    }

    public override void Attack()
    {
        if (playerMove.lastHorizontalVector >= 0 || weaponStats.numberOfAttack > 1)
        {
            // -------------------------------
            // Check attack on final level 
            // -------------------------------

            if (weaponStats.numberOfAttack > 1)
            {
                if (playerMove.lastHorizontalVector >= 0)
                {
                    rightWhipObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
                    rightLight.color = defaultLightColor;
                }
                else
                {
                    rightLight.color = bloodyLightColor;
                    rightWhipObject.GetComponent<SpriteRenderer>().material = bloodyMaterial;
                }
            }

            // -------------------------------

            rightWhipObject.SetActive(true);
            rightLight.intensity = 4.7f;
            rightLight.gameObject.SetActive(true);

            Collider2D rightWhipCollider = rightWhipObject.GetComponent<Collider2D>();

            // Check if trigger 
            if (!rightWhipCollider.isTrigger)
                rightWhipCollider.isTrigger = true;

            // Damage monsters
            if (rightWhipCollider != null)
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipCollider.bounds.center, rightWhipCollider.bounds.size, 0f);
                ApplyDamage(colliders);
            }
            else
            {
                Debug.Log("Error : Object -> " + gameObject.name + " must be with Collider2D.");
            }
        }

        if(playerMove.lastHorizontalVector <= 0 || weaponStats.numberOfAttack > 1)
        {
            // -------------------------------
            // Check attack on final level 
            // -------------------------------

            if (weaponStats.numberOfAttack > 1)
            {
                if (playerMove.lastHorizontalVector <= 0)
                {
                    leftWhipObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
                    leftLight.color = defaultLightColor;
                }
                else
                {
                    leftLight.color = bloodyLightColor;
                    leftWhipObject.GetComponent<SpriteRenderer>().material = bloodyMaterial;
                }
            }

            // -------------------------------

            leftWhipObject.SetActive(true);
            leftLight.intensity = 4.7f;
            leftLight.gameObject.SetActive(true);

            Collider2D leftWhipCollider = leftWhipObject.GetComponent<Collider2D>();

            // Check if trigger 
            if (!leftWhipCollider.isTrigger)
                leftWhipCollider.isTrigger = true;

            // Damage monsters
            if (leftWhipCollider != null)
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipCollider.bounds.center, leftWhipCollider.bounds.size, 0f);
                ApplyDamage(colliders);
            }
            else
            {
                Debug.Log("Error : Object -> " + gameObject.name + " must be with Collider2D.");
            }
        }
    }
}
