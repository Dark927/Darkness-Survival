using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterAttack : MonoBehaviour
{
    Animator animator;

    bool isAttacking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        isAttacking = true;
        animator.Play("sharpBlade_def");
    }

    private void LateUpdate()
    {
        if (!isAttacking)
        {
            gameObject.SetActive(false);
        }
    }

    public void offAttack()
    {
        isAttacking = false;
    }

}
