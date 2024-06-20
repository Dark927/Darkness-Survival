using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCyberSword : MonoBehaviour
{
    [SerializeField] CyberSword leftAttackObject;
    [SerializeField] CyberSword rightAttackObject;

    [SerializeField] float firstHitDamage = 8f;
    [SerializeField] float secondHitDamage = 12f;

    [SerializeField] float firstKnockBackForce = 4f;
    [SerializeField] float secondKnockBackForce = 7f;

    // Default values 

    float defaultFirstHitDmg;
    float defaultSecondHitDmg;

    PlayerMove playerMove;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    private void Start()
    {
        defaultFirstHitDmg = firstHitDamage;
        defaultSecondHitDmg = secondHitDamage;
    }

    /////////////////////////
    // Getters and setters //
    /////////////////////////

    public float FIRST_HIT_DAMAGE
    {
        get { return defaultFirstHitDmg; }
        set { firstHitDamage = value; }
    }


    public float SECOND_HIT_DAMAGE
    {
        get { return defaultSecondHitDmg; }
        set { secondHitDamage = value; }
    }

    /////////////////////////

    // Code for first hit
    public void Hit(bool isFirstHit)
    {
        if (playerMove.lastHorizontalVector >= 0)
        {
            rightAttackObject.gameObject.SetActive(true);

            // Collider for first or second hit 

            Collider2D rightHit = (isFirstHit) ? rightAttackObject.onFirstHit() : rightAttackObject.onSecondHit();

            if (!rightHit.isTrigger)
                rightHit.isTrigger = true;

            // Damage monsters
            if (rightHit != null)
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightHit.bounds.center, rightHit.bounds.size, 0f);

                if (isFirstHit)
                {
                    CinemachineShake.instance.ShakeCamera(1.2f, .1f);
                    ApplyDamage(colliders, firstHitDamage, firstKnockBackForce);
                }
                else
                {
                    CinemachineShake.instance.ShakeCamera(2f, .25f);
                    ApplyDamage(colliders, secondHitDamage, secondKnockBackForce);
                }

                // After applying damage, off all sword objects

                if (isFirstHit)
                    rightAttackObject.offFirstHit();
                else
                    rightAttackObject.offSecondHit();

                rightAttackObject.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Error : Object -> " + gameObject.name + " must be with Collider2D.");
            }
        }
        else
        {
            leftAttackObject.gameObject.SetActive(true);

            Collider2D leftHit = (isFirstHit) ? leftAttackObject.onFirstHit() : leftAttackObject.onSecondHit();

            if (!leftHit.isTrigger)
                leftHit.isTrigger = true;

            // Damage monsters
            if (leftHit != null)
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftHit.bounds.center, leftHit.bounds.size, 0f);

                if(isFirstHit)
                {
                    CinemachineShake.instance.ShakeCamera(1.2f, .1f);
                    ApplyDamage(colliders, firstHitDamage, firstKnockBackForce);
                }
                else
                {
                    CinemachineShake.instance.ShakeCamera(2f, .25f);
                    ApplyDamage(colliders, secondHitDamage, secondKnockBackForce);
                }

                // After applying damage, off all sword objects

                if (isFirstHit)
                    leftAttackObject.offFirstHit();
                else
                    leftAttackObject.offSecondHit();

                leftAttackObject.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Error : Object -> " + gameObject.name + " must be with Collider2D.");
            }
        }
    }

    // Apply Damage

    private void ApplyDamage(Collider2D[] colliders, float hitDamage, float knockBackForce = 0f)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            IDamageable damageableObj = colliders[i].GetComponent<IDamageable>();

            if (damageableObj != null)
            {
                if (colliders[i].GetComponent<Monsters>() != null)
                {
                    Vector2 direction = (colliders[i].gameObject.transform.position - playerMove.gameObject.transform.position).normalized;
                    Vector2 knockBack = direction * knockBackForce;

                    damageableObj.TakeDamage(hitDamage, knockBack);
                }
                else
                {
                    damageableObj.TakeDamage(hitDamage);
                }
            }
        }
    }
}
